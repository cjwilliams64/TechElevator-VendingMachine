using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class FileLog
    {
        public void Log(string message, decimal moneyStart, decimal moneyAfter)
        {
            string dir = @"C:\Users\Student\git\c-module-1-capstone-team-8\19_Capstone"; // TODO: Potentially remove later 
            Directory.SetCurrentDirectory(dir); // TODO: Potentially remove later
            DateTime date = DateTime.Now;
            string calendarDate = date.ToString("MM/dd/yyyy hh:mm:ss tt");

            string moneyStartString = moneyStart.ToString("C");

            string moneyAfterString = moneyAfter.ToString("C");

            string logLine = $"{calendarDate} {message} {moneyStartString} {moneyAfterString}";

            try
            {
                using(StreamWriter sw = new StreamWriter("Log.txt", true))
                {
                    sw.WriteLine(logLine);
                }
            }
            catch
            {
                Console.WriteLine("Ran into an error when trying to log the file.");
                return;
            }
        }
    }
}
