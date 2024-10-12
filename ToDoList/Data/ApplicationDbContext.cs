using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ApplicationDbContext: DbContext
    {
       public DbSet<ToDoItem> ToDoItems { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
        var Connection = builder.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(Connection);
    }
    }
}
