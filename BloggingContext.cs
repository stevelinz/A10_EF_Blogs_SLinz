using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;




namespace BlogsConsole
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public void AddBlog(Blog blog)
        {
            this.Blogs.Add(blog);
            this.SaveChanges();       
        }

        public void AddPost(Post post)
        {
            this.Posts.Add(post);         
            this.SaveChanges();
        }

        public void QuitProgram()
        {
            this.Database.CloseConnection();
            File.Delete("appsettings.json");
            System.Environment.Exit(53095);
        }
        public  string path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                   
           IConfiguration config = new ConfigurationBuilder()
 
                .AddJsonFile(path)                 
                .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

        }
        
    }
}
