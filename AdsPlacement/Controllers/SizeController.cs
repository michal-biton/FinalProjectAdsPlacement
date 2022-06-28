using Bll;
using Bll.AlgorithmD;
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
    public class SizeController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Size
        public RequestResult Get()
        {
            return classDb.GetAllSizes();
        }

        // GET: api/Size/5
        [HttpGet]
        [Route("api/Size/{idSize}")]
        public RequestResult Get(int idSize)
        {
            return classDb.ReturnPreferens(idSize);
        }
        // POST: api/Size
        public void Post(SizeDto s)
        {
            classDb.AddSize(s);
        }

        // PUT: api/Size/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Size/5
        public void Delete(SizeDto s)
        {
            classDb.RemoveSize(s);
        }
    }
}
