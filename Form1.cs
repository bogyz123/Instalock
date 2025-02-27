using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Locker
{
    // Code & documentation written by Bogdan (github.com/bogyz123)!
    enum GameState
    {
        ReadyCheck,
        ChampSelect,
        Lobby,
        Error,
        None
    }

    enum Spell
    {
        Barrier = 21,
        Cleanse = 1,
        Ignite = 14,
        Exhaust = 3,
        Flash = 4,
        Ghost = 6,
        Heal = 7,
        Clarity = 13,
        Smite = 11,
        Snowball = 32,
        Teleport = 12
    }
    public partial class Form1 : Form
    {
        public Timer clientTimer;
        public Timer championTimer;
        public string port;
        public HttpClient client;
        public string auth;
        public string baseUrl;
        public PictureBox[] selectedSpells = new PictureBox[2];
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
            foreach (Control control in spellList.Controls)
            {
                if (control is PictureBox spellIcon)
                {
                    spellIcon.Cursor = Cursors.Cross;
                    spellIcon.Click += SpellIcon_Click;
                }
            }

        }
        // I will try to document each function a bit respectively.
        private async Task<string> SendRequest(HttpMethod method, string endpoint, object data)
        {
            // We use this to send a Http Request.
            HttpRequestMessage request = new HttpRequestMessage(method, endpoint);
            request.Headers.Add("Authorization", "Basic " + auth);
            if (data != null)
            {
                var asJson = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(asJson, Encoding.UTF8, "application/json");
            }
            var res = await client.SendAsync(request);
            var answer = await res.Content.ReadAsStringAsync();
            return answer;
        }
        private void SpellIcon_Click(object sender, EventArgs e)
        {
            // When user clicks on a Spell image, adds Spell name to selectedSpells array
            if (sender is PictureBox clickedPictureBox)
            {
                selectedSpellsPanel.Visible = true;


                if (selectedSpells[0] == null)
                {
                    selectedSpells[0] = clickedPictureBox;
                    selectedSpellsPanel.Controls.Add(new PictureBox
                    {
                        Image = clickedPictureBox.Image,
                        Size = clickedPictureBox.Size,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    });
                }

                else if (selectedSpells[1] == null)
                {
                    if (selectedSpells[0].Name == clickedPictureBox.Name)
                    {
                        return;
                    }
                    selectedSpells[1] = clickedPictureBox;
                    selectedSpellsPanel.Controls.Add(new PictureBox
                    {
                        Image = clickedPictureBox.Image,
                        Size = clickedPictureBox.Size,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Location = new System.Drawing.Point(27, 0)

                    });
                }

                else
                {
                    if (MessageBox.Show("You selected the maximum number of spells. Click Yes to reset.", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        selectedSpells[0] = null;
                        selectedSpells[1] = null;
                        selectedSpellsPanel.Controls.Clear();
                        selectedSpellsPanel.Visible = false;
                    }
                }
            }
        }


        private async void ChampionTimer_Tick(object sender, EventArgs e)
        {
            // When users presses Save, check for Game State, if champion select, then lock in champion, ban champion & select Spells that user provided.
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

                if (spellList.Visible)
                {
                    spellList.Visible = false;
                }
                dynamic actionId = await GetActionId();
                if (actionId != null)
                {
                    string actionType = actionId["actionType"];
                    string id = actionId["id"];
                    if (selectedSpells[0] != null && selectedSpells[1] != null)
                    {
                        Spell spell1 = (Spell)Enum.Parse(typeof(Spell), selectedSpells[0].Name);
                        Spell spell2 = (Spell)Enum.Parse(typeof(Spell), selectedSpells[1].Name);
                        await SelectSpells(spell1, spell2);
                    }

                    if (actionType == "pick")
                    {
                        string championToPick = GetChampionIdByName(textBox1.Text);
                        if (championToPick != null)
                        {
                            await PickChamp(id, championToPick, "pick");
                        }
                    }
                    else if (actionType == "ban")
                    {
                        string championToBan = GetChampionIdByName(textBox2.Text);
                        if (championToBan != null)
                        {
                            await PickChamp(id, championToBan, "ban");
                        }

                    }

                }
            }
            else
            {
                spellList.Visible = true;
            }
        }
        private async Task SelectSpells(Spell spell1, Spell spell2)
        {
            // Selects the given Spells, converts them to their ID and sends the data.
            string endpoint = baseUrl + "lol-champ-select/v1/session/my-selection";
            var data = new
            {
                spell1Id = (int)spell1,
                spell2Id = (int)spell2
            };
            await SendRequest(new HttpMethod("PATCH"), endpoint, data);
        }


        private async Task<Dictionary<string, string>> GetActionId()
        {
            // When we are in champion select, this function will check if we are banning or picking.
            // It will then get our actionId from actions object so we can send that id with our request (that's how they distinguish what player called)
            // also we return actionType, which is either pick or ban.
            string ourSummonerId = await GetSummonerIdAsync();
            string endpoint = baseUrl + "lol-champ-select/v1/session";

            string response = await SendRequest(HttpMethod.Get, endpoint, null);


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

        private async Task PickChamp(string actionId, string championId, string actionType)
        {
            // Picks a champion by it's ID.
            string declareIntent = baseUrl + $"lol-champ-select/v1/session/actions/{actionId}";
            string lockIntent = baseUrl + $"lol-champ-select/v1/session/actions/{actionId}/complete";


            var content = new
            {
                championId = championId
            };
            await SendRequest(new HttpMethod("PATCH"), declareIntent, content);



            // Locks the champion in.

            var lockContent = new ChampionData
            {
                championId = championId,
                completed = true
            };
            await SendRequest(HttpMethod.Post, lockIntent, lockContent);


        }

        private async Task<string> GetSummonerIdAsync()
        {
            // Gets our own summoner id.
            string endpoint = baseUrl + "lol-summoner/v1/current-summoner";
            string response = await SendRequest(HttpMethod.Get, endpoint, null);
            dynamic asJson = JsonConvert.DeserializeObject(response);
            return asJson.summonerId.ToString();
        }
        private string GetChampionIdByName(string championName)
        {
            // Gets the champion ID by champion name.
            string capitalized = char.ToUpper(championName[0]) + championName.Substring(1).ToLower();
            var data = GetChampionIds();
            foreach (var item in data)
            {
                if (item.Key == capitalized)
                {
                    return item.Value.ToString();
                }
            }
            return null;
        }
        private Dictionary<string, string> GetChampionIds()
        {
            // Returns all champion ids from the web cdn.
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
            // Accepts a match if ReadyCheck is present.
            string endpoint = baseUrl + "lol-matchmaking/v1/ready-check/accept";
            await SendRequest(HttpMethod.Post, endpoint, null);
        }

        private void ClientTimer_Tick1(object sender, EventArgs e)
        {

        }
        private async Task<GameState> GetGameStateAsync()
        {
            // Gets the game state, converts it to GameState enum.
            string endpoint = baseUrl + "lol-gameflow/v1/gameflow-phase";
            string response = await SendRequest(HttpMethod.Get, endpoint, null);
            string trimmed = response.Trim('"');
            try
            {
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
            // If the LeagueClient opened, attach a Exit handler to the process, and get all necessary data from the client.
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
            // When our program loads, checks if LeagueClient is running or not. If not, start clientTimer that waits for it to open, otherwise, get all necessary data.
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
            // Gets connection details needed to authenticate the Http Requests. All details are present in the lockfile when LeagueClient is running.
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
            // This function triggers when LeagueClient exits. We then hide our controls from the panel and start a timer that waits for League to open.
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
            // Determines whether LeagueClient is running or not, if yes, returns the Process.
            Process[] leagueProcesses = Process.GetProcessesByName("LeagueClient");
            if (leagueProcesses.Length > 0)
            {
                return leagueProcesses[0];
            }
            return null; // Return null when the process is not running
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Save button, checks if there is input, if so, start a timer that waits for user to get into champion select so we can pick, ban, set spells.
            if (textBox1.Text.Length == 0)
            {
                errorLabel.Text = "Error, enter a champion to pick.";
                return;
            }
            championTimer.Start();
            SetIcon();
            SetBackground();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Reset spells button, sets selected spell to null and clears them from the panel.
            selectedSpells[0] = null;
            selectedSpells[1] = null;
            selectedSpellsPanel.Controls.Clear();
            selectedSpellsPanel.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Reset all button, stops every timer and removes everything so the user can re-select the spells, champs, etc, and press Save to restart timers.
            championTimer.Stop();
            clientTimer.Stop();
            textBox1.Text = "";
            textBox2.Text = "";
            selectedSpellsPanel.Controls.Clear();
            selectedSpells[0] = null;
            selectedSpells[1] = null;
            selectedSpellsPanel.Visible = false;
        }
        private async Task SetIcon()
        {
            string endpoint = baseUrl + "lol-chat/v1/me";
            var value = Convert.ToInt32(iconBox.Text);
            await SendRequest(HttpMethod.Put, endpoint, new { icon = value });
        }
        private async Task SetRank(string rankTier, string rankDivision)
        {
            string endpoint = baseUrl + "lol-chat/v1/me";
            var data = new
            {
                lol = new
                {
                    rankedLeagueQueue = "RANKED_SOLO_5x5",
                    rankedLeagueTier = rankTier,
                    rankedLeagueDivision = rankDivision,
                }
            };
            await SendRequest(HttpMethod.Put, endpoint, data);
        }
        private async Task SetBackground()
        {
            string endpoint = baseUrl + "lol-summoner/v1/current-summoner/summoner-profile/";
            var val = Convert.ToInt32(bgBox.Text);
            var data = new
            {
                key = "backgroundSkinId",
                value = val
            };
             await SendRequest(HttpMethod.Post, endpoint, data);
    
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SetRank("Bronze", "I");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SetRank("Silver", "I");
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            SetRank("Gold", "I");
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            SetRank("Platinum", "I");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            SetRank("Emerald", "I");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            SetRank("Diamond", "I");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SetRank("Master", "I");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SetRank("Grandmaster", "I");
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            SetRank("Challenger", "I");
        }
    }
}
class ChampionData
{

    public string championId { get; set; }
    public bool completed { get; set; }
}
class Summoner
{
    public string name { get; set; }
}
class Champion
{
    public string key { get; set; }
    public string id { get; set; }
}