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
    public class PlacementController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Placement
        public RequestResult Get()
        {
            return classDb.GetAllPlacement();
        }

        // GET: api/Placement/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Placement
        public void Post(PlacementDto p)
        {
            classDb.AddPlacement(p);
        }

        // PUT: api/Placement/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Placement/5
        public void Delete(PlacementDto p)
        {
            classDb.RemovePlacement(p);
        }
    }
}
