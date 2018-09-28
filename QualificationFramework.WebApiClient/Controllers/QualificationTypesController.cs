using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using QualificationFramework.WebApiClient.Models;

namespace QualificationFramework.WebApiClient.Controllers
{
    public class QualificationTypesController : ApiController
    {
        private QualificationFrameworkContext db = new QualificationFrameworkContext();

        // GET: api/AwardingBodies/el
        [HttpGet]
        [Route("api/qualificationtype/{language}")]
        public IList<QualificationType> GetQualificationTypes(string language = "el")
        {
            try
            {
                var dbQualificationTypes = db.QualificationTypes.Include("Languages").Include("EducationalLevel").Include("EducationalSector").ToList();
                //                var translations = db.QualificationTypeLanguages;
                var educationalLevelTranslations = db.EducationalLevelLanguages.ToList();
                var educationalSectorTranslations = db.EducationalSectorLanguages.ToList();
                
                foreach (var qualificationType in dbQualificationTypes)
                {
                    qualificationType.ActiveLanguage =
                        (from k in qualificationType.Languages where k.Language == language select k).FirstOrDefault();

                    if (qualificationType.EducationalLevel != null)
                    {
                        qualificationType.EducationalLevel.ActiveLanguage =
                         (from k in educationalLevelTranslations
                          where k.Language == language && k.EducationalLevelId == qualificationType.EducationalLevelId
                          select k).FirstOrDefault();
                    }

                    if (qualificationType.EducationalSector != null)
                    {
                        qualificationType.EducationalSector.ActiveLanguage =
                            (from k in educationalSectorTranslations
                             where k.Language == language && k.EducationalSectorId == qualificationType.EducationalSectorId
                             select k).FirstOrDefault();
                    }
                }



                dbQualificationTypes.Add(new QualificationType
                    { Id = -1, ActiveLanguage = new QualificationTypeLanguage() { Language = language, Name = "" } });

                return (from k in dbQualificationTypes orderby k.Id select k).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/AwardingBodies/el/5
        [HttpGet]
        [Route("api/qualificationtype/{language}/{id}")]
        [ResponseType(typeof(QualificationType))]
        public async Task<IHttpActionResult> GetQualificationType(int id, string language = "el")
        {
            var qualificationType = db.QualificationTypes.Where(x => x.Id == id).Include(x => x.EducationalLevel)
                .Include(x => x.EducationalSector).Include(x => x.Languages).FirstOrDefault();
//            QualificationType qualificationType = await db.QualificationTypes.FindAsync(id);
            if (qualificationType == null)
            {
                return NotFound();
            }

            qualificationType.ActiveLanguage =
                (from k in db.QualificationTypeLanguages where k.QualificationTypeId == id && k.Language == language select k)
                .FirstOrDefault();

            var educationalLevelTranslations = db.EducationalLevelLanguages.ToList();
            var educationalSectorTranslations = db.EducationalSectorLanguages.ToList();


            if (qualificationType.EducationalLevel != null)
            {
                qualificationType.EducationalLevel.ActiveLanguage =
                    (from k in educationalLevelTranslations
                     where k.Language == language && k.EducationalLevelId == qualificationType.EducationalLevelId
                     select k).FirstOrDefault();
            }

            if (qualificationType.EducationalSector != null)
            {
                qualificationType.EducationalSector.ActiveLanguage =
                    (from k in educationalSectorTranslations
                     where k.Language == language && k.EducationalSectorId == qualificationType.EducationalSectorId
                     select k).FirstOrDefault();
            }
            return Ok(qualificationType);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}