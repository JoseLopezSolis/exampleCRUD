using System.Data.Common;
using exampleCRUD.Models;

namespace exampleCRUD.DataAccess;
using exampleCRUD.Utilities;
using Microsoft.EntityFrameworkCore;

public class EmployeeDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } 
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string conectionDB = $"Filename={ConnectionDB.returnRoute("employees.db")}";
        optionsBuilder.UseSqlite(conectionDB);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(col => col.IdEmployee);
            
            entity.Property(col => col.IdEmployee).IsRequired().ValueGeneratedOnAdd();
            
        });
    }

}