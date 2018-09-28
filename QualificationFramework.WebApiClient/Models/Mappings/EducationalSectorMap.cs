using System.Data.Entity.ModelConfiguration;

namespace QualificationFramework.WebApiClient.Models.Mappings
{
    public class EducationalSectorMap : EntityTypeConfiguration<EducationalSector>
    {
        public EducationalSectorMap()
        {
            HasKey(x => x.Id);
            Ignore(x => x.ActiveLanguage);
            HasMany(x => x.Languages).WithRequired(x => x.EducationalSector).HasForeignKey(x => x.EducationalSectorId)
                .WillCascadeOnDelete(true);
        }
    }
}