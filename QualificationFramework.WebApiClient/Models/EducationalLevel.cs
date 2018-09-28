using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class EducationalLevel
    {
        public int Id { get; set; }

        public int LevelId { get; set; }
        public int EQFLevel { get; set; }

        public EducationalLevelLanguage ActiveLanguage { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public IList<EducationalLevelLanguage> Languages { get; set; }

        public EducationalLevel()
        {
            Languages = new List<EducationalLevelLanguage>();
        }
    }
}