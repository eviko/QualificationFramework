using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class EducationalLevelLanguage
    {
        public int Id { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public EducationalLevel EducationalLevel { get; set; }
        public int EducationalLevelId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Knowledge { get; set; }
        public string Skills { get; set; }
        public string Competence { get; set; }
    }
}