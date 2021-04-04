using System;
using System.IO;
using Microsoft.EntityFrameworkCore;


namespace BlogsConsole
{
    public class Login 
    {

        private string userName = "";
        private string passWord = "";

       public  string path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
        public void userConnectionString()
        {

            System.Console.WriteLine("Welcome to the Blogging Database");

            System.Console.Write("Enter User Name: ");
            userName = Console.ReadLine();

            System.Console.Write("Enter Password: ");
            passWord = Console.ReadLine();
 

            string createConnection =
            "{ \"ConnectionStrings\": { \"DefaultConnection\": \"Server =bitsql.wctc.edu; Database = Blogs_SL_22097;"
            + "User Id =" + userName + "; Password =" + passWord + ";" + "\"" + "} }";
            File.WriteAllText(path, createConnection);

         

        }
        
    }
}

