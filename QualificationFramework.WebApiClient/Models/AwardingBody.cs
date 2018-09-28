using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class AwardingBody
    {
        public int Id { get; set; }
        
        [NotMapped]
        public AwardingBodyLanguage ActiveLanguage { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]

        public IList<AwardingBodyLanguage> Languages { get; set; }

        public IList<QualificationType> QualificationTypes { get; set; } 

        public AwardingBody()
        {
            Languages = new List<AwardingBodyLanguage>();
            QualificationTypes = new List<QualificationType>();
        }
    }
}