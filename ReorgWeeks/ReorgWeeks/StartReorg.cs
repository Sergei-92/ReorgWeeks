using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ReorgWeeks
{
    public class StartReorg
    {
        //Находим все субботы в текущем месяце
        public static List<String> dateReorg()
        {
            DateTime now = DateTime.Now;

            int year = now.Year;
            int month = now.Month;
            int dayMonth = DateTime.DaysInMonth(year, month);
            String dayWeek;
            List<String> col = new List<String>();

            for(int i = 1; i<=dayMonth; i++)
            {
                DateTime date = new DateTime(year, month, i);
                if(date.ToString("dddd") == "понедельник")
                {
                    dayWeek = date.ToString("dd.MM.yyyy");
                    col.Add(dayWeek);
                }
            }

            return col;
        }

        //Закрываем Центер управления DB2
        public void ControlcenterDB2Close()
        {
            Process[] ps1 = System.Diagnostics.Process.GetProcessesByName("javaw"); //Имя процесса
            foreach (Process p1 in ps1)
            {
                p1.Kill();
            }
        }

        //Запускаем батник для реорганизации
        public void StartBatFile(String path)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments = @"/C cd C:\testreorg\ & " + path;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                int exitCode = proc.ExitCode;
                proc.Close();
              }
             catch(Exception)
              {
                 Console.WriteLine("Bat файл по заданному пути не найден.");
                  
             }
            
        }

    }
}
