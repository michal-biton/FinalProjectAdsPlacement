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
    public class AdImageController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/AdImage
        [HttpGet]
        [Route("api/AdImage/{fillPath}")]
        public int Get(string fillPath)
        {
            return classDb.FindImage(fillPath);
        }
        // GET: api/AdImage/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AdImage
        public void Post(AdImageDto ai)
        {
            classDb.AddAdImage(ai);
        }

        // PUT: api/AdImage/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AdImage/5
        public void Delete(AdImageDto ai)
        {
            classDb.RemoveAdImage(ai);
        }
    }
}
