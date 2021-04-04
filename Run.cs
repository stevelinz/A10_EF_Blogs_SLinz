using System;
using System.IO;
using System.Linq;
using NLog.Web;
using Microsoft.EntityFrameworkCore;


namespace BlogsConsole
{
    public class Run
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public void startUp()
        {
            System.Console.WriteLine("Enter your selection:");
            System.Console.WriteLine("1) Display all Blogs");
            System.Console.WriteLine("2) Add Blog");
            System.Console.WriteLine("3) Display Posts");
            System.Console.WriteLine("4) Add Post");
            System.Console.WriteLine("Enter q to quit");
        }
        BloggingContext db = new BloggingContext();
        public void selectOption()
        {
        beginning:
            string select = "";
            select = Console.ReadLine();
            switch (select)
            {
                case "1": // DISPLAY BLOGS
                    var orderQueryName = db.Blogs.OrderBy(b => b.Name);
                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in orderQueryName)
                    {
                        Console.WriteLine(item.Name);
                    }
                    startUp();
                    goto beginning;
                case "2": // ADD BLOG
                    tryAgain:
                    Console.Write("Enter a name for a new Blog: ");
                    var name = Console.ReadLine();
                    if(String.IsNullOrEmpty(name)) goto tryAgain;
                    var blog = new Blog { Name = name };
                    db.AddBlog(blog);
                    logger.Info("Blog added - {name}", name);
                    startUp();
                    goto beginning;
                case "3": // DISPLAY POSTS

                    int blogIdL = selectBlog();

                    using (var context = new BloggingContext())
                    {
                     var blogs = context.Blogs
                     .FromSqlInterpolated($"SELECT * FROM dbo.Blogs Where BlogId = ({blogIdL})").ToList();

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        foreach (Blog blogName in blogs)
                        {
                            System.Console.WriteLine($"Blog: " + blogName.Name);
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    using (var context = new BloggingContext())
                    {
                        var posts = context.Posts
                        .FromSqlInterpolated($"SELECT * FROM dbo.Posts Where BlogId = ({blogIdL})").ToList();
                        foreach (Post post in posts)
                        {
                            System.Console.WriteLine($"Title: " + post.Title + "\tContent: " + post.Content);
                        }
                    }



                    startUp();
                    goto beginning;
                case "4":  // ADD POST
                    int workingBlogId = selectBlog();
                    oppsNoTitle:
                    Console.Write("Enter a title for the new Post: ");
                    var title = Console.ReadLine();
                    if(String.IsNullOrEmpty(title)) goto oppsNoTitle;
                    
                    Console.Write("Enter content for the new Post: ");
                    var content = Console.ReadLine();
                    using (var db = new BloggingContext())
                    {
                        var post = new Post()
                        {
                            BlogId = workingBlogId,
                            Content = content,
                            Title = title
                        };
                        db.Posts.Add(post);
                        db.SaveChanges();
                    }
                    logger.Info("Post added!");
                    startUp();
                    goto beginning;
                case "q":
                    Console.WriteLine("Quit");
                    db.QuitProgram();
                    break;
                default:
                    Console.WriteLine("Wrong input try again");
                    startUp();
                    goto beginning;

            }
        }
        public int selectBlog()
        {
            int count = 0;
            string blogID;
            var orderQueryId = db.Blogs.OrderBy(b => b.BlogId);
            Console.WriteLine("Select the ID number of the Blog you want");
            System.Console.WriteLine("ID\tBLOG");
            foreach (var item in orderQueryId)
            {
                Console.WriteLine(item.BlogId + "\t" + item.Name);
                count++;
            }
        andAgain:
            int x;
            bool intTest;
            blogID = Console.ReadLine();
            intTest = int.TryParse(blogID, out x);
            int theBlogIdTest = Int32.Parse(blogID);
            if (intTest == false || theBlogIdTest > count || theBlogIdTest == 0)
            {
                Console.WriteLine("Select the ID number of the Blog you want");
                goto andAgain;
            }
            else
            {
                return theBlogIdTest;  // return a BlogId
            }
        }



    }
}