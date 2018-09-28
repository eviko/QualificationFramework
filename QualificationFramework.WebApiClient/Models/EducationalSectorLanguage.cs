using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class EducationalSectorLanguage
    {
        public int Id { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]

        public EducationalSector EducationalSector { get; set; }
        public int EducationalSectorId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
    }
}