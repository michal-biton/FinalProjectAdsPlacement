using Bll;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AdsPlacement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PreferencePageController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Preference
        public RequestResult Get()
        {
            return classDb.GetAllPreferencesPage();
        }

        // GET: api/Preference/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Preference
        public void Post(PreferencePageDto p)
        {
            classDb.AddPreferencePage(p);
        }

        // PUT: api/Preference/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Preference/5
        public void Delete(PreferencePageDto p)
        {
            classDb.RemovePreferencePage(p);
        }
    }
}
