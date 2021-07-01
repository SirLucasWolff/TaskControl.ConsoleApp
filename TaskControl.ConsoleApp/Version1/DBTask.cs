using System;
using System.Data.SqlClient;

namespace TaskControl.ConsoleApp
{
    internal class DBTask
    {
        public static string Task { get; set; }
        public static int ConclusioPercent { get; set; }
        public static string ImportanceLevel { get; set; }
        public static int Option { get; set; }
        public object Id { get; internal set; }
        public static DateTime ConclusionDate { get; set; }
        public static DateTime CreationDate { get; set; }
        public static string id { get; set; }

        internal static void DBInsert()
        {
            Console.Clear();
            Console.WriteLine("New Task:");
            Task = Console.ReadLine();
            Console.Clear();

            CreationDate = DateTime.Now;

            Console.Clear();
            Console.WriteLine("New Conclusion date:");
            ConclusionDate = Convert.ToDateTime(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Choose the importance level");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Low = 1");
            Console.WriteLine("Normal = 2");
            Console.WriteLine("High = 3");
            Console.ResetColor();
            Option = Convert.ToInt32(Console.ReadLine());

            if (Option == 1)
            {
                ImportanceLevel = "Low";
            }
            if (Option == 2)
            {
                ImportanceLevel = "Normal";
            }
            if (Option == 3)
            {
                ImportanceLevel = "High";
            }
            Console.Clear();

            Console.WriteLine("Choose the conclusion percent:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("0 = 0");
            Console.WriteLine("10% = 1");
            Console.WriteLine("20% = 2");
            Console.WriteLine("30% = 3");
            Console.WriteLine("40% = 4");
            Console.WriteLine("50% = 5");
            Console.WriteLine("60% = 6");
            Console.WriteLine("70% = 7");
            Console.WriteLine("80% = 8");
            Console.WriteLine("90% = 9");
            Console.WriteLine("100% = 10");
            Console.ResetColor();
            ConclusioPercent = Convert.ToInt32(Console.ReadLine());

            if (ConclusioPercent == 0)
            {
                ConclusioPercent = 0;
            }
            if (ConclusioPercent == 1)
            {
                ConclusioPercent = 10;
            }
            if (ConclusioPercent == 2)
            {
                ConclusioPercent = 20;
            }
            if (ConclusioPercent == 3)
            {
                ConclusioPercent = 30;
            }
            if (ConclusioPercent == 4)
            {
                ConclusioPercent = 40;
            }
            if (ConclusioPercent == 5)
            {
                ConclusioPercent = 50;
            }
            if (ConclusioPercent == 6)
            {
                ConclusioPercent = 60;
            }
            if (ConclusioPercent == 7)
            {
                ConclusioPercent = 70;
            }
            if (ConclusioPercent == 8)
            {
                ConclusioPercent = 80;
            }
            if (ConclusioPercent == 9)
            {
                ConclusioPercent = 90;
            }
            if (ConclusioPercent == 100)
            {
                ConclusioPercent = 100;
            }


            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithTask = new SqlConnection();
            connectionWithTask.ConnectionString = AdressDBtask;

            connectionWithTask.Open();

            SqlCommand commandInsert = new SqlCommand();
            commandInsert.Connection = connectionWithTask;

            string sqlInsert =
                @"INSERT INTO DBTASK 
                (
                    [Task],
                    [Creation Date],
                    [Conclusion Date],
                    [Importance Level],
                    [Conclusion Percent]
                )
                VALUES 
                (
                    @Task,
                    @CreationDate,
                    @ConclusionDate,
                    @ImportanceLevel,
                    @ConclusionPercent
                )";
            sqlInsert +=
                @"SELECT SCOPE_IDENTITY();";

            commandInsert.CommandText = sqlInsert;

            commandInsert.Parameters.AddWithValue("Task", Task);
            commandInsert.Parameters.AddWithValue("CreationDate", CreationDate);
            commandInsert.Parameters.AddWithValue("ConclusionDate", ConclusionDate);
            commandInsert.Parameters.AddWithValue("ImportanceLevel", ImportanceLevel);
            commandInsert.Parameters.AddWithValue("ConclusionPercent", ConclusioPercent);

            object id = commandInsert.ExecuteScalar();

            object Id = Convert.ToInt32(id);

            connectionWithTask.Close();
            Console.Clear();
            ScreenStart.screenStart();
        }

        internal static void DBView()
        {
            Console.Clear();
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithTask = new SqlConnection();
            connectionWithTask.ConnectionString = AdressDBtask;

            connectionWithTask.Open();

            SqlCommand commandGet = new SqlCommand();
            commandGet.Connection = connectionWithTask;

            string sqlGet =
                @"SELECT * FROM DBTASK
                   ";

            sqlGet +=
                @"SELECT SCOPE_IDENTITY();";

            commandGet.CommandText = sqlGet;

            using (SqlDataReader oReader = commandGet.ExecuteReader())
            {
                while (oReader.Read())
                {
                    id = oReader["ID"].ToString();
                    Task = oReader["Task"].ToString();
                    CreationDate = (DateTime)oReader["Creation Date"];
                    ConclusionDate = (DateTime)oReader["Conclusion Date"];
                    ImportanceLevel = oReader["Importance Level"].ToString();
                    ConclusioPercent = (int)oReader["Conclusion Percent"];
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"| ID: {id}  |TASK: {Task} | CREATION DATE: {CreationDate} | CONCLUSION DATE: {ConclusionDate} | IMPORTANCE LEVEL: {ImportanceLevel} | CONCLUSION PERCENT: {ConclusioPercent} |");
                }
            }
            connectionWithTask.Close();
            Console.ResetColor();
            Console.ReadLine();
            ScreenTask.screenTask();
        }

