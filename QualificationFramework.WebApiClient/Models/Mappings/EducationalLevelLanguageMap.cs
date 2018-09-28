using System.Data.Entity.ModelConfiguration;

namespace QualificationFramework.WebApiClient.Models.Mappings
{
    public class EducationalLevelLanguageMap : EntityTypeConfiguration<EducationalLevelLanguage>
    {
        public EducationalLevelLanguageMap()
        {
            HasKey(x => x.Id);            
            Property(x => x.Language).HasMaxLength(10);
            Property(x => x.Name).HasMaxLength(8000);
            Property(x => x.Skills).HasMaxLength(8000);
            Property(x => x.Knowledge).HasMaxLength(8000);
            Property(x => x.Competence).HasMaxLength(8000);
        }
    }
}