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
    public class DealTypeController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/DealType
        public RequestResult Get()
        {
            return classDb.GetAllDealTypes();
        }

        // GET: api/DealType/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DealType
        public void Post(DealTypeDto dt)
        {
            classDb.AddDealType(dt);
        }

        // PUT: api/DealType/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DealType/5
        public void Delete(DealTypeDto d)
        {
            classDb.RemoveDealType(d);
        }
    }
}
