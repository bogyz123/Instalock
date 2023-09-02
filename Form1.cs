using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Locker
{
    // Code & Docs by Bogdan (github.com/bogyz123)
    public partial class Form1 : Form
    {
        public Timer clientTimer;
        public Timer championTimer;
        public string port;
        public HttpClient client;
        public string auth;
        public string baseUrl;
        public Form1()
        {
            InitializeComponent();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            errorLabel.ForeColor = System.Drawing.Color.Red;
            client = new HttpClient();
            clientTimer = new Timer();
            championTimer = new Timer();
            clientTimer.Interval = 200;
            clientTimer.Tick += ClientTimer_Tick;
            championTimer.Interval = 500;
            championTimer.Tick += ChampionTimer_Tick;

        }

        private async void ChampionTimer_Tick(object sender, EventArgs e)
        {
            GameState state = await GetGameStateAsync();
            if (state == GameState.ReadyCheck)
            {
                if (checkBox1.Checked)
                {
                    await AcceptMatch();
                }
            }
            else if (state == GameState.ChampSelect)
            {

                dynamic actionId = await GetActionId();
                if (actionId != null)
                {
                    string actionType = actionId["actionType"];
                    string id = actionId["id"];
                    if (actionType == "pick")
                    {
                        string championToPick = GetChampionIdByName(textBox1.Text);
                        await PickChamp(id, championToPick, "pick");
                    }
                    else if (actionType == "ban")
                    {
                        string championToBan = GetChampionIdByName(textBox2.Text);
                        await PickChamp(id, championToBan, "ban");

                    }
                }
            }



        }
        private async Task<Dictionary<string, string>> GetActionId()
        {
            string ourSummonerId = await GetSummonerIdAsync();

            string endpoint = baseUrl + "lol-champ-select/v1/session";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Add("Authorization", "Basic " + auth);

            var data = await client.SendAsync(request);
            var response = await data.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(response);

            foreach (dynamic player in json.myTeam)
            {
                if (player.summonerId == ourSummonerId)
                {
                    var ourCellId = player.cellId;
                    foreach (JArray action in json.actions)
                    {
                        for (int i = 0; i < action.Count; i++)
                        {
                            dynamic act = (dynamic)action[i];
                            if (act.actorCellId == ourCellId)
                            {
                                if ((bool)act.isInProgress)
                                {
                                    Dictionary<string, string> info = new Dictionary<string, string>();
                                    info.Add("id", act["id"].ToString());
                                    info.Add("actionType", (string)act.type);
                                    return info;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
        class ChampionData
        {
            public string championId { get; set; }
            public bool completed { get; set; }
        }
        private async Task PickChamp(string actionId, string championId, string actionType)
        {
            string declareIntent = baseUrl + $"lol-champ-select/v1/session/actions/{actionId}";
            string lockIntent = baseUrl + $"lol-champ-select/v1/session/actions/{actionId}/complete";



            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), declareIntent);
            requestMessage.Headers.Add("Authorization", "Basic " + auth);
            var content = new
            {
                championId = championId
            };
            string toJson = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(toJson, Encoding.UTF8, "application/json");
            requestMessage.Content = stringContent;
            await client.SendAsync(requestMessage);
            // Lock in champion
            var lockRequest = new HttpRequestMessage(HttpMethod.Post, lockIntent);
            lockRequest.Headers.Add("Authorization", "Basic " + auth);
            var lockContent = new ChampionData
            {
                championId = championId,
                completed = true
            };

            string lockContentJson = JsonConvert.SerializeObject(lockContent);
            var stringContentJson = new StringContent(lockContentJson, Encoding.UTF8, "application/json");
            lockRequest.Content = stringContentJson;
            await client.SendAsync(lockRequest);


        }




        private async Task<string> GetSummonerIdAsync()
        {
            string endpoint = baseUrl + "lol-summoner/v1/current-summoner";
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Add("Authorization", "Basic " + auth);
            var response = await client.SendAsync(request);
            var message = await response.Content.ReadAsStringAsync();
            dynamic asJson = JsonConvert.DeserializeObject(message);
            return asJson.summonerId.ToString();
        }
        class Champion
        {
            public string key { get; set; }
            public string id { get; set; }
        }
        private string GetChampionIdByName(string championName)
        {
            var data = GetChampionIds();
            foreach (var item in data)
            {
                if (item.Key == championName)
                {
                    return item.Value.ToString();
                }
            }
            return null;
        }
        private Dictionary<string, string> GetChampionIds()
        {
            string championDataUrl = "http://ddragon.leagueoflegends.com/cdn/13.17.1/data/en_US/champion.json";
            Dictionary<string, string> championDictionary = new Dictionary<string, string>();

            using (WebClient wc = new WebClient())
            {
                try
                {
                    string json = wc.DownloadString(championDataUrl);

                    JObject championData = JObject.Parse(json)["data"] as JObject;

                    foreach (JProperty championProperty in championData.Children())
                    {
                        Champion champion = championProperty.Value.ToObject<Champion>();
                        championDictionary[champion.id] = champion.key;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return championDictionary;
        }
        private async Task AcceptMatch()
        {
            string endpoint = baseUrl + "lol-matchmaking/v1/ready-check/accept";
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), endpoint);
            request.Headers.Add("Authorization", "Basic " + auth);
            await client.SendAsync(request);
        }

        enum GameState
        {
            ReadyCheck,
            ChampSelect,
            Lobby,
            Error,
            None
        }

        private void ClientTimer_Tick1(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
        private async Task<GameState> GetGameStateAsync()
        {
            string endpoint = baseUrl + "lol-gameflow/v1/gameflow-phase";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Add("Authorization", "Basic " + auth);
            try
            {
                var res = await client.SendAsync(request);
                string state = await res.Content.ReadAsStringAsync();
                string trimmed = state.Trim('"');
                switch (trimmed)
                {
                    case "Lobby":
                        {
                            return GameState.Lobby;
                        }
                    case "ReadyCheck":
                        {
                            return GameState.ReadyCheck;
                        }
                    case "ChampSelect":
                        {
                            return GameState.ChampSelect;
                        }
                }
            }
            catch (Exception ex) { }
            return GameState.Error;
        }

        private async void ClientTimer_Tick(object sender, EventArgs e)
        {
            var process = IsClientRunning();
            if (process != null)
            {
                process.Exited += Process_Exited;
                process.EnableRaisingEvents = true;
                open.ForeColor = System.Drawing.Color.Green;
                open.Text = "LeagueClient is running!";
                var data = await GetConnectionDetailsAsync();
                if (data != null)
                {
                    var port = data.Item1;
                    var connection = data.Item2;
                    auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("riot:" + connection));
                    baseUrl = "https://127.0.0.1:" + port + "/";

                }
                panel1.Visible = true;
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var process = IsClientRunning();
            if (process != null)
            {
                open.ForeColor = System.Drawing.Color.Green;
                open.Text = "LeagueClient is running!";
                var data = await GetConnectionDetailsAsync();
                if (data != null)
                {
                    var port = data.Item1;
                    var connection = data.Item2;
                    auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("riot:" + connection));
                    baseUrl = "https://127.0.0.1:" + port + "/";
                }
                process.Exited += Process_Exited;
                process.EnableRaisingEvents = true;
                panel1.Visible = true;
            }
            else
            {
                open.ForeColor = System.Drawing.Color.Red;
                open.Text = "LeagueClient is closed!";
                panel1.Visible = false;
                clientTimer.Start();
            }
        }
        private async Task<Tuple<string, string>> GetConnectionDetailsAsync()
        {
            string path = @"C:\Riot Games\League of Legends\lockfile";

            if (File.Exists(path))
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader rdr = new StreamReader(stream))
                    {
                        var data = await rdr.ReadToEndAsync();
                        var parts = data.Split(':');

                        if (parts.Length >= 4)
                        {
                            var port = parts[2];
                            var connection = parts[3];
                            return new Tuple<string, string>(port, connection);
                        }
                    }
                }
            }
            return null;
        }


        private void Process_Exited(object sender, EventArgs e)
        {
            panel1.Invoke((MethodInvoker)delegate
            {
                open.ForeColor = System.Drawing.Color.Red;
                open.Text = "LeagueClient is closed!";
                panel1.Visible = false;
                clientTimer.Start();
            });
        }
        private Process IsClientRunning()
        {
            Process[] leagueProcesses = Process.GetProcessesByName("LeagueClient");
            if (leagueProcesses.Length > 0)
            {
                return leagueProcesses[0];
            }
            return null; // Return null when the process is not running
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                errorLabel.Text = "Error, enter a champion.";
                return;
            }
            championTimer.Start();
        }
    }
}
