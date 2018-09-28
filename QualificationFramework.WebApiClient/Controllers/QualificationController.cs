using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Util;
using System.Web.WebPages;
using QualificationFramework.WebApiClient.Models;

namespace QualificationFramework.WebApiClient.Controllers
{
    public class QualificationController : ApiController
    {
        private QualificationFrameworkContext db = new QualificationFrameworkContext();

        // GET: api/AwardingBodies/el
        [HttpGet]
        [Route("api/qualifications/{language}")]
        public IQueryable<Qualification> GetQualifications(string language = "el")
        {
            var qualifications = db.Qualifications.Include("Languages").Include("QualificationType").Include("EducationalLevel").Include("AwardingBody");
            var educationalLevelTranslations = db.EducationalLevelLanguages.ToList();
            var qualificationTypeTranslations = db.QualificationTypeLanguages.ToList();
            var awardingBodyTranslations = db.AwardingBodyLanguages.ToList();

            foreach (var qualification in qualifications)
            {
                qualification.ActiveLanguage =
                    (from k in qualification.Languages where k.Language == language select k).FirstOrDefault();
                if (qualification.EducationalLevel != null)
                {
                    qualification.EducationalLevel.ActiveLanguage =
                        (from k in educationalLevelTranslations
                         where k.Language == language && k.EducationalLevelId == qualification.EducationalLevelId
                         select k).FirstOrDefault();
                }

                if (qualification.QualificationType != null)
                {
                    qualification.QualificationType.ActiveLanguage =
                        (from k in qualificationTypeTranslations
                         where k.Language == language && k.QualificationTypeId == qualification.QualificationTypeId
                         select k).FirstOrDefault();
                }

                if (qualification.AwardingBody != null)
                {
                    qualification.AwardingBody.ActiveLanguage =
                        (from k in awardingBodyTranslations
                         where k.Language == language && k.AwardingBodyId == qualification.AwardingBodyId
                         select k).FirstOrDefault();
                }
            }

            return qualifications;
        }

        // GET: api/AwardingBodies/el/5
        [ResponseType(typeof(Qualification))]
        [HttpGet]
        [Route("api/qualifications/{language}/{id}")]
        public async Task<IHttpActionResult> GetQualification(int id, string language = "el")
        {
            var qualification = db.Qualifications.Where(x => x.Id == id).Include(x => x.EducationalLevel)
                .Include(x => x.QualificationType).Include(x => x.AwardingBody).Include(x => x.Languages).FirstOrDefault();
            if (qualification == null)
            {
                return NotFound();
            }

            qualification.ActiveLanguage =
                (from k in db.QualificationLanguages where k.QualificationId == id && k.Language == language select k)
                .FirstOrDefault();
            var educationalLevelTranslations = db.EducationalLevelLanguages.ToList();
            var qualificationTypeTranslations = db.QualificationTypeLanguages.ToList();
            var awardingBodyTranslations = db.AwardingBodyLanguages.ToList();
            if (qualification.EducationalLevel != null)
            {
                qualification.EducationalLevel.ActiveLanguage =
                    (from k in educationalLevelTranslations
                     where k.Language == language && k.EducationalLevelId == qualification.EducationalLevelId
                     select k).FirstOrDefault();
            }

            if (qualification.QualificationType != null)
            {
                qualification.QualificationType.ActiveLanguage =
                    (from k in qualificationTypeTranslations
                     where k.Language == language && k.QualificationTypeId == qualification.QualificationTypeId
                     select k).FirstOrDefault();
            }

            if (qualification.AwardingBody != null)
            {
                qualification.AwardingBody.ActiveLanguage =
                    (from k in awardingBodyTranslations
                     where k.Language == language && k.AwardingBodyId == qualification.AwardingBodyId
                     select k).FirstOrDefault();
            }
            return Ok(qualification);
        }


