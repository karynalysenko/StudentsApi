//using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace StudentsApi1.Models
{
    public class University
    {
        [Key]
        public int UniversityId { get; set; }
        public string? UniversityName { get; set; }

        //public List<Student>? Students { get; set; } = new List<Student>();
        //public List<Course> Courses { get; set; }

    }
}
   