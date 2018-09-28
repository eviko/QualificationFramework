using System.Data.Entity.ModelConfiguration;

namespace QualificationFramework.WebApiClient.Models.Mappings
{
    public class AwardingBodyLanguageMap : EntityTypeConfiguration<AwardingBodyLanguage>
    {
        public AwardingBodyLanguageMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Language).HasMaxLength(10);
            Property(x => x.Url).HasMaxLength(200);
            Property(x => x.Name).HasMaxLength(8000);
        }
    }
}