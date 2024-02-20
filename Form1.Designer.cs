namespace Locker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.open = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.spellList = new System.Windows.Forms.Panel();
            this.Flash = new System.Windows.Forms.PictureBox();
            this.Clarity = new System.Windows.Forms.PictureBox();
            this.Barrier = new System.Windows.Forms.PictureBox();
            this.Ignite = new System.Windows.Forms.PictureBox();
            this.Cleanse = new System.Windows.Forms.PictureBox();
            this.Smite = new System.Windows.Forms.PictureBox();
            this.Teleport = new System.Windows.Forms.PictureBox();
            this.Snowball = new System.Windows.Forms.PictureBox();
            this.Heal = new System.Windows.Forms.PictureBox();
            this.Exhaust = new System.Windows.Forms.PictureBox();
            this.Ghost = new System.Windows.Forms.PictureBox();
            this.selectedSpellsPanel = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.spellList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Flash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Clarity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Barrier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ignite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cleanse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Smite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Teleport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Snowball)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Heal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exhaust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ghost)).BeginInit();
            this.SuspendLayout();
            // 
            // open
            // 
            this.open.AutoSize = true;
            this.open.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.open.ForeColor = System.Drawing.Color.Crimson;
            this.open.Location = new System.Drawing.Point(179, 9);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(81, 13);
            this.open.TabIndex = 0;
            this.open.Text = "LeagueClient";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.spellList);
            this.panel1.Controls.Add(this.selectedSpellsPanel);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.errorLabel);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 288);
            this.panel1.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Black;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Crimson;
            this.button4.Location = new System.Drawing.Point(74, 256);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(73, 24);
            this.button4.TabIndex = 26;
            this.button4.Text = "Dodge";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Crimson;
            this.button2.Location = new System.Drawing.Point(3, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 24);
            this.button2.TabIndex = 25;
            this.button2.Text = "Reset All";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // spellList
            // 
            this.spellList.BackColor = System.Drawing.Color.Black;
            this.spellList.Controls.Add(this.Flash);
            this.spellList.Controls.Add(this.Clarity);
            this.spellList.Controls.Add(this.Barrier);
            this.spellList.Controls.Add(this.Ignite);
            this.spellList.Controls.Add(this.Cleanse);
            this.spellList.Controls.Add(this.Smite);
            this.spellList.Controls.Add(this.Teleport);
            this.spellList.Controls.Add(this.Snowball);
            this.spellList.Controls.Add(this.Heal);
            this.spellList.Controls.Add(this.Exhaust);
            this.spellList.Controls.Add(this.Ghost);
            this.spellList.Location = new System.Drawing.Point(69, 90);
            this.spellList.Name = "spellList";
            this.spellList.Size = new System.Drawing.Size(317, 28);
            this.spellList.TabIndex = 24;
            // 
            // Flash
            // 
            this.Flash.Image = ((System.Drawing.Image)(resources.GetObject("Flash.Image")));
            this.Flash.Location = new System.Drawing.Point(3, 1);
            this.Flash.Name = "Flash";
            this.Flash.Size = new System.Drawing.Size(27, 27);
            this.Flash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Flash.TabIndex = 11;
            this.Flash.TabStop = false;
            // 
            // Clarity
            // 
            this.Clarity.Image = ((System.Drawing.Image)(resources.GetObject("Clarity.Image")));
            this.Clarity.Location = new System.Drawing.Point(230, 1);
            this.Clarity.Name = "Clarity";
            this.Clarity.Size = new System.Drawing.Size(27, 27);
            this.Clarity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Clarity.TabIndex = 12;
            this.Clarity.TabStop = false;
            // 
            // Barrier
            // 
            this.Barrier.Image = ((System.Drawing.Image)(resources.GetObject("Barrier.Image")));
            this.Barrier.Location = new System.Drawing.Point(201, 1);
            this.Barrier.Name = "Barrier";
            this.Barrier.Size = new System.Drawing.Size(27, 27);
            this.Barrier.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Barrier.TabIndex = 13;
            this.Barrier.TabStop = false;
            // 
            // Ignite
            // 
            this.Ignite.Image = ((System.Drawing.Image)(resources.GetObject("Ignite.Image")));
            this.Ignite.Location = new System.Drawing.Point(32, 1);
            this.Ignite.Name = "Ignite";
            this.Ignite.Size = new System.Drawing.Size(27, 27);
            this.Ignite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Ignite.TabIndex = 21;
            this.Ignite.TabStop = false;
            // 
            // Cleanse
            // 
            this.Cleanse.Image = ((System.Drawing.Image)(resources.GetObject("Cleanse.Image")));
            this.Cleanse.Location = new System.Drawing.Point(286, 1);
            this.Cleanse.Name = "Cleanse";
            this.Cleanse.Size = new System.Drawing.Size(27, 27);
            this.Cleanse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Cleanse.TabIndex = 14;
            this.Cleanse.TabStop = false;
            // 
            // Smite
            // 
            this.Smite.Image = ((System.Drawing.Image)(resources.GetObject("Smite.Image")));
            this.Smite.Location = new System.Drawing.Point(88, 1);
            this.Smite.Name = "Smite";
            this.Smite.Size = new System.Drawing.Size(27, 27);
            this.Smite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Smite.TabIndex = 20;
            this.Smite.TabStop = false;
            // 
            // Teleport
            // 
            this.Teleport.Image = ((System.Drawing.Image)(resources.GetObject("Teleport.Image")));
            this.Teleport.Location = new System.Drawing.Point(145, 1);
            this.Teleport.Name = "Teleport";
            this.Teleport.Size = new System.Drawing.Size(27, 27);
            this.Teleport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Teleport.TabIndex = 15;
            this.Teleport.TabStop = false;
            // 
            // Snowball
            // 
            this.Snowball.Image = ((System.Drawing.Image)(resources.GetObject("Snowball.Image")));
            this.Snowball.Location = new System.Drawing.Point(258, 1);
            this.Snowball.Name = "Snowball";
            this.Snowball.Size = new System.Drawing.Size(27, 27);
            this.Snowball.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Snowball.TabIndex = 19;
            this.Snowball.TabStop = false;
            // 
            // Heal
            // 
            this.Heal.Image = ((System.Drawing.Image)(resources.GetObject("Heal.Image")));
            this.Heal.Location = new System.Drawing.Point(173, 1);
            this.Heal.Name = "Heal";
            this.Heal.Size = new System.Drawing.Size(27, 27);
            this.Heal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Heal.TabIndex = 16;
            this.Heal.TabStop = false;
            // 
            // Exhaust
            // 
            this.Exhaust.Image = ((System.Drawing.Image)(resources.GetObject("Exhaust.Image")));
            this.Exhaust.Location = new System.Drawing.Point(117, 1);
            this.Exhaust.Name = "Exhaust";
            this.Exhaust.Size = new System.Drawing.Size(27, 27);
            this.Exhaust.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Exhaust.TabIndex = 18;
            this.Exhaust.TabStop = false;
            // 
            // Ghost
            // 
            this.Ghost.Image = ((System.Drawing.Image)(resources.GetObject("Ghost.Image")));
            this.Ghost.Location = new System.Drawing.Point(61, 1);
            this.Ghost.Name = "Ghost";
            this.Ghost.Size = new System.Drawing.Size(27, 27);
            this.Ghost.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Ghost.TabIndex = 17;
            this.Ghost.TabStop = false;
            // 
            // selectedSpellsPanel
            // 
            this.selectedSpellsPanel.BackColor = System.Drawing.Color.Black;
            this.selectedSpellsPanel.Location = new System.Drawing.Point(193, 182);
            this.selectedSpellsPanel.Name = "selectedSpellsPanel";
            this.selectedSpellsPanel.Size = new System.Drawing.Size(67, 31);
            this.selectedSpellsPanel.TabIndex = 23;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Black;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Crimson;
            this.button3.Location = new System.Drawing.Point(72, 123);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(310, 24);
            this.button3.TabIndex = 22;
            this.button3.Text = "Reset Spells";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Crimson;
            this.textBox2.Location = new System.Drawing.Point(260, 64);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(112, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "Zed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Crimson;
            this.label4.Location = new System.Drawing.Point(266, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ban Champion";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.ForeColor = System.Drawing.Color.Crimson;
            this.checkBox1.Location = new System.Drawing.Point(372, 261);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Auto Accept Q";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(147, 265);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 13);
            this.errorLabel.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Crimson;
            this.button1.Location = new System.Drawing.Point(78, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(298, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(92, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Champion";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Crimson;
            this.textBox1.Location = new System.Drawing.Point(74, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Yasuo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(84, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select champion to pick, ban, spells and Save!";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(484, 315);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.open);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Instalock";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.spellList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Flash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Clarity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Barrier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ignite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cleanse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Smite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Teleport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Snowball)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Heal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exhaust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ghost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label open;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox Flash;
        private System.Windows.Forms.PictureBox Ignite;
        private System.Windows.Forms.PictureBox Smite;
        private System.Windows.Forms.PictureBox Snowball;
        private System.Windows.Forms.PictureBox Exhaust;
        private System.Windows.Forms.PictureBox Ghost;
        private System.Windows.Forms.PictureBox Heal;
        private System.Windows.Forms.PictureBox Teleport;
        private System.Windows.Forms.PictureBox Cleanse;
        private System.Windows.Forms.PictureBox Barrier;
        private System.Windows.Forms.PictureBox Clarity;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel selectedSpellsPanel;
        private System.Windows.Forms.Panel spellList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
    }
}

