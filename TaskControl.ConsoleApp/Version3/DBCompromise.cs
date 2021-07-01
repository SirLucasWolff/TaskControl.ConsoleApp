using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControl.ConsoleApp
{
    public static class DBCompromise
    {
        public static string Subject { get; set; }
        public static string Spot { get; set; }
        public static DateTime CompromiseDate { get; set; }
        public static string StartHour { get; set; }
        public static string EndHour { get; set; }
        public static string KindCompromise { get; set; }
        public static int Option { get; private set; }
        public static string id { get; set; }
        public static string NameofContact { get; private set; }
        public static string Compromise { get; private set; }
        public static string Status { get; private set; }
        public static DateTime Opti { get; private set; }
        public static DateTime Date { get; set; }
        public static int OptionView { get; private set; }

        public static void DBInsert()
        {
            Console.Clear();
            Console.WriteLine("New subject:");
            Subject = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("New spot:");
            Spot = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("New compromise date:");
            CompromiseDate = Convert.ToDateTime(Console.ReadLine());

            if (CompromiseDate.DayOfWeek == DayOfWeek.Saturday)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The dates for compromises only from Monday at Friday!");
                Console.ReadLine();
                Console.ResetColor();
                DBInsert();
            }
            if (CompromiseDate.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The dates for compromises only from Monday at Friday!");
                Console.ReadLine();
                Console.ResetColor();
                DBInsert();
            }

            Console.Clear();
            Console.WriteLine("New start hour:");
            StartHour = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("New end hour:");
            EndHour = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Select the kind of compromise");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1 - Remote");
            Console.WriteLine("2 - Classroom");
            Console.ResetColor();

            Option = Convert.ToInt32(Console.ReadLine());
            if (Option == 1)
            {
                Console.Clear();
                KindCompromise = "Remote";

                DBLink.DBInsert();
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Clear();
                Console.WriteLine("Link registed with succsses, don't forget your compromise at date:" + CompromiseDate);
                Console.ResetColor();
                Console.ReadLine();
            }
            if (Option == 2)
            {
                Console.Clear();
                KindCompromise = "Classroom";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Clear();
                Console.WriteLine("Compromise save, don't forget your compromise at date:" + CompromiseDate);
                Console.ResetColor();
                Console.ReadLine();
            }

            if (CompromiseDate <= DateTime.Now)
            {
                string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

                SqlConnection connectionWithCompromise = new SqlConnection();
                connectionWithCompromise.ConnectionString = AdressDBtask;

                connectionWithCompromise.Open();

                SqlCommand commandInsert = new SqlCommand();
                commandInsert.Connection = connectionWithCompromise;

                string sqlInsert = @"INSERT INTO DBCOMPROMISE 
                (
                 [Subject],
                 [Spot],
                 [Compromise Date],
                 [Start hour],
                 [End hour],
                 [Kind of compromise]
                )
                VALUES 
                (
                 @Subject,
                 @Spot,
                 @CompromiseDate,
                 @Starthour,
                 @Endhour,
                 @Kindofcompromise
                )";

                sqlInsert +=
                    @"SELECT SCOPE_IDENTITY();";

                commandInsert.CommandText = sqlInsert;

                commandInsert.Parameters.AddWithValue("Subject", Subject);
                commandInsert.Parameters.AddWithValue("Spot", Spot);
                commandInsert.Parameters.AddWithValue("CompromiseDate", CompromiseDate);
                commandInsert.Parameters.AddWithValue("Starthour", StartHour);
                commandInsert.Parameters.AddWithValue("Endhour", EndHour);
                commandInsert.Parameters.AddWithValue("Kindofcompromise", KindCompromise);

                object id = commandInsert.ExecuteScalar();

                object Id = Convert.ToInt32(id);

                Console.WriteLine(commandInsert.ExecuteNonQuery());

                connectionWithCompromise.Close();
            }
            if (CompromiseDate >= DateTime.Now)
            {
                string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

                SqlConnection connectionWithCompromise = new SqlConnection();
                connectionWithCompromise.ConnectionString = AdressDBtask;

                connectionWithCompromise.Open();

                SqlCommand commandInsert = new SqlCommand();
                commandInsert.Connection = connectionWithCompromise;

                string sqlInsert =
                @"INSERT INTO DBCOMPROMISEFUTURE 
                (
                 [Subject],
                 [Spot],
                 [Compromise Date],
                 [Start hour],
                 [End hour],
                 [Kind of compromise]
                )
                VALUES 
                (
                 @Subject,
                 @Spot,
                 @CompromiseDate,
                 @Starthour,
                 @Endhour,
                 @Kindofcompromise
                )";

                sqlInsert +=
                    @"SELECT SCOPE_IDENTITY();";

                commandInsert.CommandText = sqlInsert;

                commandInsert.Parameters.AddWithValue("Subject", Subject);
                commandInsert.Parameters.AddWithValue("Spot", Spot);
                commandInsert.Parameters.AddWithValue("CompromiseDate", CompromiseDate);
                commandInsert.Parameters.AddWithValue("Starthour", StartHour);
                commandInsert.Parameters.AddWithValue("Endhour", EndHour);
                commandInsert.Parameters.AddWithValue("Kindofcompromise", KindCompromise);

                object id = commandInsert.ExecuteScalar();

                object Id = Convert.ToInt32(id);

                Console.WriteLine(commandInsert.ExecuteNonQuery());

                connectionWithCompromise.Close();
            }

            Console.Clear();
            Console.WriteLine("Your compromise is with someone of contacts?");
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");
            Option = Convert.ToInt32(Console.ReadLine());

            if (Option == 1)
            {
                DBContactsWithCompromise.Add();
            }
            ScreenCompromise.screenCompromise();
        }

        

        internal static void DBView()
        {
            Console.Clear();
            Console.WriteLine("1 - Currently compromises");
            Console.WriteLine("2 - Past compromises");
            Console.WriteLine("3 - Future compromises");
            OptionView = Convert.ToInt32(Console.ReadLine());

            if (OptionView == 1)
            {
                Console.Clear();
                DBCompromise.ViewDB();
            }
            if (OptionView == 2)
            {
                Console.Clear();
                DBCompromise.PastCompromise();
            
            }
            if(OptionView == 3)
            {
                Console.Clear();
                DBCompromise.FutureCompromises();
            }

            Console.Clear();
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
                    Spot = oReader["Spot"].ToString();
                    StartHour = oReader["Start Hour"].ToString();
                    EndHour = oReader["End Hour"].ToString();
                    KindCompromise = oReader["Kind of Compromise"].ToString();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"| ID: {id}  | SUBJECT: {Subject} | SPOT: {Spot} | START HOUR: {StartHour} | END HOUR: {EndHour} | KIND OF COMPROMISE: {KindCompromise} |");
                }
            }
            connectionWithCompromise.Close();
            Console.ResetColor();
            Console.ReadLine();
            ScreenCompromise.screenCompromise();
        }

        private static void FutureCompromises()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromiseFuture = new SqlConnection();
            connectionWithCompromiseFuture.ConnectionString = AdressDBtask;

            connectionWithCompromiseFuture.Open();

            SqlCommand commandGet = new SqlCommand();
            commandGet.Connection = connectionWithCompromiseFuture;

            string sqlGet =
                @"SELECT * FROM DBCOMPROMISEFUTURE
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
                    Spot = oReader["Spot"].ToString();
                    CompromiseDate = (DateTime)oReader["compromise Date"];
                    StartHour = oReader["Start Hour"].ToString();
                    EndHour = oReader["End Hour"].ToString();
                    KindCompromise = oReader["Kind of Compromise"].ToString();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"| ID: {id}  | SUBJECT: {Subject} | SPOT: {Spot} | COMPROMISE DATE  {CompromiseDate} | START HOUR: {StartHour} | END HOUR: {EndHour} | KIND OF COMPROMISE: {KindCompromise} |");
                }
            }
            connectionWithCompromiseFuture.Close();
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("1 - You wish see the link of remote compromises?");
            Console.WriteLine("2 - Exit");
            Option = Convert.ToInt32(Console.ReadLine());
            if (Option == 1)
            {
                Console.Clear();
                DBLink.ViewTable();
            }
            if (Option == 2)
            {
                Console.Clear();
                ScreenCompromise.screenCompromise();
            }
            ScreenCompromise.screenCompromise();
        }
        
        

        private static void PastCompromise()
        {
            Console.Clear();
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
                    Spot = oReader["Spot"].ToString();
                    CompromiseDate = (DateTime)oReader["compromise Date"];
                    StartHour = oReader["Start Hour"].ToString();
                    EndHour = oReader["End Hour"].ToString();
                    KindCompromise = oReader["Kind of Compromise"].ToString();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"| ID: {id}  | SUBJECT: {Subject} | SPOT: {Spot} | COMPROMISE DATE  {CompromiseDate} | START HOUR: {StartHour} | END HOUR: {EndHour} | KIND OF COMPROMISE: {KindCompromise} |");
                }
            }
            connectionWithCompromise.Close();
            Console.ResetColor();
            Console.ReadLine();
            ScreenCompromise.screenCompromise();
        }

        public static void DeleteAll()
        {
            Console.Clear();
            ViewDeleteID();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Put the currently date for a validation");
            Console.ResetColor();
            Date = Convert.ToDateTime(Console.ReadLine());

            if (Date <= CompromiseDate)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please wait for your compromise!");
                Console.ResetColor();
                Console.ReadLine();
                DeleteAll();
            }
            if (Date >= CompromiseDate)
            {
                string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

                SqlConnection connectionWithCompromise = new SqlConnection();
                connectionWithCompromise.ConnectionString = AdressDBtask;

                connectionWithCompromise.Open();

                SqlCommand commandDelete = new SqlCommand();
                commandDelete.Connection = connectionWithCompromise;

                string sqlDelete =
                    @"DELETE FROM DBCOMPROMISE 
                    WHERE
                        [Compromise Date] = @CompromiseDate";

                commandDelete.CommandText = sqlDelete;
                commandDelete.Parameters.AddWithValue("@CompromiseDate", CompromiseDate <= DateTime.Now);

                commandDelete.ExecuteNonQuery();

                connectionWithCompromise.Close();

                ScreenCompromise.screenCompromise();
            }

            ScreenCompromise.screenCompromise();
        }

        public static void ViewDeleteID()
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
                    Spot = oReader["Spot"].ToString();
                    //CompromiseDate = (DateTime)oReader["compromise Date"];
                    StartHour = oReader["Start Hour"].ToString();
                    EndHour = oReader["End Hour"].ToString();
                    KindCompromise = oReader["Kind of Compromise"].ToString();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"| ID: {id}  | SUBJECT: {Subject} | SPOT: {Spot} | COMPROMISE DATE  {CompromiseDate} | START HOUR: {StartHour} | END HOUR: {EndHour} | KIND OF COMPROMISE: {KindCompromise} |");
                }
            }
            connectionWithCompromise.Close();
            Console.ResetColor();
            Console.ReadLine();
        }

        private static void ViewDB()
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
                    Spot = oReader["Spot"].ToString();
                    //CompromiseDate = (DateTime)oReader["compromise Date"];
                    StartHour = oReader["Start Hour"].ToString();
                    EndHour = oReader["End Hour"].ToString();
                    KindCompromise = oReader["Kind of Compromise"].ToString();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"| ID: {id}  | SUBJECT: {Subject} | SPOT: {Spot} | COMPROMISE DATE  {CompromiseDate} | START HOUR: {StartHour} | END HOUR: {EndHour} | KIND OF COMPROMISE: {KindCompromise} |");
                }
            }
            connectionWithCompromise.Close();
            Console.ResetColor();
            Console.ReadLine();
            ScreenCompromise.screenCompromise();
        }

        internal static void DBDelete()
        {
            Console.WriteLine("1 - Delete past compromises");
            Console.WriteLine("2 - Delete compromises for Id");
            Option = Convert.ToInt32(Console.ReadLine());
            if (Option == 1)
            {
                DeleteAll();
            }
            if (Option == 2)
            {
                Console.Clear();
                ViewDeleteID();
                Console.WriteLine("Put the ID to delete the compromise:");
                id = Console.ReadLine();

                string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

                SqlConnection connectionWithCompromise = new SqlConnection();
                connectionWithCompromise.ConnectionString = AdressDBtask;

                connectionWithCompromise.Open();

                SqlCommand commandDelete = new SqlCommand();
                commandDelete.Connection = connectionWithCompromise;

                string sqlDelete =
                    @"DELETE FROM DBCOMPROMISE 
                    WHERE
                        [ID] = @id";

                commandDelete.CommandText = sqlDelete;
                commandDelete.Parameters.AddWithValue("@id", id);

                commandDelete.ExecuteNonQuery();

                connectionWithCompromise.Close();
                Console.Clear();
                ScreenCompromise.screenCompromise();
            }
           
        }

        internal static void DBEdit()
        {
            Console.Clear();
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromise = new SqlConnection();
            connectionWithCompromise.ConnectionString = AdressDBtask;

            connectionWithCompromise.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithCompromise;

            string sqlEdit =
            @"UPDATE DBCOMPROMISE
                 SET
                 [Subject] = @Subject,
                 [Spot] = @Spot,
                 [Compromise Date] = @CompromiseDate,
                 [Start hour] =  @Starthour,
                 [End hour] = @Endhour,
                 [Kind of compromise] = @Kindofcompromise
                 WHERE
                   [ID] = id";

            commandEdit.CommandText = sqlEdit;

            ViewDeleteID();
            Console.WriteLine("What you wish to edit?");
            Console.WriteLine("1 - Subject");
            Console.WriteLine("2 - Spot");
            Console.WriteLine("3 - Compromise date");
            Console.WriteLine("4 - Start hour");
            Console.WriteLine("5 - End hour");
            Console.WriteLine("6 - Exit");
            Option = Convert.ToInt32(Console.ReadLine());

            if (Option == 1)
            {
                Console.Clear();
                ViewDeleteID();
                Console.WriteLine("Edit the Subject...");
                Subject = Console.ReadLine();
                commandEdit.Parameters.AddWithValue("@Subject", Subject);
                DBCompromiseEditSubject();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    Console.Clear();
                    ScreenCompromise.screenCompromise();
                }
                if (Option == 1)
                {
                    DBEdit();
                }
            }
            if (Option == 2)
            {
                Console.Clear();
                Console.WriteLine("Edit the spot...");
                Spot = Console.ReadLine();
                commandEdit.Parameters.AddWithValue("@Spot", Spot);
                DBCompromiseEditSpot();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    Console.Clear();
                    ScreenCompromise.screenCompromise();
                }
                if (Option == 1)
                {
                    DBEdit();

                }
            }
            if (Option == 3)
            {
                Console.Clear();
                Console.WriteLine("Put the ID to edit the compromise:");
                id = Console.ReadLine();
                Console.WriteLine("Edit the comrpomise date...");
                CompromiseDate = Convert.ToDateTime(Console.ReadLine());
                commandEdit.Parameters.AddWithValue("@CompromiseDate", CompromiseDate);
                DBCompromiseEditCompromiseDate();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    Console.Clear();
                    ScreenCompromise.screenCompromise();
                }
                if (Option == 1)
                {
                    DBEdit();

                }
            }
            if (Option == 4)
            {
                Console.Clear();
                Console.WriteLine("Edit the start hour...");
                StartHour = Console.ReadLine();
                commandEdit.Parameters.AddWithValue("@StartHour", StartHour);
                DBCompromiseEditStartHour();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    Console.Clear();
                    ScreenCompromise.screenCompromise();
                }
                if (Option == 1)
                {
                    DBEdit();
                }
            }
            if (Option == 5)
            {
                Console.Clear();
                Console.WriteLine("Edit the end hour...");
                EndHour = Console.ReadLine();
                commandEdit.Parameters.AddWithValue("@EndHour", EndHour);
                DBCompromiseEditEndHour();
                Console.WriteLine("Wish to edit something more?");
                Console.WriteLine("1 - Edit other option");
                Console.WriteLine("2 - Exit");
                Option = Convert.ToInt32(Console.ReadLine());
                if (Option == 2)
                {
                    Console.Clear();
                    ScreenCompromise.screenCompromise();
                }
                if (Option == 1)
                {
                    DBEdit();
                }
            }
            if (Option == 6)
            {
                ScreenCompromise.screenCompromise();
            }
        }

        private static void DBCompromiseEditEndHour()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromise = new SqlConnection();
            connectionWithCompromise.ConnectionString = AdressDBtask;

            connectionWithCompromise.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithCompromise;
            Console.Clear();
            ViewDeleteID();
            Console.WriteLine("Put the ID to edit the compromise:");

            id = Console.ReadLine();

            string sqlEdit =
               @"UPDATE DBCOMPROMISE
                 SET
                 [End hour] = @Endhour
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;
            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Endhour", EndHour);

            commandEdit.ExecuteNonQuery();

            connectionWithCompromise.Close();
            Console.Clear();
            DBEdit();
        }

        private static void DBCompromiseEditCompromiseDate()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromise = new SqlConnection();
            connectionWithCompromise.ConnectionString = AdressDBtask;

            connectionWithCompromise.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithCompromise;
            Console.Clear();

            ViewDeleteID();

            string sqlEdit =
               @"UPDATE DBCOMPROMISE
                 SET
                 [Compromise Date] = @CompromiseDate
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;
            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@CompromiseDate", CompromiseDate);

            commandEdit.ExecuteNonQuery();

            connectionWithCompromise.Close();
            Console.Clear();
            DBEdit();
        }

        private static void DBCompromiseEditSpot()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromise = new SqlConnection();
            connectionWithCompromise.ConnectionString = AdressDBtask;

            connectionWithCompromise.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithCompromise;
            Console.Clear();
            ViewDeleteID();
            Console.WriteLine("Put the ID to edit the compromise:");

            id = Console.ReadLine();

            string sqlEdit =
               @"UPDATE DBCOMPROMISE
                 SET
                 [Spot] = @Spot
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;
            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Spot", Spot);

            commandEdit.ExecuteNonQuery();

            connectionWithCompromise.Close();
            Console.Clear();
            DBEdit();
        }

        private static void DBCompromiseEditSubject()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromise = new SqlConnection();
            connectionWithCompromise.ConnectionString = AdressDBtask;

            connectionWithCompromise.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithCompromise;
            Console.Clear();
            ViewDeleteID();
            Console.WriteLine("Put the ID to edit the compromise:");
            id = Console.ReadLine();

            string sqlEdit =
               @"UPDATE DBCOMPROMISE
                 SET
                 [Subject] = @Subject
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;
            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Subject", Subject);

            commandEdit.ExecuteNonQuery();

            connectionWithCompromise.Close();
            Console.Clear();
            DBEdit();
        }

        private static void DBCompromiseEditStartHour()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithCompromise = new SqlConnection();
            connectionWithCompromise.ConnectionString = AdressDBtask;

            connectionWithCompromise.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithCompromise;
            Console.Clear();
            ViewDeleteID();
            Console.WriteLine("Put the ID to edit the compromise:");

            id = Console.ReadLine();

            string sqlEdit =
               @"UPDATE DBCOMPROMISE
                 SET
                 [Start hour] = @Starthour
                 WHERE
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;
            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Starthour", StartHour);

            commandEdit.ExecuteNonQuery();

            connectionWithCompromise.Close();
            Console.Clear();
            DBEdit();
        }

        internal static void DBViewContacts()
        {
            Console.Clear();
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandGet = new SqlCommand();
            commandGet.Connection = connectionWithContact;

            string sqlGet =
                @"SELECT * FROM [TABLE]
                   ";

            sqlGet +=
                @"SELECT SCOPE_IDENTITY();";

            commandGet.CommandText = sqlGet;

            using (SqlDataReader oReader = commandGet.ExecuteReader())
            {
                while (oReader.Read())
                {
                    id = oReader["ID"].ToString();
                    NameofContact = oReader["Name"].ToString();
                    Compromise = oReader["Compromise"].ToString();
                    Status = oReader["Status"].ToString();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"| ID: {id}  | NOME OF CONTACT: {NameofContact} | COMPROMISE {Compromise} | STATUS: {Status} |");
                }
            }
            connectionWithContact.Close();
            Console.ResetColor();
            Console.ReadLine();
            ScreenCompromise.screenCompromise();
        }
    }
}
