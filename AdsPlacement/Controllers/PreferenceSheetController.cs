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
    public class PreferenceSheetController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/PreferenceSheet
        public RequestResult Get()
        {
            return classDb.GetAllPreferencesSheet();
        }

        // GET: api/PreferenceSheet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PreferenceSheet
        public void Post(PreferenceSheetDto p)
        {
            classDb.AddPreferenceSheet(p);
        }

        // PUT: api/PreferenceSheet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PreferenceSheet/5
        public void Delete(PreferenceSheetDto p)
        {
            classDb.RemovePreferenceSheet(p);
        }
    }
}
