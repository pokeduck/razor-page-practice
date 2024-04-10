using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace ContosoUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>();
            modelBuilder.Entity<Enrollment>();
            modelBuilder.Entity<Student>();
            // modelBuilder
            //     .Entity<Enrollment>()
            //     .HasOne(e => e.Course)
            //     .WithMany(e => e.Enrollments)
            //     .HasForeignKey(e => e.CourseID);

            // modelBuilder
            //     .Entity<Enrollment>()
            //     .HasOne(e => e.Student)
            //     .WithMany(e => e.Enrollments)
            //     .HasForeignKey(e => e.StudentID);
        }
    }
}
