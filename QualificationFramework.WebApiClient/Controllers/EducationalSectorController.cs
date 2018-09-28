using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using QualificationFramework.WebApiClient.Models;

namespace QualificationFramework.WebApiClient.Controllers
{
    public class EducationalSectorController : ApiController
    {
        // GET: EducationalSector
        private QualificationFrameworkContext db = new QualificationFrameworkContext();

        // GET: api/AwardingBodies/el
        [HttpGet]
        [Route("api/sector/{language}")]
        public IList<EducationalSector> GetEducationalSectors(string language = "el")
        {
            var dbEducationalSectors = db.EducationalSectors.Include("Languages").ToList();            

            foreach (var educationalSector in dbEducationalSectors)
            {
                educationalSector.ActiveLanguage =
                    (from k in educationalSector.Languages where k.Language == language select k).FirstOrDefault();
            }
            dbEducationalSectors.Add(new EducationalSector
                { Id = -1, ActiveLanguage = new EducationalSectorLanguage() { Language = language, Title = "" } });

            return (from k in dbEducationalSectors orderby k.Id select k).ToList();            
        }

        // GET: api/AwardingBodies/el/5
        [ResponseType(typeof(EducationalSector))]
        [HttpGet]
        [Route("api/sector/{language}/{id}")]
        public async Task<IHttpActionResult> GetEducationalSector(int id, string language = "el")
        {
            EducationalSector educationalSector = await db.EducationalSectors.FindAsync(id);
            if (educationalSector == null)
            {
                return NotFound();
            }

            educationalSector.ActiveLanguage =
                (from k in db.EducationalSectorLanguages where k.EducationalSectorId == id && k.Language == language select k)
                .FirstOrDefault();
            return Ok(educationalSector);
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