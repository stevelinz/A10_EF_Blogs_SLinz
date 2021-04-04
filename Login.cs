using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogsConsole
{
    public class Login  : DbContext
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

         //   checkConn();

        }

        
         
        //  void checkConn()
        //  {
        //     SqlConnection conn = 
        //     new SqlConnection("Server =bitsql.wctc.edu; Database = Blogs_SL_22097;"
        //     + "User Id =" + userName + "; Password =" + passWord );
           
        //         if (conn.State == ConnectionState.Open)
        //         {
        //             System.Console.WriteLine("Wrong username and/or password");
        //             userConnectionString();
        //         } 
        //  }

        
    }
}

