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
    public class DealController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Deal
        public RequestResult Get()
        {
            return classDb.GetAllDeals();
        }

        // GET: api/Deal/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Deal
        public void Post(DealDto d)
        {
            classDb.AddDeal(d);
        }

        // PUT: api/Deal/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Deal/5
        public void Delete(int id)
        {
        }
    }
}
