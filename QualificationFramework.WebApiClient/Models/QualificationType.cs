using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class QualificationType
    {
        public int Id { get; set; }

        public int EducationalLevelId { get; set; }
        public EducationalLevel EducationalLevel { get; set; }

        public int EducationalSectorId { get; set; }
        public EducationalSector EducationalSector { get; set; }

        [NotMapped]
        public QualificationTypeLanguage ActiveLanguage { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]

        public IList<QualificationTypeLanguage> Languages { get; set; }

        public IList<AwardingBody> AwardingBodies { get; set; }
        public QualificationType()
        {
            Languages = new List<QualificationTypeLanguage>();
            AwardingBodies = new List<AwardingBody>();
        }
    }
}