        [HttpGet]
        [Route("api/qualifications/filter/{language}/{levelId?}/{sectorId?}/{bodyId?}/{typeId?}/{text?}")]
        //Call Example: /api/qualifications/filter/el?levelId=35&text=some text&bodyId=89
        public IList<Qualification> GetQualificationsByFilter(string language = "el", int? sectorId = null, int? bodyId = null, int? typeId = null, int? levelId = null, string text = null)
        {
            var qualifications = db.Qualifications.Include("Languages").Include("QualificationType").Include("EducationalLevel").Include("AwardingBody").ToList();

            if (bodyId != null)
                qualifications = (from k in qualifications where k.AwardingBodyId == Convert.ToInt32(bodyId) select k).ToList();

            if (levelId != null)
                qualifications = (from k in qualifications where k.EducationalLevelId == Convert.ToInt32(levelId) select k).ToList();

            if (sectorId != null)
            {
                var actualSectorId = Convert.ToInt32(sectorId);
                var types = db.QualificationTypes.Where(x => x.EducationalSectorId == actualSectorId).ToList();
                qualifications = (from k in qualifications where types.Contains(k.QualificationType) select k).ToList();
            }


            if (typeId != null)
                qualifications = (from k in qualifications where k.QualificationTypeId == Convert.ToInt32(typeId) select k).ToList();


            var educationalLevelTranslations = db.EducationalLevelLanguages.ToList();
            var qualificationTypeTranslations = db.QualificationTypeLanguages.ToList();
            var awardingBodyTranslations = db.AwardingBodyLanguages.ToList();

            var finalQualifications = new List<Qualification>();


            foreach (var qualification in qualifications)
            {
                qualification.ActiveLanguage =
                    (from k in qualification.Languages where k.Language == language select k).FirstOrDefault();

                if (qualification.ActiveLanguage == null) continue;

                if (!string.IsNullOrEmpty(text))
                {
                    var cleanedSearchText = CleanTextForLanguage(text, language);
                    var cleanedName = CleanTextForLanguage(qualification.ActiveLanguage.Name, language);
                    var cleanedDescription = CleanTextForLanguage(qualification.ActiveLanguage.Description, language);
                    var toBeIncluded = ContainsFilter(cleanedName, cleanedSearchText) || ContainsFilter(cleanedDescription, cleanedSearchText);
                    if (!toBeIncluded) continue;
                }

                if (qualification.EducationalLevel != null)
                {
                    qualification.EducationalLevel.ActiveLanguage =
                        (from k in educationalLevelTranslations
                         where k.Language == language && k.EducationalLevelId == qualification.EducationalLevelId
                         select k).FirstOrDefault();
                }

                if (qualification.QualificationType != null)
                {
                    qualification.QualificationType.ActiveLanguage =
                        (from k in qualificationTypeTranslations
                         where k.Language == language && k.QualificationTypeId == qualification.QualificationTypeId
                         select k).FirstOrDefault();
                }

                if (qualification.AwardingBody != null)
                {
                    qualification.AwardingBody.ActiveLanguage =
                        (from k in awardingBodyTranslations
                         where k.Language == language && k.AwardingBodyId == qualification.AwardingBodyId
                         select k).FirstOrDefault();
                }

                finalQualifications.Add(qualification);
            }

            return finalQualifications;
        }

        private IList<string> CleanTextForLanguage(string text, string language = "el")
        {
            var finalWords = new List<string>();

            if (language == "el")
            {
                var tolower = text.ToLower();
                //                var regex = new Regex(@"[^\p{L}]*\p{Z}[^\p{L}]*");
                //                var words = regex.Split(tolower).ToList();
                string[] words = tolower.Split(new[] {',', ' ', '.', ':', ';', '+', '-', '&'});

                foreach (var word in words)
                {                    
                    var trimmedWord = word.Trim();
                    if (string.IsNullOrEmpty(trimmedWord) || trimmedWord.Length <4) continue;
                    
                    if (GreekLanguageRemovedArticles.ContainsKey(trimmedWord))
                        continue;


                    var wordToAdd = trimmedWord;
                    if (wordToAdd.EndsWith("ς"))
                        wordToAdd = wordToAdd.Substring(0, wordToAdd.Length - 1);
                    foreach (var greekLanguageAlteredChar in GreekLanguageAlteredChars)
                    {
                        wordToAdd = wordToAdd.Replace(greekLanguageAlteredChar.Key, greekLanguageAlteredChar.Value);
                    }

                    finalWords.Add(wordToAdd);
                }
            }

            return finalWords;
        }

        private bool ContainsFilter(IList<string> source, IList<string> target)
        {
            foreach (var targetWord in target)
            {
                if (source.Contains(targetWord)) return true;
            }

            return false;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private static Dictionary<char, char> GreekLanguageAlteredChars
        {
            get
            {
                var newDict = new Dictionary<char, char>();
                newDict.Add('ά', 'α');
                newDict.Add('έ', 'ε');
                newDict.Add('ή', 'η');
                newDict.Add('ί', 'ι');
                newDict.Add('ό', 'ο');
                newDict.Add('ύ', 'υ');
                newDict.Add('ώ', 'ω');
                newDict.Add('.', ' ');
                newDict.Add(',', ' ');
                newDict.Add(':', ' ');
                newDict.Add(';', ' ');                
                return newDict;
            }
        }
        private static Dictionary<string, string> GreekLanguageRemovedArticles
        {
            get
            {
                var newDict = new Dictionary<string, string>();
                newDict.Add("το","");
                newDict.Add("τα", "");
                newDict.Add("ο", "");
                newDict.Add("η", "");
                newDict.Add("του", "");
                newDict.Add("την", "");
                newDict.Add("της", "");                
                return newDict;
            }
        }
    }
}