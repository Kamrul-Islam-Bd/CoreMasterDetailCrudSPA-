using Microsoft.EntityFrameworkCore;

namespace CoreMasterDetailSPAForEvidence.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>op):base(op)
        {}
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseName = "C#" },
                new Course { CourseId = 2, CourseName = "J2EE" },
                new Course { CourseId = 3, CourseName = "NT" }
           );
        }
    }
}
