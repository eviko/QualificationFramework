using System.Data.Entity.ModelConfiguration;

namespace QualificationFramework.WebApiClient.Models.Mappings
{
    public class EducationalLevelMap : EntityTypeConfiguration<EducationalLevel>
    {
        public EducationalLevelMap()
        {
            HasKey(x => x.Id);
            Ignore(x => x.ActiveLanguage);
            Property(x => x.LevelId);
            Property(x => x.EQFLevel);
            HasMany(x => x.Languages).WithRequired(x => x.EducationalLevel).HasForeignKey(x => x.EducationalLevelId)
                .WillCascadeOnDelete(true);
        }
    }
}