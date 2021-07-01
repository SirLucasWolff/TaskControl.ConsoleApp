using System;
using System.Data.SqlClient;

namespace TaskControl.ConsoleApp
{
    internal class DBContactsWithCompromise
    {
        public static string Subject { get; private set; }

        private static string id;

        public static string Name { get; private set; }
        public static string Email { get; private set; }
        public static int Phone { get; private set; }
        public static string Firm { get; private set; }
        public static string Position { get; private set; }
        public static string NameofContact { get; set; }
        public static object Compromise { get; set; }
        public static object Status { get; set; }
        public static string OptionName { get; set; }
        public static int Option { get; private set; }
        public static object Pending { get; private set; }


        internal static void Add()
        {
           

            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandGet = new SqlCommand();
            commandGet.Connection = connectionWithContact;

            string sqlGet =
                @"SELECT * FROM DBCONTACTS
                   ";

            sqlGet +=
                @"SELECT SCOPE_IDENTITY();";

            commandGet.CommandText = sqlGet;

            using (SqlDataReader oReader = commandGet.ExecuteReader())
            {
                while (oReader.Read())
                {
                    id = oReader["ID"].ToString();
                    Name = oReader["Name"].ToString();
                    Email = oReader["Email"].ToString();
                    Phone = (int)oReader["Phone"];
                    Firm = oReader["Firm"].ToString();
                    Position = oReader["Position"].ToString();
                    Console.ForegroundColor = ConsoleColor.Yellow;

                }
            }
            connectionWithContact.Close();

            Console.Clear();
            Console.WriteLine("Name of contact:");
            NameofContact = Console.ReadLine();

            if (NameofContact == Name)
            {
                GetCompromise();
            }
            if (NameofContact != Name)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Contact not found!");
                Console.ReadLine();
                Console.ResetColor();
                Console.WriteLine("1 - Try again");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 1)
                {
                    Add();
                }
                if (Option == 2)
                {
                    ScreenCompromise.screenCompromise();
                }
                ScreenCompromise.screenCompromise();
            }
        }

        private static void GetCompromise()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromise = new SqlConnection();
            connectionWithCompromise.ConnectionString = AdressDBtask;

            connectionWithCompromise.Open();

            SqlCommand commandGet = new SqlCommand();
            commandGet.Connection = connectionWithCompromise;

            string sqlGet =
                @"SELECT * FROM DBCOMPROMISE
                   ";

            sqlGet +=
                @"SELECT SCOPE_IDENTITY();";

            commandGet.CommandText = sqlGet;

            using (SqlDataReader oReader = commandGet.ExecuteReader())
            {
                while (oReader.Read())
                {
                    id = oReader["ID"].ToString();
                    Subject = oReader["Subject"].ToString();
                }
            }
            connectionWithCompromise.Close();
            Console.ResetColor();


            Compromise = (Subject);


            InsertCC();
        }

        public static void CompromiseAdd()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromise = new SqlConnection();
            connectionWithCompromise.ConnectionString = AdressDBtask;

            connectionWithCompromise.Open();

            SqlCommand commandGet = new SqlCommand();
            commandGet.Connection = connectionWithCompromise;

            string sqlGet =
                @"SELECT * FROM DBCOMPROMISE
                   ";

            sqlGet +=
                @"SELECT SCOPE_IDENTITY();";

            commandGet.CommandText = sqlGet;

            using (SqlDataReader oReader = commandGet.ExecuteReader())
            {
                while (oReader.Read())
                {
                    id = oReader["ID"].ToString();
                    Subject = oReader["Subject"].ToString();


                }
            }
            connectionWithCompromise.Close();
        }
    

        private static void InsertCC()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contact found!");
            Console.Clear();
            Console.ReadLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("!Contact vinculed with your compromise");
            Console.ReadLine();
            Console.ResetColor();
            Status = ("Pending");
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromise = new SqlConnection();
            connectionWithCompromise.ConnectionString = AdressDBtask;

            connectionWithCompromise.Open();

            SqlCommand commandInsert = new SqlCommand();
            commandInsert.Connection = connectionWithCompromise;

            string sqlInsert =
            @"INSERT INTO [TABLE] 
                (
                 [Name],
                 [Compromise],
                 [Status]
                )
                VALUES 
                (
                 @Name,
                 @Compromise,
                 @Status
                )";

            sqlInsert +=
                @"SELECT SCOPE_IDENTITY();";

            commandInsert.CommandText = sqlInsert;

            commandInsert.Parameters.AddWithValue("Name", NameofContact);
            commandInsert.Parameters.AddWithValue("Compromise", Compromise);
            commandInsert.Parameters.AddWithValue("Status", Status);


            object id = commandInsert.ExecuteScalar();

            object Id = Convert.ToInt32(id);


            connectionWithCompromise.Close();
            ScreenCompromise.screenCompromise();
        }
    }
}