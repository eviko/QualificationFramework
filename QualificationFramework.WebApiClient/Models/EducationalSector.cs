using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class EducationalSector
    {
        public int Id { get; set; }

        
        [NotMapped]
        public EducationalSectorLanguage ActiveLanguage { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]

        public IList<EducationalSectorLanguage> Languages { get; set; }

        public EducationalSector()
        {
            Languages = new List<EducationalSectorLanguage>();
        }
    }
}