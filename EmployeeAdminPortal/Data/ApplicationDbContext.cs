using EmployeeAdminPortal.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Data
{
    public class ApplicationDbContext : DbContext // appDbContext is inhering from DbContext class that is coming from the package that we installed 
    {// ctor short key to make a constructor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        //made a property collection to store employee in db
        public DbSet<Employee> Employees { get; set; }
    }
}
