using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Models.Domain;

namespace SimpleCRUD.Models.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