        internal static void DBEdit()
        {
            Console.Clear();


            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithTask = new SqlConnection();
            connectionWithTask.ConnectionString = AdressDBtask;

            connectionWithTask.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithTask;

            string sqlEdit =
               @"UPDATE DBTASK
                 SET
                 [Task] = @Task,
                 [Importance Level] = @Importance Level,
                 [Creation Date] = @Creation Date,
                 [Conclusion Date] =  @Conclusion Date,
                 [Conclusion Percent] = @Conclusion Percent
                 WHERE
                   [ID] = id";

            commandEdit.CommandText = sqlEdit;
            ViewDB();

            Console.WriteLine("What you wish to edit?");
            Console.WriteLine("1 - Task");
            Console.WriteLine("2 - Conclusion Date");
            Console.WriteLine("3 - Conclusion Percent");

            Option = Convert.ToInt32(Console.ReadLine());

            if (Option == 1)
            {
                Console.Clear();
                ViewDB();
                Console.WriteLine("Edit the task...");
                Task = Console.ReadLine();
                commandEdit.Parameters.AddWithValue("@Task", Task);
                DBTaskEditTask();
                ViewDB();
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
                ViewDB();
                Console.WriteLine("Edit the conclusion date...");
                ConclusionDate = Convert.ToDateTime(Console.ReadLine());
                commandEdit.Parameters.AddWithValue("@ConclusionDate", ConclusionDate);
                DBTaskEditConclusionDate();
                ViewDB();
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
                ViewDB();
                Console.WriteLine("Edit the conclusion percent...");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("0 = 0");
                Console.WriteLine("10% = 1");
                Console.WriteLine("20% = 2");
                Console.WriteLine("30% = 3");
                Console.WriteLine("40% = 4");
                Console.WriteLine("50% = 5");
                Console.WriteLine("60% = 6");
                Console.WriteLine("70% = 7");
                Console.WriteLine("80% = 8");
                Console.WriteLine("90% = 9");
                Console.WriteLine("100% = 10");
                Console.ResetColor();
                ConclusioPercent = Convert.ToInt32(Console.ReadLine());
                if (ConclusioPercent == 0)
                {
                    ConclusioPercent = 0;
                }
                if (ConclusioPercent == 1)
                {
                    ConclusioPercent = 10;
                }
                if (ConclusioPercent == 2)
                {
                    ConclusioPercent = 20;
                }
                if (ConclusioPercent == 3)
                {
                    ConclusioPercent = 30;
                }
                if (ConclusioPercent == 4)
                {
                    ConclusioPercent = 40;
                }
                if (ConclusioPercent == 5)
                {
                    ConclusioPercent = 50;
                }
                if (ConclusioPercent == 6)
                {
                    ConclusioPercent = 60;
                }
                if (ConclusioPercent == 7)
                {
                    ConclusioPercent = 70;
                }
                if (ConclusioPercent == 8)
                {
                    ConclusioPercent = 80;
                }
                if (ConclusioPercent == 9)
                {
                    ConclusioPercent = 90;
                }
                if (ConclusioPercent == 10)
                {
                    ConclusioPercent = 100;
                }
                commandEdit.Parameters.AddWithValue("@ConclusionPercent", ConclusioPercent);
                DBTaskConclusionPercent();
                ViewDB();
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
                ScreenTask.screenTask();
            }
        }

        private static void DBTaskConclusionPercent()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithTask = new SqlConnection();
            connectionWithTask.ConnectionString = AdressDBtask;

            connectionWithTask.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithTask;

            Console.WriteLine("Put the ID to edit the task:");

            id = Console.ReadLine();

            string sqlEdit =
               @"UPDATE DBTASK
                 SET
                 [Conclusion Percent] = @ConclusionPercent
                 WHERE 
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;

            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@ConclusionPercent", ConclusioPercent);

            commandEdit.ExecuteNonQuery();

            connectionWithTask.Close();
            Console.Clear();
            ScreenTask.screenTask();
        }

        private static void DBTaskEditConclusionDate()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithTask = new SqlConnection();
            connectionWithTask.ConnectionString = AdressDBtask;

            connectionWithTask.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithTask;

            Console.WriteLine("Put the ID to edit the task:");

