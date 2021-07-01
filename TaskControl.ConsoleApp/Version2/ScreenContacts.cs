using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControl.ConsoleApp
{
    class ScreenContacts
    {
        public static int Option { get; private set; }

        internal static void screenContact()
        {
            Console.Clear();
            Console.WriteLine("Add a contact = 1");
            Console.WriteLine("View the contacts = 2");
            Console.WriteLine("Edit a contact = 3");
            Console.WriteLine("Delete a contact = 4");
            Console.WriteLine("Screen start = 5");
            Console.WriteLine("Exit = 0");
            Option = Convert.ToInt32(Console.ReadLine());

            if (Option == 1)
            {
                DBContacts.DBInsert();
            }
            if (Option == 2)
            {
                DBContacts.DBView();
            }
            if (Option == 3)
            {
                DBContacts.DBEdit();
            }
            if (Option == 4)
            {
                DBContacts.DBDelete();
            }
            if (Option == 5)
            {
                ScreenStart.screenStart();
            }
            if (Option == 0)
            {

            }
        }
    }
}
