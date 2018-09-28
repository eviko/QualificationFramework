using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class Qualification
    {
        public int Id { get; set; }

        public int? QualificationTypeId { get; set; }
        public QualificationType QualificationType { get; set; }

        public int? EducationalLevelId { get; set; }
        public EducationalLevel EducationalLevel { get; set; }

        public int? AwardingBodyId { get; set; }
        public AwardingBody AwardingBody { get; set; }


        [NotMapped]
        public QualificationLanguage ActiveLanguage { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]

        public IList<QualificationLanguage> Languages { get; set; }

        public Qualification()
        {
            Languages = new List<QualificationLanguage>();
        }
    }
}