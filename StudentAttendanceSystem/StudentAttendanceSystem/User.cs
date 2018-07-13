namespace StudentAttendanceSystem
{
    internal class User
    {
        //Initial Values for teacher (system admin)
        public static string TEACHER_EMAIL = "TestEmailForSPTeacher@mail.com";

        public static string TEACHER_EMAIL_PASS = "I4mThePass/Teach";
        public static string TEACHER_ID = "teacher";
        public static string TEACHER_NAME = "This Is The Name Of Teacher";
        public static string TEACHER_PASSWORD = "teacher";
        public static string TEACHER_PHONE_NUMBER = "077-teach-010";
        public static int TEACHER_SERIAL_NUMBER = 0;

        private string email;
        private string id;
        private string name;
        private string password;
        private string phone_number;
        private int serial_number;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Phone_number
        {
            get { return phone_number; }
            set { phone_number = value; }
        }

        public int Serial_number
        {
            get { return serial_number; }
            set { serial_number = value; }
        }

        public User(string id, string name, string email, int serial_number, string phone_number, string password)
        {
            this.Email = email;
            this.ID = id;
            this.Name = name;
            this.Password = password;
            this.Phone_number = phone_number;
            this.Serial_number = serial_number;
        }
    }
}