using System.Data.Entity.ModelConfiguration;

namespace QualificationFramework.WebApiClient.Models.Mappings
{
    public class QualificationMap : EntityTypeConfiguration<Qualification>
    {
        public QualificationMap()
        {
            HasKey(x => x.Id);
            Ignore(x => x.ActiveLanguage);
            HasOptional(x => x.QualificationType).WithMany().HasForeignKey(x => x.QualificationTypeId)
                .WillCascadeOnDelete(false);
            HasOptional(x => x.EducationalLevel).WithMany().HasForeignKey(x => x.EducationalLevelId)
                .WillCascadeOnDelete(false);
            HasOptional(x => x.AwardingBody).WithMany().HasForeignKey(x => x.AwardingBodyId)
                .WillCascadeOnDelete(false);
            HasMany(x => x.Languages).WithRequired(x => x.Qualification).HasForeignKey(x => x.QualificationId)
                .WillCascadeOnDelete(true);
        }
    }
}