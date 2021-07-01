using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControl.ConsoleApp
{
    class ScreenCompromise
    {
        public static int Option { get; private set; }

        internal static void screenCompromise()
        {
            Console.Clear();
            Console.WriteLine("Add a compromise = 1");
            Console.WriteLine("View the compromises = 2");
            Console.WriteLine("Edit a compromise = 3");
            Console.WriteLine("Delete a compromise = 4");
            Console.WriteLine("View the contacts = 5");
            Console.WriteLine("Screen start = 6");
            Console.WriteLine("Exit = 0");
            Option = Convert.ToInt32(Console.ReadLine());

            if (Option == 1)
            {
                DBCompromise.DBInsert();
            }
            if (Option == 2)
            {

                DBCompromise.DBView();
            }
            if (Option == 3)
            {
                DBCompromise.DBEdit();
            }
            if (Option == 4)
            {
                DBCompromise.DBDelete();
            }
            if (Option == 5)
            {
                DBCompromise.DBViewContacts();
            }
            if (Option == 6)
            {
                ScreenStart.screenStart();
            }
            if (Option == 0)
            {

            }
        }
    }
}
