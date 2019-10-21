using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace ReorgWeeks
{
    class MainReorgWeeks
    {
        static void Main(string[] args)
        {
            DateTime today = DateTime.Now;
            StartReorg obj = new StartReorg();
            INIManager ini = new INIManager(Path.Combine(Environment.CurrentDirectory, "config.ini"));
            String var;

            obj.ControlcenterDB2Close();

            var = ini.GetPrivateString("checkReorg", "value");
          

            foreach (String date in StartReorg.dateReorg())
            {
                if(today.ToString("dd.MM.yyyy") == date)
                {
                    if (Int32.Parse(var) % 2 != 0)
                    {
                        //obj.StartBatFile(@"/testreorg & start_reorg_week1.bat");
                        obj.StartBatFile("start_reorg_week1.bat");
                        //Меняем значение в App.config на четное число
                        ini.WritePrivateString("checkReorg", "value", "2");
                        //Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        //currentConfig.AppSettings.Settings["checkReorg"].Value = "2";
                        //currentConfig.Save(ConfigurationSaveMode.Full);
                        //ConfigurationManager.RefreshSection("appSettings");
                    }
                    if (Int32.Parse(var) % 2 == 0)
                    {
                        obj.StartBatFile("start_reorg_week2.bat");
                        //Меняем значение в App.config на нечетное число
                        ini.WritePrivateString("checkReorg", "value", "1");
                        //Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        //currentConfig.AppSettings.Settings["checkReorg"].Value = "1";
                        //currentConfig.Save(ConfigurationSaveMode.Modified);
                        //ConfigurationManager.RefreshSection("appSettings");
                    }
                }
            }

            Console.WriteLine("Регламентные работы завершины. Посмотрите log выполнения регламентных работ.");
            Console.ReadKey();
        }
    }
}
