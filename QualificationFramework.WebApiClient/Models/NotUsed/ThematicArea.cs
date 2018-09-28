using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualificationFramework.WebApiClient.Models
{
    [Table("ThematicArea")]
    public class ThematicArea
    {
        [Key]
        public int Id { get; set; }

        public string ParentCode { get; set; }
        public string Code { get; set; }

        [NotMapped]
        public ThematicAreaLanguage ActiveLanguage { get; set; }
        public IList<ThematicAreaLanguage> Languages { get; set; }

        public ThematicArea()
        {
            Languages = new List<ThematicAreaLanguage>();
        }
    }
}