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
    public class PriorityController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Priority
        public RequestResult Get()
        {
            return classDb.GetAllPrioritys();
        }

        // GET: api/Priority/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Priority
        public void Post(PriorityDto p)
        {
            classDb.AddPriority(p);
        }

        // PUT: api/Priority/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Priority/5
        public void Delete(PriorityDto p)
        {
            classDb.RemovePriority(p);
        }
    }
}
