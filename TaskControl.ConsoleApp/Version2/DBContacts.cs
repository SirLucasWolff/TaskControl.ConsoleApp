using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControl.ConsoleApp
{
    class DBContacts
    {
        public static string Name;
        public static string Email;
        public static int Phone;
        public static string Firm;
        public static string Position;
        public static int Option;
        public static string id { get; set; }

        internal static void DBInsert()
        {
            Console.Clear();
            Console.WriteLine("New name:");
            Name = Console.ReadLine();
            Console.Clear();

            id = "55";

            Console.WriteLine("New email:");
            Email = Console.ReadLine();
            Console.Clear();

            if (Email.Length < 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid");
                Console.ResetColor();
                Console.ReadLine();
            }

            Console.WriteLine("New phone:");
            Phone = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("New firm:");
            Firm = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("New position:");
            Position = Console.ReadLine();
            Console.Clear();

            DBContactsInsert();
        }

        private static void DBContactsInsert()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandInsert = new SqlCommand();
            commandInsert.Connection = connectionWithContact;

            string sqlInsert =
                @"INSERT INTO DBCONTACTS 
                (
                 [Name],
                 [Email],
                 [Phone],
                 [Firm],
                 [Position]
                )
                VALUES 
                (
                 @Name,
                 @Email,
                 @Phone,
                 @Firm,
                 @Position
                )";

            sqlInsert +=
                @"SELECT SCOPE_IDENTITY();";

            commandInsert.CommandText = sqlInsert;

            commandInsert.Parameters.AddWithValue("NAME", Name);
            commandInsert.Parameters.AddWithValue("EMAIL", Email);
            commandInsert.Parameters.AddWithValue("PHONE", Phone);
            commandInsert.Parameters.AddWithValue("FIRM", Firm);
            commandInsert.Parameters.AddWithValue("POSITION", Position);

            object id = commandInsert.ExecuteScalar();

            object Id = Convert.ToInt32(id);

            connectionWithContact.Close();

            ScreenStart.screenStart();
        }

        internal static void DBView()
        {
            Console.Clear();
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
                    Console.WriteLine($"| ID: {id}  |NOME: {Name} | EMAIL: {Email} | PHONE: {Phone} | FIRM: {Firm} | POSITION: {Position} |");
                }
            }
            connectionWithContact.Close();
            Console.ResetColor();
            Console.ReadLine();
            ScreenContacts.screenContact();
        }

        internal static void DBEdit()
        {
            Console.Clear();
            ViewTable();
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithContact;

            string sqlEdit =
                @"UPDATE DBCONTACTS
                 SET
                 [Name] = @Name,
                 [Email] = @Email,
                 [Phone] = @Phone,
                 [Firm] =  @Firm,
                 [Position] = @Position
                 WHERE
                   [ID] = id";

            commandEdit.CommandText = sqlEdit;

            commandEdit.Parameters.AddWithValue("@Name", Name);
            commandEdit.Parameters.AddWithValue("@Email", Email);
            commandEdit.Parameters.AddWithValue("@Phone", Phone);
            commandEdit.Parameters.AddWithValue("@Firm", Firm);
            commandEdit.Parameters.AddWithValue("@Position", Position);

            Console.WriteLine("What you wish to edit?");
            Console.WriteLine("1 - Name");
            Console.WriteLine("2 - Email");
            Console.WriteLine("3 - Phone");
            Console.WriteLine("4 - Firm");
            Console.WriteLine("5 - Position");
            Option = Convert.ToInt32(Console.ReadLine());

            if (Option == 1)
            {
                Console.Clear();
                ViewTable();
                Console.WriteLine("Edit the name...");
                Name = Console.ReadLine();
                commandEdit.Parameters.AddWithValue("@Name", Name);
                DBContactsEditName();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    ScreenStart.screenStart();
                }
                if (Option == 1)
                {
                    DBEdit();

                }
            }
            if (Option == 2)
            {
                Console.Clear();
                Console.WriteLine("Edit the email...");
                Email = Console.ReadLine();
                commandEdit.Parameters.AddWithValue("EMAIL", Email);
                DBContactsEditEmail();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    Console.Clear();
                    ScreenStart.screenStart();
                }
                if (Option == 1)
                {
                    DBEdit();
                }
            }
            if (Option == 3)
            {
                Console.Clear();
                Console.WriteLine("Edit the Phone...");
                Phone = Convert.ToInt32(Console.ReadLine());
                commandEdit.Parameters.AddWithValue("PHONE", Phone);
                DBContactsEditPhone();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    Console.Clear();
                    ScreenStart.screenStart();
                }
                if (Option == 1)
                {
                    DBEdit();

                }
            }
            if (Option == 4)
            {
                Console.Clear();
                Console.WriteLine("Edit the firm...");
                Firm = Console.ReadLine();
                commandEdit.Parameters.AddWithValue("FIRM", Firm);
                DBContactsEditFirm();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    Console.Clear();
                    ScreenStart.screenStart();
                }
                if (Option == 1)
                {
                    DBEdit();
                }
            }
            if (Option == 5)
            {
                Console.Clear();
                Console.WriteLine("Edit the position...");
                Position = Console.ReadLine();
                commandEdit.Parameters.AddWithValue("POSITION", Position);
                DBContactsEditPosition();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    Console.Clear();
                    ScreenStart.screenStart();
                }
                if (Option == 1)
                {
                    DBEdit();
                }
            }

            ScreenStart.screenStart();
        }

        private static void DBContactsEditName()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithContact;
            ViewTable();
            Console.WriteLine("Put the ID to edit the contact:");

            id = Console.ReadLine();

            string sqlEdit =
                @"UPDATE DBCONTACTS
                 SET
                   [Name] = @Name
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;

            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Name", Name);

            commandEdit.ExecuteNonQuery();

            connectionWithContact.Close();
            Console.Clear();
            ScreenStart.screenStart();
        }

        private static void DBContactsEditEmail()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithContact;
            ViewTable();
            Console.WriteLine("Put the ID to edit the contact:");

            id = Console.ReadLine();

            string sqlEdit =
                @"UPDATE DBCONTACTS
                 SET
                   [Email] = @Email
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;

            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Email", Email);

            commandEdit.ExecuteNonQuery();

            connectionWithContact.Close();
            Console.Clear();
            ScreenStart.screenStart();
        }

        private static void DBContactsEditPhone()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithContact;

            Console.WriteLine("Put the ID to edit the contact:");

            id = Console.ReadLine();

            string sqlEdit =
                @"UPDATE DBCONTACTS
                 SET
                   [Phone] = @Phone
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;

            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Phone", Phone);

            commandEdit.ExecuteNonQuery();

            connectionWithContact.Close();
            Console.Clear();
            ScreenStart.screenStart();
        }

        private static void DBContactsEditFirm()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithContact;
            ViewTable();
            Console.WriteLine("Put the ID to edit the contact:");

            id = Console.ReadLine();

            string sqlEdit =
                @"UPDATE DBCONTACTS
                 SET
                   [Firm] = @Firm
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;

            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Firm", Firm);

            commandEdit.ExecuteNonQuery();

            connectionWithContact.Close();
            Console.Clear();
            ScreenStart.screenStart();
        }

        private static void DBContactsEditPosition()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithContact;
            ViewTable();
            Console.WriteLine("Put the ID to edit the contact:");

            id = Console.ReadLine();

            string sqlEdit =
                @"UPDATE DBCONTACTS
                 SET
                   [Position] = @Position
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;

            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Position", Position);

            commandEdit.ExecuteNonQuery();

            connectionWithContact.Close();
            Console.Clear();
            ScreenStart.screenStart();
        }

        private static void ViewTable()
        {
            Console.Clear();
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
                    Console.WriteLine($"| ID: {id}  |NOME: {Name} | EMAIL: {Email} | PHONE: {Phone} | FIRM: {Firm} | POSITION: {Position} |");
                }
            }
            connectionWithContact.Close();
            Console.ResetColor();
            Console.ReadLine();
        }

        internal static void DBDelete()
        {
            Console.Clear();
            ViewTable();
            Console.WriteLine("1 - Clear the list?");
            Console.WriteLine("2 - Delete with Id");
            Console.WriteLine("3 - Retur to Screen Contact");
            Option = Convert.ToInt32(Console.ReadLine());
            if (Option == 1)
            {
                Deleteall();
            }
            if (Option == 2)
            {
                Console.Clear();
                Console.WriteLine("Put the ID to delete the compromise:");
                id = Console.ReadLine();

                string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

                SqlConnection connectionWithContact = new SqlConnection();
                connectionWithContact.ConnectionString = AdressDBtask;

                connectionWithContact.Open();

                SqlCommand commandDelete = new SqlCommand();
                commandDelete.Connection = connectionWithContact;

                string sqlDelete =
                    @"DELETE FROM DBCONTACTS 
                    WHERE
                        [ID] = @id";

                commandDelete.CommandText = sqlDelete;
                commandDelete.Parameters.AddWithValue("@id", id);

                commandDelete.ExecuteNonQuery();

                connectionWithContact.Close();

            }
            if (Option == 3)
            {
                ScreenContacts.screenContact();
            }

            ScreenCompromise.screenCompromise();
        }

        private static void Deleteall()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandDelete = new SqlCommand();
            commandDelete.Connection = connectionWithContact;

            string sqlDelete =
                @"DELETE FROM DBCONTACTS 
                   ";

            commandDelete.CommandText = sqlDelete;


            commandDelete.ExecuteNonQuery();

            connectionWithContact.Close();
        }
    }
}