            id = Console.ReadLine();

            string sqlEdit =
               @"UPDATE DBTASK
                 SET
                 [Conclusion Date] = @ConclusionDate
                 WHERE 
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;

            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@ConclusionDate", ConclusionDate);

            commandEdit.ExecuteNonQuery();

            connectionWithTask.Close();
            Console.Clear();
            ScreenTask.screenTask();
        }

        private static void DBTaskEditTask()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithTask = new SqlConnection();
            connectionWithTask.ConnectionString = AdressDBtask;

            connectionWithTask.Open();

            SqlCommand commandEdit = new SqlCommand();
            commandEdit.Connection = connectionWithTask;

            Console.WriteLine("Put the ID to edit the task:");

            id = Console.ReadLine();

            string sqlEdit =
               @"UPDATE DBTASK
                 SET
                 [Task] = @Task
                 WHERE 
                   [ID] = @id";

            commandEdit.CommandText = sqlEdit;

            commandEdit.Parameters.AddWithValue("@id", id);
            commandEdit.Parameters.AddWithValue("@Task", Task);

            commandEdit.ExecuteNonQuery();

            connectionWithTask.Close();
            Console.Clear();
            ScreenTask.screenTask();
        }

        internal static void DBDelete()
        {
            Console.Clear();
            ViewDB();
            Console.WriteLine("1 - Clear the list?");
            Console.WriteLine("2 - Delete complete tasks");
            Console.WriteLine("3 - Delete with Id");
            Console.WriteLine("4 - Retur to Screen Task");
            Option = Convert.ToInt32(Console.ReadLine());
            if (Option == 1)
            {
                DeleteAll();
            }
            if (Option == 3)
            {
                Console.Clear();
                ViewDB();
                Console.WriteLine("Put the ID to delete the Task:");
                id = Console.ReadLine();

                string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

                SqlConnection connectionWithTask = new SqlConnection();
                connectionWithTask.ConnectionString = AdressDBtask;

                connectionWithTask.Open();

                SqlCommand commandDelete = new SqlCommand();
                commandDelete.Connection = connectionWithTask;

                string sqlDelete =
                    @"DELETE FROM DBTASK 	                
	                WHERE 
		                [ID] = @id";


                commandDelete.CommandText = sqlDelete;

                commandDelete.Parameters.AddWithValue("@id", id);

                commandDelete.ExecuteNonQuery();

                connectionWithTask.Close();
            }
            if (Option == 4)
            {
                ScreenTask.screenTask();
            }
            if (Option == 2)
            {
                DeleteID();
            }

            ScreenTask.screenTask();
        }

        private static void DeleteAll()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithTask = new SqlConnection();
            connectionWithTask.ConnectionString = AdressDBtask;

            connectionWithTask.Open();

            SqlCommand commandDelete = new SqlCommand();
            commandDelete.Connection = connectionWithTask;

            string sqlDelete =
                @"DELETE FROM DBTASK 	                
	               ";


            commandDelete.CommandText = sqlDelete;



            commandDelete.ExecuteNonQuery();

            connectionWithTask.Close();

            ScreenTask.screenTask();
        }

        private static void ViewDB()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithTask = new SqlConnection();
            connectionWithTask.ConnectionString = AdressDBtask;

            connectionWithTask.Open();

            SqlCommand commandGet = new SqlCommand();
            commandGet.Connection = connectionWithTask;

            string sqlGet =
                @"SELECT * FROM DBTASK
                   ";

            sqlGet +=
                @"SELECT SCOPE_IDENTITY();";

            commandGet.CommandText = sqlGet;

            using (SqlDataReader oReader = commandGet.ExecuteReader())
            {
                while (oReader.Read())
                {
                    id = oReader["ID"].ToString();
                    Task = oReader["Task"].ToString();
                    CreationDate = (DateTime)oReader["Creation Date"];
                    ConclusionDate = (DateTime)oReader["Conclusion Date"];
                    ImportanceLevel = oReader["Importance Level"].ToString();
                    ConclusioPercent = (int)oReader["Conclusion Percent"];
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"| ID: {id}  |TASK: {Task} | CREATION DATE: {CreationDate} | CONCLUSION DATE: {ConclusionDate} | IMPORTANCE LEVEL: {ImportanceLevel} | CONCLUSION PERCENT: {ConclusioPercent} |");
                }
            }
            connectionWithTask.Close();
            Console.ResetColor();
        }

        private static void DeleteID()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithTask = new SqlConnection();
            connectionWithTask.ConnectionString = AdressDBtask;

            connectionWithTask.Open();

            SqlCommand commandDelete = new SqlCommand();
            commandDelete.Connection = connectionWithTask;

            string sqlDelete =
                @"DELETE FROM DBTASK 	                
	               WHERE
                 [Conclusion Percent] = 100";


            commandDelete.CommandText = sqlDelete;



            commandDelete.ExecuteNonQuery();

            connectionWithTask.Close();

            ScreenTask.screenTask();
        }
    }
}