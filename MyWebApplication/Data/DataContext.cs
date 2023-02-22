using Microsoft.EntityFrameworkCore;
using StudentsApi1.Models;

namespace StudentsApi1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)  : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>().HasData(
                new University { UniversityId = 1, UniversityName = "IPCA" },
                new University { UniversityId = 2, UniversityName = "UMinho" },
                new University { UniversityId = 3, UniversityName = "IPVC" },
                new University { UniversityId = 4, UniversityName = "Politecnico de Aveiro" },
                new University { UniversityId = 5, UniversityName = "ISEP" }
                );

            //modelBuilder.Entity<Course>().HasData(
            //    new Course { CourseId = 1, CourseName = "LEIM" },
            //    new Course { CourseId = 2, CourseName = "LESI" },
            //    new Course { CourseId = 3, CourseName = "LEBIS" },
            //    new Course { CourseId = 4, CourseName = "LEMP" }
            //    );
            modelBuilder.Entity<Student>()
                .HasOne(s => s.University)
                .WithMany()
                .HasForeignKey( s => s.UniversityId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<Student> Students { get; set; }
        //public DbSet<University> Universitys { get; set; }
        //public DbSet<Course> Courses { get; set; }
        //public DbSet<Skill> Skills { get; set; }

    }
}
