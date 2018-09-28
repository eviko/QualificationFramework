using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using QualificationFramework.WebApiClient.Models;

namespace QualificationFramework.WebApiClient.Controllers
{
    public class AwardingBodiesController : ApiController
    {
        private QualificationFrameworkContext db = new QualificationFrameworkContext();

        // GET: api/AwardingBodies/el3
        [HttpGet]
        [Route("api/awardingbodies/{language}")]
        public IList<AwardingBody> GetAwardingBodies(string language="el")
        {            
            var bodies = db.AwardingBodies.Include("Languages").ToList();
//            var translations = db.AwardingBodyLanguages;

            foreach (var awardingBody in bodies)
            {
                awardingBody.ActiveLanguage =
                    (from k in awardingBody.Languages where k.Language == language
                        && awardingBody.Id == k.AwardingBodyId select k).FirstOrDefault();
            }
            bodies.Add(new AwardingBody
                { Id = -1, ActiveLanguage = new AwardingBodyLanguage() { Language = language, Name = "" } });

            return (from k in bodies orderby k.Id select k).ToList();            
        }

        // GET: api/AwardingBodies/el/5
        [ResponseType(typeof(AwardingBody))]
        [HttpGet]
        [Route("api/awardingbodies/{language}/{id}")]
        public async Task<IHttpActionResult> GetAwardingBody(int id, string language = "el")
        {
            AwardingBody awardingBody = await db.AwardingBodies.FindAsync(id);
            if (awardingBody == null)
            {
                return NotFound();
            }

            awardingBody.ActiveLanguage =
                (from k in db.AwardingBodyLanguages where k.AwardingBodyId == id && k.Language == language select k)
                .FirstOrDefault();
            return Ok(awardingBody);
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