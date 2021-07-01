using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        public DateTime CompromiseTest { get; set; }
        public DayOfWeek Option { get; private set; }

        [TestMethod]
        public bool TestDate()
        {
            var Date1 = DayOfWeek.Friday;
            var Date2 = DayOfWeek.Saturday;
            var Date3 = DayOfWeek.Monday;
            var Date4 = DayOfWeek.Wednesday;
            var Date5 = DayOfWeek.Tuesday;
            var Date6 = DayOfWeek.Thursday;
            var Date7 = DayOfWeek.Sunday;

            //Option = Date1;
            //Option = Date2;
            //Option = Date3;
            //Option = Date4;
            //Option = Date5;
            //Option = Date6;
            Option = Date7;

            if (Option == Date1)
            {
                return true;
            }
            if (Option == Date2)
            {
                return false;
            }
            if (Option == Date3)
            {
                return false;
            }
            if (Option == Date4)
            {
                return true;
            }
            if (Option == Date5)
            {
                return true;
            }
            if (Option == Date6)
            {
                return true;
            }
            if (Option == Date7)
            {
                return false;
            }
            return TestDate();
        }

      
    }
}
