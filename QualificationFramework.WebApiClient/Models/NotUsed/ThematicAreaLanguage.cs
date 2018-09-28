using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualificationFramework.WebApiClient.Models
{
    [Table("ThematicAreaLanguage")]
    public class ThematicAreaLanguage
    {
        [Key]
        public int Id { get; set; }

        public int ThematicAreaId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
    }
}