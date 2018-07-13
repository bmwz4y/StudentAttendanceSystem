using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace StudentAttendanceSystem
{
    //selfnote: MVC style without doing MVC
    public partial class Form1 : Form
    {
        private static string sqlConnectionString = "server=localhost;user id=root;password=23;database=StudentAttendanceSystemDB";
        private static MySqlConnection sqlConnection = new MySqlConnection(sqlConnectionString);

        private static User user;
        private string todayColumnString;
        private static DataSet dataSet = null;

        //Initial Values
        public static int TOTAL_NUMBER_OF_LECTURES = 19;

        public static string LECTURE_START_TIME = "7:45 AM";
        public static int LECTURE_MAX_LATE_MINUTES = 15;
        public static string CLIENT_EMAIL = "TestEmailForSP@yandex.com";
        public static string CLIENT_EMAIL_PASS = "Thepass4testing";
        public static string SMTP_SERVER_URL_ADDRESS = "smtp.yandex.com";
        private static DateTime lectureStartTime = new DateTime();
        private static DateTime lectureLateTime = new DateTime();
        private static Timer EventTimer = new Timer();//within 3 hours from lecture time it runs daily
        private static bool dailyIntervalIsSet = false;

        // Creates or loads an INI file in the same directory as your executable
        private static IniFile SettingsIni = new IniFile("Settings.ini");

        public Form1()
        {
            //initialize settings
            initializeIniFile();

            // Adds the event and the event handler for the method that will process the timer event to the timer.
            EventTimer.Tick += new EventHandler(TimerEventProcessor);
            // Sets initial timer interval to 5 seconds.
            EventTimer.Interval = 5000;
            EventTimer.Start();

            //initialize form
            InitializeComponent();
            loginID_textBox.Select();

            //initialize special variables
            DateTime.TryParse(LECTURE_START_TIME, out lectureStartTime);
            lectureLateTime = lectureStartTime.AddMinutes(LECTURE_MAX_LATE_MINUTES);
        }

        private static void initializeIniFile()
        {
            // set Teacher section of Ini if not exist
            if (!SettingsIni.KeyExists("Name", "Teacher"))
            {
                SettingsIni.Write("Name", User.TEACHER_NAME, "Teacher");
            }
            else
            {
                //read value from file
                User.TEACHER_NAME = SettingsIni.Read("Name", "Teacher");
            }
            if (!SettingsIni.KeyExists("Email", "Teacher"))
            {
                SettingsIni.Write("Email", User.TEACHER_EMAIL, "Teacher");
            }
            else
            {
                User.TEACHER_EMAIL = SettingsIni.Read("Email", "Teacher");
            }
            if (!SettingsIni.KeyExists("Password", "Teacher"))
            {
                SettingsIni.Write("Password", User.TEACHER_PASSWORD, "Teacher");
            }
            else
            {
                User.TEACHER_PASSWORD = SettingsIni.Read("Password", "Teacher");
            }
            if (!SettingsIni.KeyExists("Phone_number", "Teacher"))
            {
                SettingsIni.Write("Phone_number", User.TEACHER_PHONE_NUMBER, "Teacher");
            }
            else
            {
                User.TEACHER_PHONE_NUMBER = SettingsIni.Read("Phone_number", "Teacher");
            }
        }

        // This is the method to run when the timer is raised (Scheduled Task)
        private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            //if time now is within 3 hours from lecture late time (lecture time + max late minutes) and daily not set then update interval for timer to 1 day and set daily
            if (DateTime.Now > lectureLateTime && DateTime.Now < lectureLateTime.AddHours(3) && !dailyIntervalIsSet)
            {
                EventTimer.Interval = (int)((TimeSpan.FromDays(1)).TotalMilliseconds);
                dailyIntervalIsSet = true;
            }

            if (dailyIntervalIsSet)
            {
                //do Scheduled Task: Send emails to both the student and lecturer when the student absents exceeds 10%, 15% and 20% of the total number of lectures.
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    //return all rows
                    string sqlStatement = "SELECT * FROM Lecture_Attendance";
                    MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);
                    MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(sqlStatement, sqlConnection);
                    try
                    {
                        MySqlCommandBuilder sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);
                        dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }

                int studentAbsencePercentage;
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    //use University_ID to get percentage out of 100
                    string studentUniversity_ID = row.ItemArray[0].ToString();
                    studentAbsencePercentage = student_absence_percentage(studentUniversity_ID);
                    int mailedPercentage = 0;

                    // student absents exceeds 10%, 15% and 20% send mail
                    if (studentAbsencePercentage >= 20)
                        mailedPercentage = 20;
                    else if (studentAbsencePercentage >= 15)
                        mailedPercentage = 15;
                    else if (studentAbsencePercentage >= 10)
                        mailedPercentage = 10;
                    if (mailedPercentage != 0)
                    {
                        string studentEmailAddress = "";
                        sqlConnection.Open();
                        if (sqlConnection.State == System.Data.ConnectionState.Open)
                        {
                            //get email address for the particular student
                            string sqlStatement = "SELECT * FROM Students WHERE University_ID='" + studentUniversity_ID + "'";
                            MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);
                            MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                            if (sqlDataReader.Read())
                            {
                                try
                                {
                                    studentEmailAddress = sqlDataReader.GetString("Email");
                                }
                                finally
                                {
                                    sqlDataReader.Close();
                                    sqlConnection.Close();
                                }
                            }
                        }

                        try
                        {
                            MailMessage mailMessage = new MailMessage();
                            SmtpClient smtpClient = new SmtpClient(Form1.SMTP_SERVER_URL_ADDRESS);
                            mailMessage.From = new MailAddress(Form1.CLIENT_EMAIL);
                            mailMessage.To.Add(studentEmailAddress);
                            mailMessage.CC.Add(User.TEACHER_EMAIL);
                            mailMessage.Subject = "Absence Percentage Notice " + mailedPercentage + "%";
                            mailMessage.Body = "Student with University_ID: " + row.ItemArray[0].ToString() + " have exceeded " + mailedPercentage + "% of total lectures";
                            mailMessage.IsBodyHtml = false;
                            smtpClient.Port = 587;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.Credentials = new System.Net.NetworkCredential(Form1.CLIENT_EMAIL, Form1.CLIENT_EMAIL_PASS);
                            smtpClient.EnableSsl = true;
                            smtpClient.Timeout = 20000;
                            smtpClient.Send(mailMessage);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            // Restarts the timer
            EventTimer.Enabled = true;
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            if (loginID_textBox.Text == "")
                loginID_errorProvider.SetError(loginID_textBox, "Please Enter Username");
            else
            {
                if (loginPass_textBox.Text == "")
                    loginPass_errorProvider.SetError(loginPass_textBox, "Please Enter Password");
                else
                {
                    if (loginID_textBox.Text == User.TEACHER_ID && loginPass_textBox.Text == User.TEACHER_PASSWORD)
                    {
                        //setup teacher specifics
                        user = new User(User.TEACHER_ID, User.TEACHER_NAME, User.TEACHER_EMAIL, User.TEACHER_SERIAL_NUMBER, User.TEACHER_PHONE_NUMBER, User.TEACHER_PASSWORD);

                        //setup left panel
                        info_label.Text = "My account (Teacher):";
                        info_richTextBox.Text =
                            "ID: " + user.ID + "\n\n"
                            + "Name: " + user.Name + "\n\n"
                            + "Email: " + user.Email + "\n\n"
                            + "Phone #: " + user.Phone_number + "\n";
                        today_dateTimePicker.Value = DateTime.Now;
                        todayColumnString = today_dateTimePicker.Value.Day + "/" + today_dateTimePicker.Value.Month + "/" + today_dateTimePicker.Value.Year;

                        //setup right panel
                        button1.Text = "Skip Day";
                        button1.Show();
                        button2.Text = "Show All Absences";
                        button2.Show();
                        button3.Text = "Check All Students' Information";
                        button3.Show();
                        button4.Text = "Edit Absences";
                        button4.Show();
                        button5.Text = "Change My Account Information";
                        button5.Show();
                        button6.Hide();

                        //show teacher view
                        panel1_leftPanel.Hide();
                        panel2_leftPanel.Show();
                        panel1_rightPanel.Hide();
                        panel2_rightPanel.Show();
                    }
                    else
                    {
                        string studentUniversityID;
                        sqlConnection.Open();
                        if (sqlConnection.State == System.Data.ConnectionState.Open)
                        {
                            string sqlStatement = "SELECT * FROM Students WHERE University_ID='" + loginID_textBox.Text + "' AND Pass_Word='" + loginPass_textBox.Text + "'";
                            MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);
                            MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                            if (!sqlDataReader.Read())
                            {
                                //sql statement with incorrect credentials invoked
                                MessageBox.Show("login Error!", login_button.Text);
                                sqlDataReader.Close();
                                sqlConnection.Close();
                            }
                            else
                            {
                                try
                                {
                                    //setup student specifics
                                    studentUniversityID = sqlDataReader.GetString("University_ID");
                                    user = new User(studentUniversityID, sqlDataReader.GetString("Student_Name"), sqlDataReader.GetString("Email"), sqlDataReader.GetInt32("Serial_Number"), sqlDataReader.GetString("Phone_Number"), sqlDataReader.GetString("Pass_Word"));
                                }
                                finally
                                {
                                    sqlDataReader.Close();
                                    sqlConnection.Close();
                                }

                                if (student_exeeded_max(studentUniversityID))
                                {
                                    MessageBox.Show("You can't login due to exceeding the maximum allowed absences!\n", login_button.Text);
                                }
                                else
                                {
                                    //setup left panel
                                    info_label.Text = "My account (Student):";
                                    info_richTextBox.Text =
                                    "ID: " + user.ID + "\n\n"
                                    + "Name: " + user.Name + "\n\n"
                                    + "Email: " + user.Email + "\n\n"
                                    + "Phone #: " + user.Phone_number + "\n\n"
                                    + "Serial #: " + user.Serial_number + "\n";
                                    today_dateTimePicker.Value = DateTime.Now;
                                    todayColumnString = today_dateTimePicker.Value.Day + "/" + today_dateTimePicker.Value.Month + "/" + today_dateTimePicker.Value.Year;

                                    //setup right panel
                                    button1.Text = "Confirm Attendance";
                                    button1.Show();
                                    button2.Text = "Show My Absences";
                                    button2.Show();
                                    button3.Hide();
                                    button4.Hide();
                                    button5.Text = "Change My Account Information";
                                    button5.Show();
                                    button6.Hide();

                                    //show student view
                                    panel1_leftPanel.Hide();
                                    panel2_leftPanel.Show();
                                    panel1_rightPanel.Hide();
                                    panel2_rightPanel.Show();
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool student_exeeded_max(string studentUniversityID)
        {
            //return true on exceeding 20%
            if (student_absence_percentage(studentUniversityID) >= 20)
                return true;
            else
                return false;
        }

        //Return percentage of absences as integer out of 100
        private static int student_absence_percentage(string studentUniversityID)
        {
            int absentDaysCounter = 0;

            sqlConnection.Open();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                string sqlStatement = "SELECT * FROM Lecture_Attendance WHERE University_ID='" + studentUniversityID + "'";
                MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                try
                {
                    while (sqlDataReader.Read())
                    {
                        for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            if (String.IsNullOrWhiteSpace(sqlDataReader[i].ToString()))
                            {
                                absentDaysCounter++;
                            }
                    }
                }
                finally
                {
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return absentDaysCounter * (100 / Form1.TOTAL_NUMBER_OF_LECTURES);
        }

        private void loginPass_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login_button.PerformClick();
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            //user data cleanup
            user = null;

            //clear and show login view
            panel1_leftPanel.Show();
            loginID_textBox.Clear();
            loginPass_textBox.Clear();
            loginID_errorProvider.Clear();
            loginPass_errorProvider.Clear();
            panel2_leftPanel.Hide();
            panel1_rightPanel.Show();
            panel2_rightPanel.Hide();
            loginID_textBox.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (user.Serial_number == User.TEACHER_SERIAL_NUMBER)
            {
                //teacher button1.Text = "Skip Day";

                // Create a popup with a date picker and two buttons to use as the accept and cancel buttons.
                Form popupForm = new Form();
                Button buttonOK = new Button();
                Button buttonCancel = new Button();
                DateTimePicker picker = new DateTimePicker();
                picker.Location = new Point(30, 10);
                buttonOK.Text = "OK";
                buttonOK.Location = new Point(50, 200);
                buttonCancel.Text = "Cancel";
                buttonCancel.Location
                   = new Point(buttonOK.Left + buttonCancel.Width + 30, buttonOK.Top);
                buttonOK.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;
                popupForm.Text = button1.Text;

                popupForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                popupForm.AcceptButton = buttonOK;
                popupForm.CancelButton = buttonCancel;
                popupForm.StartPosition = FormStartPosition.CenterScreen;

                popupForm.Controls.Add(buttonOK);
                popupForm.Controls.Add(buttonCancel);
                popupForm.Controls.Add(picker);

                popupForm.ShowDialog();

                if (popupForm.DialogResult == DialogResult.OK)
                {
                    sqlConnection.Open();
                    if (sqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        //insert current time stamp on todays date column for all students
                        string skipDayColumnString = picker.Value.Day + "/" + picker.Value.Month + "/" + picker.Value.Year;
                        string sqlStatement = "UPDATE Lecture_Attendance SET `" + skipDayColumnString + "`='" + DateTime.Now.ToShortTimeString() + "'";
                        MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);

                        try
                        {
                            sqlCommand.ExecuteNonQuery();
                            MessageBox.Show("Skip Confirmed", buttonOK.Text);
                        }
                        catch (MySqlException ex)
                        {
                            //to add today column in the database
                            string alterSqlStatement = "ALTER TABLE Lecture_Attendance ADD `" + skipDayColumnString + "` VARCHAR(8)";
                            MySqlCommand alterSqlCommand = new MySqlCommand(alterSqlStatement, sqlConnection);
                            alterSqlCommand.ExecuteNonQuery();
                            if (sqlCommand.ExecuteNonQuery() > 0)
                                MessageBox.Show("Skip Confirmed", buttonOK.Text);
                            else
                                MessageBox.Show(ex.Message, buttonOK.Text);
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }
                    }
                    popupForm.Dispose();
                }
                else
                {
                    // the Cancel button was clicked.
                    popupForm.Dispose();
                }
            }
            else
            {
                //student button1.Text = "Confirm Attendance";

                //if time is late show msg else confirm attendance
                if (DateTime.Now > lectureLateTime)
                {
                    MessageBox.Show("You Are Late!", button1.Text);
                }
                else
                {
                    if (DateTime.Now < lectureStartTime)
                    {
                        //return after lecture starts
                        MessageBox.Show("You Are Too Early!\n\nPlease reconfirm attendance when lecture starts", button1.Text);
                    }
                    else
                    {
                        sqlConnection.Open();
                        if (sqlConnection.State == System.Data.ConnectionState.Open)
                        {
                            //insert current time stamp on todays date column and only over null value to keep first time stamp if multiple
                            string sqlStatement = "UPDATE Lecture_Attendance SET `" + todayColumnString + "` ='" + DateTime.Now.ToShortTimeString() + "' WHERE University_ID='" + user.ID + "' AND `" + todayColumnString + "` IS NULL OR `" + todayColumnString + "` = ''";
                            MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);

                            try
                            {
                                sqlCommand.ExecuteNonQuery();
                                MessageBox.Show("Attendance Confirmed", button1.Text);
                            }
                            catch (MySqlException ex)
                            {
                                //to add today column in the database
                                string alterSqlStatement = "ALTER TABLE Lecture_Attendance ADD `" + todayColumnString + "` VARCHAR(8)";
                                MySqlCommand alterSqlCommand = new MySqlCommand(alterSqlStatement, sqlConnection);
                                alterSqlCommand.ExecuteNonQuery();
                                if (sqlCommand.ExecuteNonQuery() == 1)
                                    MessageBox.Show("Attendance Confirmed", button1.Text);
                                else
                                    MessageBox.Show(ex.Message, button1.Text);
                            }
                            finally
                            {
                                sqlConnection.Close();
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (user.Serial_number == User.TEACHER_SERIAL_NUMBER)
            {
                //teacher button2.Text = "Show All Absences";
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    //return all rows
                    string sqlStatement = "SELECT * FROM Lecture_Attendance";
                    MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);
                    MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(sqlStatement, sqlConnection);
                    try
                    {
                        MySqlCommandBuilder sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);
                        dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        dataGridView1.ReadOnly = true;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.AllowUserToDeleteRows = false;
                        dataGridView1.DataSource = dataSet.Tables[0];
                        dataGridView1.Columns["University_ID"].DefaultCellStyle.BackColor = Color.AliceBlue;
                        dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
                        dataGridView1.Show();
                        label1_rightPanel.Text = button2.Text + ":";
                        label1_rightPanel.Show();
                        button6.Text = "<-- Go Back";
                        button6.Show();
                        button5.Hide();
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
            else
            {
                //student button2.Text = "Show My Absences";
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    //return all row entries with column name for current student
                    string sqlStatement = "SELECT * FROM Lecture_Attendance WHERE University_ID='" + user.ID + "'";
                    MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);
                    MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    try
                    {
                        int listCounter = 0;
                        string printString = "";
                        while (sqlDataReader.Read())
                        {
                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                                if (String.IsNullOrWhiteSpace(sqlDataReader[i].ToString()))
                                {
                                    listCounter++;
                                    printString += listCounter + ") " + sqlDataReader.GetName(i) + "\n";
                                }
                            if (String.IsNullOrWhiteSpace(printString))
                                printString = "Student: " + user.Name + " was never absent";
                            else // student sees a list of absences and percentage of total
                                printString = "Student: " + user.Name + " was absent in:\n\n" + printString + "Percentage: " + listCounter * (100 / Form1.TOTAL_NUMBER_OF_LECTURES) + "%";

                            MessageBox.Show(printString, button2.Text);
                        }
                    }
                    finally
                    {
                        sqlDataReader.Close();
                        sqlConnection.Close();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (user.Serial_number == User.TEACHER_SERIAL_NUMBER)
            {
                //teacher button3.Text = "Check All Students' Information";
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    //return all rows
                    string sqlStatement = "SELECT * FROM Students";
                    MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);
                    MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(sqlStatement, sqlConnection);
                    try
                    {
                        MySqlCommandBuilder sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);
                        dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        dataGridView1.ReadOnly = true;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.AllowUserToDeleteRows = false;
                        dataGridView1.DataSource = dataSet.Tables[0];
                        dataGridView1.Columns["University_ID"].DefaultCellStyle.BackColor = Color.AliceBlue;
                        dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
                        dataGridView1.Show();
                        label1_rightPanel.Text = button3.Text + ":";
                        label1_rightPanel.Show();
                        button6.Text = "<-- Go Back";
                        button6.Show();
                        button5.Hide();
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (user.Serial_number == User.TEACHER_SERIAL_NUMBER)
            {
                //teacher button4.Text = "Edit Absences";
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    //return all rows
                    string sqlStatement = "SELECT * FROM Lecture_Attendance";
                    MySqlCommand sqlCommand = new MySqlCommand(sqlStatement, sqlConnection);
                    MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(sqlStatement, sqlConnection);
                    try
                    {
                        MySqlCommandBuilder sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);
                        dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        dataGridView1.ReadOnly = false;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.AllowUserToDeleteRows = false;
                        dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
                        dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
                        dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
                        dataGridView1.DataSource = dataSet.Tables[0];
                        dataGridView1.Columns["University_ID"].ReadOnly = true;
                        dataGridView1.Columns["University_ID"].DefaultCellStyle.BackColor = Color.LemonChiffon;
                        dataGridView1.Show();
                        label1_rightPanel.Text = button4.Text + ":";
                        label1_rightPanel.Show();
                        button6.Text = "<-- Go Back";
                        button6.Show();
                        button5.Hide();
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dataGridView1.CancelEdit();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //know current position in the grid and save the new value to database
            string id = dataSet.Tables[0].Rows[e.RowIndex]["University_ID"] + "";
            string col = dataSet.Tables[0].Columns[e.ColumnIndex].ColumnName;
            string data = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "";

            string sql = String.Format("UPDATE `Lecture_Attendance` SET `{0}` = '{1}' WHERE University_ID = {2};", col, data, id);

            sqlConnection.Open();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        // Changes the color of cells of absent
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(e.Value.ToString()))
                e.CellStyle.BackColor = Color.MistyRose;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //teacher button5.Text = "Change My Account Information";
            //student button5.Text = "Change My Account Information";
            // Create a popup with labels and textboxes and two buttons to use as the save and discard buttons.
            Form popupForm = new Form();
            Button buttonSave = new Button();
            Button buttonCancel = new Button();
            buttonSave.Text = "Save";
            buttonSave.Location = new Point(50, 200);
            buttonCancel.Text = "Discard";
            buttonCancel.Location
               = new Point(buttonSave.Left + buttonCancel.Width + 30, buttonSave.Top);
            buttonSave.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            popupForm.Text = button5.Text;

            popupForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            popupForm.AcceptButton = buttonSave;
            popupForm.CancelButton = buttonCancel;
            popupForm.StartPosition = FormStartPosition.CenterScreen;
            popupForm.Controls.Add(buttonSave);
            popupForm.Controls.Add(buttonCancel);

            //dynamically creating labels and textboxes to hold and edit info
            int n = 4;//count of editable info
            Label[] labels = new Label[n];
            TextBox[] textBoxes = new TextBox[n];
            for (int i = 0; i < n; i++)
            {
                labels[i] = new Label();
                textBoxes[i] = new TextBox();

                //Position on screen
                labels[i].Left = 10;
                labels[i].Top = (i + 1) * 30;
                textBoxes[i].Left = 80;
                textBoxes[i].Top = (i + 1) * 30;
                textBoxes[i].Width += 80;
            }
            //fill Texts
            labels[0].Text = "Name: ";
            textBoxes[0].Text = user.Name;
            labels[1].Text = "Email: ";
            textBoxes[1].Text = user.Email;
            labels[2].Text = "Password: ";
            textBoxes[2].Text = user.Password;
            labels[3].Text = "Phone #: ";
            textBoxes[3].Text = user.Phone_number;
            for (int i = 0; i < n; i++)
            {
                popupForm.Controls.Add(textBoxes[i]);
                popupForm.Controls.Add(labels[i]);
            }

            popupForm.ShowDialog();
            if (popupForm.DialogResult == DialogResult.OK)
            {
                //must be in the same order as fill Texts above

                user.Name = textBoxes[0].Text;
                user.Email = textBoxes[1].Text;
                user.Password = textBoxes[2].Text;
                user.Phone_number = textBoxes[3].Text;

                if (user.Serial_number == User.TEACHER_SERIAL_NUMBER)
                {
                    //teacher so write to both User.STATIC_VARIABLE and the ini file
                    User.TEACHER_NAME = user.Name;
                    User.TEACHER_EMAIL = user.Email;
                    User.TEACHER_PASSWORD = user.Password;
                    User.TEACHER_PHONE_NUMBER = user.Phone_number;
                    SettingsIni.Write("Name", User.TEACHER_NAME, "Teacher");
                    SettingsIni.Write("Email", User.TEACHER_EMAIL, "Teacher");
                    SettingsIni.Write("Password", User.TEACHER_PASSWORD, "Teacher");
                    SettingsIni.Write("Phone_number", User.TEACHER_PHONE_NUMBER, "Teacher");

                    //refresh left panel data according to new ones
                    info_richTextBox.Text =
                            "ID: " + user.ID + "\n\n"
                            + "Name: " + user.Name + "\n\n"
                            + "Email: " + user.Email + "\n\n"
                            + "Phone #: " + user.Phone_number + "\n";
                }
                else
                {
                    //student so write back to database
                    string sql = String.Format("UPDATE `students` SET `{0}`='{1}', `{2}`='{3}', `{4}`='{5}', `{6}`='{7}' WHERE `{8}`='{9}'"
                        , "Student_Name", user.Name, "Email", user.Email, "Phone_Number", user.Phone_number, "Pass_Word", user.Password, "University_ID", user.ID);
                    sqlConnection.Open();
                    if (sqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        try
                        {
                            MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);
                            sqlCommand.ExecuteNonQuery();
                        }
                        finally
                        {
                            //refresh left panel data according to new ones and close connection to database
                            info_richTextBox.Text =
                            "ID: " + user.ID + "\n\n"
                            + "Name: " + user.Name + "\n\n"
                            + "Email: " + user.Email + "\n\n"
                            + "Phone #: " + user.Phone_number + "\n\n"
                            + "Serial #: " + user.Serial_number + "\n";
                            sqlConnection.Close();
                        }
                    }
                }
                popupForm.Dispose();
            }
            else
            {
                // the Cancel button was clicked.
                popupForm.Dispose();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (user.Serial_number == User.TEACHER_SERIAL_NUMBER)
            {
                if (dataGridView1.Visible)
                {
                    //teacher button6.Text = "<-- Go Back";
                    dataGridView1.Hide();
                    label1_rightPanel.Hide();
                    button6.Hide();
                    button5.Show();
                }
            }
        }
    }
}