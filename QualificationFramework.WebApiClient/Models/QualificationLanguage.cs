using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class QualificationLanguage
    {
        public int Id { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]

        public Qualification Qualification { get; set; }
        public int QualificationId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Employment { get; set; }

    }
}