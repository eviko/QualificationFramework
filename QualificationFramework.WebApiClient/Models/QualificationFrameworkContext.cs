using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using QualificationFramework.WebApiClient.Migrations;
using QualificationFramework.WebApiClient.Models.Mappings;

namespace QualificationFramework.WebApiClient.Models
{
    public class QualificationFrameworkContext : DbContext
    {
        
        public QualificationFrameworkContext() : base("Name = QualificationFramework")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QualificationFrameworkContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new EducationalLevelMap());
            modelBuilder.Configurations.Add(new EducationalLevelLanguageMap());
            modelBuilder.Configurations.Add(new EducationalSectorMap());
            modelBuilder.Configurations.Add(new EducationalSectorLanguageMap());
            modelBuilder.Configurations.Add(new AwardingBodyMap());
            modelBuilder.Configurations.Add(new AwardingBodyLanguageMap());
            modelBuilder.Configurations.Add(new QualificationTypeMap());
            modelBuilder.Configurations.Add(new QualificationTypeLanguageMap());
            modelBuilder.Configurations.Add(new QualificationMap());
            modelBuilder.Configurations.Add(new QualificationLanguageMap());
        }


        public DbSet<AwardingBody> AwardingBodies { get; set; }

        public DbSet<AwardingBodyLanguage> AwardingBodyLanguages { get; set; }

        public DbSet<QualificationType> QualificationTypes { get; set; }
        public DbSet<QualificationTypeLanguage> QualificationTypeLanguages { get; set; }

        public DbSet<EducationalLevel> EducationalLevels { get; set; }
        public DbSet<EducationalLevelLanguage> EducationalLevelLanguages { get; set; }

        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<QualificationLanguage> QualificationLanguages { get; set; }

        public DbSet<EducationalSector> EducationalSectors { get; set; }
        public DbSet<EducationalSectorLanguage> EducationalSectorLanguages { get; set; }

    }
}