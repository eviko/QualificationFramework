using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QualificationFramework.WebApiClient.Models
{
    public class AwardingBodyLanguage
    {
        public int Id { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]

        public AwardingBody AwardingBody { get; set; }
        public int AwardingBodyId { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
    }
}