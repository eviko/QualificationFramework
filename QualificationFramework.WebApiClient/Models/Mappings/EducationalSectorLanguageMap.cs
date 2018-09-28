using System.Data.Entity.ModelConfiguration;

namespace QualificationFramework.WebApiClient.Models.Mappings
{
    public class EducationalSectorLanguageMap : EntityTypeConfiguration<EducationalSectorLanguage>
    {
        public EducationalSectorLanguageMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Language).HasMaxLength(10);
            Property(x => x.Title).HasMaxLength(1000);
            Property(x => x.Description).HasMaxLength(8000);            
        }
    }
}