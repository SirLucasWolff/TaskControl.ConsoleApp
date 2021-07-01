using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControl.ConsoleApp
{
    class DBLink
    {
        private static string id;

        public static string Subject { get; private set; }
        public static string Spot { get; private set; }
        public static string NameofContact { get; private set; }
        public static string Compromise { get; private set; }
        public static string Status { get; private set; }
        public static string Link { get; private set; }
        public static int Option { get; private set; }
        public static string OptionLink { get; private set; }
        public static string StartHour { get; private set; }
        public static string EndHour { get; private set; }
        public static string KindCompromise { get; private set; }
        public static object CompromiseDate { get; private set; }


        internal static void DBInsert()
        {
            Console.WriteLine("Put the link:");
            Link = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Link registed with succsses, don't forget your compromise");
            Console.ResetColor();
            Console.ReadLine();

            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithLink = new SqlConnection();
            connectionWithLink.ConnectionString = AdressDBtask;

            connectionWithLink.Open();

            SqlCommand commandInsert = new SqlCommand();
            commandInsert.Connection = connectionWithLink;

            string sqlInsert =
            @"INSERT INTO DBLINK 
                (
                 [Link]
                )
                VALUES 
                (
                 @Link
                )";

            sqlInsert +=
                @"SELECT SCOPE_IDENTITY();";

            commandInsert.CommandText = sqlInsert;

            commandInsert.Parameters.AddWithValue("Link", Link);

            object id = commandInsert.ExecuteScalar();

            object Id = Convert.ToInt32(id);

            Console.WriteLine(commandInsert.ExecuteNonQuery());

            connectionWithLink.Close();

            ScreenCompromise.screenCompromise();
        }

        public static void ViewTable()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithContact = new SqlConnection();
            connectionWithContact.ConnectionString = AdressDBtask;

            connectionWithContact.Open();

            SqlCommand commandGet = new SqlCommand();
            commandGet.Connection = connectionWithContact;

            string sqlGet =
                @"SELECT * FROM DBLINK
                   ";

            sqlGet +=
                @"SELECT SCOPE_IDENTITY();";

            commandGet.CommandText = sqlGet;

            using (SqlDataReader oReader = commandGet.ExecuteReader())
            {
                while (oReader.Read())
                {
                    id = oReader["ID"].ToString();
                    Link = oReader["Link"].ToString();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"| ID: {id}  | LINK: {Link} |");
                }
            }
            connectionWithContact.Close();
            Console.ResetColor();
            Console.WriteLine("You wish view the compromises linked?");
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");

            Option = Convert.ToInt32(Console.ReadLine());

            if (Option == 1)
            {
                Console.WriteLine("Put the link to see the compromise");
                OptionLink = Console.ReadLine();

                if (OptionLink == Link)
                {
                    Console.Clear();
                    LinkTable();

                }
                if (OptionLink != Link)
                {
                    Console.Clear();
                    ViewTable();
                }

            }
            if (Option == 2)
            {
                ScreenCompromise.screenCompromise();
            }
        }

        public static void LinkTable()
        {
            string AdressDBtask = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";

            SqlConnection connectionWithDBLink = new SqlConnection();
            connectionWithDBLink.ConnectionString = AdressDBtask;

            connectionWithDBLink.Open();

            SqlCommand commandLink = new SqlCommand();
            commandLink.Connection = connectionWithDBLink;

            string sqlLink =
                @"SELECT * FROM DBCOMPROMISE
                         ";

            commandLink.CommandText = sqlLink;

            using (SqlDataReader oReader = commandLink.ExecuteReader())
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
            commandLink.ExecuteNonQuery();
            Console.ResetColor();
            connectionWithDBLink.Close();
            Console.ReadLine();




            connectionWithDBLink.Close();

            ScreenCompromise.screenCompromise();
        }
    }
}
