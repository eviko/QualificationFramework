using System.Data.Entity.ModelConfiguration;

namespace QualificationFramework.WebApiClient.Models.Mappings
{
    public class AwardingBodyMap : EntityTypeConfiguration<AwardingBody>
    {
        public AwardingBodyMap()
        {
            HasKey(x => x.Id);
            Ignore(x => x.ActiveLanguage);
            HasMany(x => x.QualificationTypes).WithMany(x => x.AwardingBodies).Map(s =>
            {
                s.MapLeftKey("AwardingBodyId");
                s.MapRightKey("QualificationTypeId");
                s.ToTable("AwardingBodyQualification");
            });
            HasMany(x => x.Languages).WithRequired(x => x.AwardingBody).HasForeignKey(x => x.AwardingBodyId)
                .WillCascadeOnDelete(true);
        }
    }
}