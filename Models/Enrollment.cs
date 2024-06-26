using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A,
        B,
        C,
        D,
        F
    }

    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; } //FK
        public int StudentID { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Course Course { get; set; } //Nav. Property

        public Student Student { get; set; }
    }
}
