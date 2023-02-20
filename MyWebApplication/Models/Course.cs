using System.Text.Json.Serialization;

namespace StudentsApi1.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Course
    {
        LEIM = 1,

        LESI = 2,

        LEBIS = 3,

        LEMP = 4
    }
    //public List<Student>? Students { get; set; }

}
