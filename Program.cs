using System;
using NLog.Web;
using System.IO;

namespace BlogsConsole
{
    public class Program
    {

        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");
            Login login = new Login();
            Run run = new Run();

            try
            {
                File.Delete("appsettings.json");
                login.userConnectionString();
                run.startUp();
                run.selectOption();
               
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Wrong Login and/or Password");
                login.userConnectionString();
                logger.Error(ex.Message);
                System.Console.WriteLine(ex);
            }

            logger.Info("Program ended");
        }
        

    }

}
