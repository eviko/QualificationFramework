using System.Data.Entity.ModelConfiguration;

namespace QualificationFramework.WebApiClient.Models.Mappings
{
    public class QualificationLanguageMap : EntityTypeConfiguration<QualificationLanguage>
    {
        public QualificationLanguageMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Language).HasMaxLength(10);
            Property(x => x.Name).HasMaxLength(80000);
            Property(x => x.Description).HasMaxLength(80000);
            Property(x => x.Employment).HasMaxLength(80000);            
        }
    }
}