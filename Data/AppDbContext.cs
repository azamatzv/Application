using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public class AppDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Database=CrudDatabase; Username=postgres; Password=0932");
    }
}