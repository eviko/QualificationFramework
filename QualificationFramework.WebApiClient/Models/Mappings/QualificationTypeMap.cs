using System.Data.Entity.ModelConfiguration;

namespace QualificationFramework.WebApiClient.Models.Mappings
{
    public class QualificationTypeMap : EntityTypeConfiguration<QualificationType>
    {
        public QualificationTypeMap()
        {
            HasKey(x => x.Id);
            Ignore(x => x.ActiveLanguage);
            HasMany(x => x.AwardingBodies).WithMany(x => x.QualificationTypes).Map(s =>
            {
                s.MapLeftKey("AwardingBodyId");
                s.MapRightKey("QualificationTypeId");
                s.ToTable("AwardingBodyQualification");
            });
            HasMany(x => x.Languages).WithRequired(x => x.QualificationType).HasForeignKey(x => x.QualificationTypeId)
                .WillCascadeOnDelete(true);
        }
    }
}