using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentsApi1.Models
{    
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string? CourseName { get; set; } 
        //public List<Student>? Students { get; set; } = new List<Student>();
        //public List<University> Universities { get; set; }
    }
}
