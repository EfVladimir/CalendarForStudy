using Microsoft.EntityFrameworkCore;
using HomeProjectTimeTable.Models;

namespace HomeProjectTimeTable.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }
        public DbSet<Classroom> classrooms { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Teacher> teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=VLADIMIR;Initial Catalog=calendar-db;Trusted_Connection=True");
        }
    }
}
