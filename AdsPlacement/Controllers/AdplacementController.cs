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
    public class AdplacementController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Adplacement
        public RequestResult Get()
        {
            return classDb.GetAllAdplacement();
        }

        // GET: api/Adplacement/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Adplacement
        public void Post(AdplacementDto ap)
        {
            classDb.AddAdplacement(ap);
        }

        // PUT: api/Adplacement/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Adplacement/5
        public void Delete(AdplacementDto ap)
        {
            classDb.RemoveAdplacement(ap);
        }
    }
}
