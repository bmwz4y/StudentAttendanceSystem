namespace StudentAttendanceSystem
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1_leftPanel = new System.Windows.Forms.Panel();
            this.loginID_label = new System.Windows.Forms.Label();
            this.loginID_textBox = new System.Windows.Forms.TextBox();
            this.loginPass_label = new System.Windows.Forms.Label();
            this.loginPass_textBox = new System.Windows.Forms.TextBox();
            this.login_button = new System.Windows.Forms.Button();
            this.panel2_leftPanel = new System.Windows.Forms.Panel();
            this.info_label = new System.Windows.Forms.Label();
            this.info_richTextBox = new System.Windows.Forms.RichTextBox();
            this.today_label = new System.Windows.Forms.Label();
            this.today_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel2_rightPanel = new System.Windows.Forms.Panel();
            this.label1_rightPanel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.logout_button = new System.Windows.Forms.Button();
            this.panel1_rightPanel = new System.Windows.Forms.Panel();
            this.loginPass_errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.loginID_errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1_leftPanel.SuspendLayout();
            this.panel2_leftPanel.SuspendLayout();
            this.panel2_rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginPass_errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginID_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1_leftPanel
            // 
            this.panel1_leftPanel.Controls.Add(this.loginID_label);
            this.panel1_leftPanel.Controls.Add(this.loginID_textBox);
            this.panel1_leftPanel.Controls.Add(this.loginPass_label);
            this.panel1_leftPanel.Controls.Add(this.loginPass_textBox);
            this.panel1_leftPanel.Controls.Add(this.login_button);
            this.panel1_leftPanel.Location = new System.Drawing.Point(12, 12);
            this.panel1_leftPanel.Name = "panel1_leftPanel";
            this.panel1_leftPanel.Size = new System.Drawing.Size(320, 538);
            this.panel1_leftPanel.TabIndex = 0;
            // 
            // loginID_label
            // 
            this.loginID_label.AutoSize = true;
            this.loginID_label.Font = new System.Drawing.Font("Tahoma", 14F);
            this.loginID_label.Location = new System.Drawing.Point(21, 100);
            this.loginID_label.Name = "loginID_label";
            this.loginID_label.Size = new System.Drawing.Size(141, 23);
            this.loginID_label.TabIndex = 3;
            this.loginID_label.Text = "Username (ID):";
            // 
            // loginID_textBox
            // 
            this.loginID_textBox.Font = new System.Drawing.Font("Tahoma", 14F);
            this.loginID_textBox.Location = new System.Drawing.Point(25, 126);
            this.loginID_textBox.Name = "loginID_textBox";
            this.loginID_textBox.Size = new System.Drawing.Size(256, 30);
            this.loginID_textBox.TabIndex = 1;
            this.loginID_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.loginPass_textBox_KeyDown);
            // 
            // loginPass_label
            // 
            this.loginPass_label.AutoSize = true;
            this.loginPass_label.Font = new System.Drawing.Font("Tahoma", 14F);
            this.loginPass_label.Location = new System.Drawing.Point(21, 186);
            this.loginPass_label.Name = "loginPass_label";
            this.loginPass_label.Size = new System.Drawing.Size(95, 23);
            this.loginPass_label.TabIndex = 4;
            this.loginPass_label.Text = "Password:";
            // 
            // loginPass_textBox
            // 
            this.loginPass_textBox.Font = new System.Drawing.Font("Tahoma", 14F);
            this.loginPass_textBox.Location = new System.Drawing.Point(25, 212);
            this.loginPass_textBox.Name = "loginPass_textBox";
            this.loginPass_textBox.Size = new System.Drawing.Size(256, 30);
            this.loginPass_textBox.TabIndex = 2;
            this.loginPass_textBox.UseSystemPasswordChar = true;
            this.loginPass_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.loginPass_textBox_KeyDown);
            // 
            // login_button
            // 
            this.login_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.login_button.Font = new System.Drawing.Font("Tahoma", 12F);
            this.login_button.ForeColor = System.Drawing.Color.DarkGreen;
            this.login_button.Location = new System.Drawing.Point(90, 318);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(128, 32);
            this.login_button.TabIndex = 0;
            this.login_button.Text = "Login";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // panel2_leftPanel
            // 
            this.panel2_leftPanel.Controls.Add(this.info_label);
            this.panel2_leftPanel.Controls.Add(this.info_richTextBox);
            this.panel2_leftPanel.Controls.Add(this.today_label);
            this.panel2_leftPanel.Controls.Add(this.today_dateTimePicker);
            this.panel2_leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2_leftPanel.Location = new System.Drawing.Point(0, 0);
            this.panel2_leftPanel.Name = "panel2_leftPanel";
            this.panel2_leftPanel.Size = new System.Drawing.Size(984, 562);
            this.panel2_leftPanel.TabIndex = 5;
            this.panel2_leftPanel.Visible = false;
            // 
            // info_label
            // 
            this.info_label.AutoSize = true;
            this.info_label.Font = new System.Drawing.Font("Tahoma", 14F);
            this.info_label.Location = new System.Drawing.Point(21, 19);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(240, 23);
            this.info_label.TabIndex = 1;
            this.info_label.Text = "My account (accountType):";
            // 
            // info_richTextBox
            // 
            this.info_richTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.info_richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.info_richTextBox.Font = new System.Drawing.Font("Tahoma", 12F);
            this.info_richTextBox.Location = new System.Drawing.Point(40, 60);
            this.info_richTextBox.Name = "info_richTextBox";
            this.info_richTextBox.ReadOnly = true;
            this.info_richTextBox.Size = new System.Drawing.Size(241, 313);
            this.info_richTextBox.TabIndex = 2;
            this.info_richTextBox.Text = "ID: *user id*\nName: *user name*\nEmail: *user email*\nphone #: *user phone number*\n" +
    "...\n...\n...\n";
            // 
            // today_label
            // 
            this.today_label.AutoSize = true;
            this.today_label.Font = new System.Drawing.Font("Tahoma", 12F);
            this.today_label.Location = new System.Drawing.Point(9, 480);
            this.today_label.Name = "today_label";
            this.today_label.Size = new System.Drawing.Size(64, 19);
            this.today_label.TabIndex = 3;
            this.today_label.Text = "Today: ";
            // 
            // today_dateTimePicker
            // 
            this.today_dateTimePicker.Enabled = false;
            this.today_dateTimePicker.Font = new System.Drawing.Font("Tahoma", 10F);
            this.today_dateTimePicker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.today_dateTimePicker.Location = new System.Drawing.Point(79, 480);
            this.today_dateTimePicker.Name = "today_dateTimePicker";
            this.today_dateTimePicker.Size = new System.Drawing.Size(241, 24);
            this.today_dateTimePicker.TabIndex = 0;
            this.today_dateTimePicker.Value = new System.DateTime(2016, 11, 27, 0, 0, 0, 0);
            // 
            // panel2_rightPanel
            // 
            this.panel2_rightPanel.BackColor = System.Drawing.Color.White;
            this.panel2_rightPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2_rightPanel.Controls.Add(this.label1_rightPanel);
            this.panel2_rightPanel.Controls.Add(this.dataGridView1);
            this.panel2_rightPanel.Controls.Add(this.button1);
            this.panel2_rightPanel.Controls.Add(this.button2);
            this.panel2_rightPanel.Controls.Add(this.button3);
            this.panel2_rightPanel.Controls.Add(this.button4);
            this.panel2_rightPanel.Controls.Add(this.button5);
            this.panel2_rightPanel.Controls.Add(this.button6);
            this.panel2_rightPanel.Controls.Add(this.logout_button);
            this.panel2_rightPanel.Location = new System.Drawing.Point(339, 12);
            this.panel2_rightPanel.Name = "panel2_rightPanel";
            this.panel2_rightPanel.Size = new System.Drawing.Size(633, 537);
            this.panel2_rightPanel.TabIndex = 2;
            this.panel2_rightPanel.Visible = false;
            // 
            // label1_rightPanel
            // 
            this.label1_rightPanel.AutoSize = true;
            this.label1_rightPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1_rightPanel.Location = new System.Drawing.Point(16, 13);
            this.label1_rightPanel.Name = "label1_rightPanel";
            this.label1_rightPanel.Size = new System.Drawing.Size(167, 16);
            this.label1_rightPanel.TabIndex = 6;
            this.label1_rightPanel.Text = "*label1_rightPanel text*";
            this.label1_rightPanel.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 34);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(596, 358);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(51, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(250, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "*button1 text*";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Location = new System.Drawing.Point(51, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(250, 50);
            this.button2.TabIndex = 1;
            this.button2.Text = "*button2 text*";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button3.Location = new System.Drawing.Point(51, 173);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(250, 50);
            this.button3.TabIndex = 2;
            this.button3.Text = "*button3 text*";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button4.Location = new System.Drawing.Point(51, 244);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(250, 50);
            this.button4.TabIndex = 3;
            this.button4.Text = "*button4 text*";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button5.Location = new System.Drawing.Point(356, 410);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(250, 50);
            this.button5.TabIndex = 3;
            this.button5.Text = "*button5 text*";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button6.Location = new System.Drawing.Point(19, 410);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(250, 50);
            this.button6.TabIndex = 4;
            this.button6.Text = "*button6 text*";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // logout_button
            // 
            this.logout_button.Font = new System.Drawing.Font("Tahoma", 10F);
            this.logout_button.ForeColor = System.Drawing.Color.Brown;
            this.logout_button.Location = new System.Drawing.Point(341, 34);
            this.logout_button.Name = "logout_button";
            this.logout_button.Size = new System.Drawing.Size(250, 50);
            this.logout_button.TabIndex = 3;
            this.logout_button.Text = "Logout";
            this.logout_button.UseVisualStyleBackColor = true;
            this.logout_button.Click += new System.EventHandler(this.logout_button_Click);
            // 
            // panel1_rightPanel
            // 
            this.panel1_rightPanel.BackColor = System.Drawing.Color.White;
            this.panel1_rightPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1_rightPanel.BackgroundImage")));
            this.panel1_rightPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1_rightPanel.Location = new System.Drawing.Point(339, 13);
            this.panel1_rightPanel.Name = "panel1_rightPanel";
            this.panel1_rightPanel.Size = new System.Drawing.Size(633, 537);
            this.panel1_rightPanel.TabIndex = 1;
            // 
            // loginPass_errorProvider
            // 
            this.loginPass_errorProvider.ContainerControl = this;
            // 
            // loginID_errorProvider
            // 
            this.loginID_errorProvider.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.panel2_rightPanel);
            this.Controls.Add(this.panel1_rightPanel);
            this.Controls.Add(this.panel2_leftPanel);
            this.Controls.Add(this.panel1_leftPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Student Attendance System - Main";
            this.panel1_leftPanel.ResumeLayout(false);
            this.panel1_leftPanel.PerformLayout();
            this.panel2_leftPanel.ResumeLayout(false);
            this.panel2_leftPanel.PerformLayout();
            this.panel2_rightPanel.ResumeLayout(false);
            this.panel2_rightPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginPass_errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginID_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1_leftPanel;
        private System.Windows.Forms.Panel panel1_rightPanel;
        private System.Windows.Forms.TextBox loginPass_textBox;
        private System.Windows.Forms.TextBox loginID_textBox;
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Label loginPass_label;
        private System.Windows.Forms.Label loginID_label;
        private System.Windows.Forms.Panel panel2_rightPanel;
        private System.Windows.Forms.Panel panel2_leftPanel;
        private System.Windows.Forms.RichTextBox info_richTextBox;
        private System.Windows.Forms.Label info_label;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button logout_button;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker today_dateTimePicker;
        private System.Windows.Forms.ErrorProvider loginPass_errorProvider;
        private System.Windows.Forms.ErrorProvider loginID_errorProvider;
        private System.Windows.Forms.Label today_label;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1_rightPanel;
    }
}