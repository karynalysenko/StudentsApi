using System.Text.Json.Serialization;

namespace StudentsApi1.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum University
    {
        IPCA = 1,

        UMinho = 2,

        IPVC = 3,

        PolitecnicoAveiro = 4,

        ISEP = 5
    }
}
