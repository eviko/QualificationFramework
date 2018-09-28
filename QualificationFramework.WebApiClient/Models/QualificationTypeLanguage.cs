using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class QualificationTypeLanguage
    {
        public int Id { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public QualificationType QualificationType { get; set; }
        public int QualificationTypeId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Volume { get; set; }
        public string Knowledge { get; set; }
        public string Skills { get; set; }
        public string Competence { get; set; }
        public string RelationToEmployment { get; set; }
        public string Transitions { get; set; }
    }
}