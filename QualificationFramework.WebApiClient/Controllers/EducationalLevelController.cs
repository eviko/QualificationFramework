using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using QualificationFramework.WebApiClient.Models;

namespace QualificationFramework.WebApiClient.Controllers
{
    public class EducationalLevelController : ApiController
    {
        private QualificationFrameworkContext db = new QualificationFrameworkContext();

        // GET: api/AwardingBodies/el
        [HttpGet]
        [Route("api/educationallevel/{language}")]
        public IList<EducationalLevel> GetEducationalLevels(string language = "el")
        {
            var dbEducationalLevels = db.EducationalLevels.Include("Languages").ToList();

            
            foreach (var educationalLevel in dbEducationalLevels)
            {
                educationalLevel.ActiveLanguage =
                    (from k in educationalLevel.Languages where k.Language == language select k).FirstOrDefault();
            }

            dbEducationalLevels.Add(new EducationalLevel
                { Id = -1, ActiveLanguage = new EducationalLevelLanguage { Language = language, Name = "" } });

            return (from k in dbEducationalLevels orderby k.Id select  k).ToList();
        }

        // GET: api/AwardingBodies/el/5
        [ResponseType(typeof(EducationalLevel))]
        [HttpGet]
        [Route("api/educationallevel/{language}/{id}")]
        public async Task<IHttpActionResult> GetEducationalLevel(int id, string language = "el")
        {
            EducationalLevel educationalLevel = await db.EducationalLevels.FindAsync(id);
            if (educationalLevel == null)
            {
                return NotFound();
            }

            
            educationalLevel.ActiveLanguage =
                (from k in db.EducationalLevelLanguages where k.EducationalLevelId == id && k.Language == language select k)
                .FirstOrDefault();
            return Ok(educationalLevel);
        }

        [HttpGet]
        [Route("api/educationallevel/removehtml")]
        public bool RemoveHtml()
        {
            var typeTrn = db.QualificationTypeLanguages.ToList();
            foreach (var qualificationTypeLanguage in typeTrn)
            {
                qualificationTypeLanguage.Name = RemoveHTML(qualificationTypeLanguage.Name);
                qualificationTypeLanguage.Description = RemoveHTML(qualificationTypeLanguage.Description);
                qualificationTypeLanguage.Competence = RemoveHTML(qualificationTypeLanguage.Competence);
                qualificationTypeLanguage.Knowledge = RemoveHTML(qualificationTypeLanguage.Knowledge);
                qualificationTypeLanguage.RelationToEmployment = RemoveHTML(qualificationTypeLanguage.RelationToEmployment);
                qualificationTypeLanguage.Transitions = RemoveHTML(qualificationTypeLanguage.Transitions);
                qualificationTypeLanguage.Skills = RemoveHTML(qualificationTypeLanguage.Skills);
                db.QualificationTypeLanguages.AddOrUpdate(qualificationTypeLanguage);                
            }

//            var qualTrn = db.QualificationLanguages.ToList();
//            foreach (var qualificationLanguage in qualTrn)
//            {
//                qualificationLanguage.Name = RemoveHTML(qualificationLanguage.Name);
//                qualificationLanguage.Description = RemoveHTML(qualificationLanguage.Description);
//                qualificationLanguage.Employment = RemoveHTML(qualificationLanguage.Employment);
//                db.QualificationLanguages.AddOrUpdate(qualificationLanguage);
//            }
            var qualTrn = db.EducationalSectorLanguages.ToList();
            foreach (var qualificationLanguage in qualTrn)
            {
                qualificationLanguage.Title = RemoveHTML(qualificationLanguage.Title);
                qualificationLanguage.Description = RemoveHTML(qualificationLanguage.Description);                
                db.EducationalSectorLanguages.AddOrUpdate(qualificationLanguage);
            }

            db.SaveChanges();
            
            return true;
        }

        public static string RemoveHTML(string strHTML)
        {
            if (string.IsNullOrEmpty(strHTML)) return "";
            return Regex.Replace(strHTML, "<(.|\n)*?>", "");
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