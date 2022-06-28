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
    public class SheetController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Sheet
        public RequestResult Get()
        {
            return classDb.GetAllSheets();
        }

        // GET: api/Sheet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sheet
        public void Post(SheetDto s)
        {
            classDb.AddSheet(s);
        }

        // PUT: api/Sheet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sheet/5
        public void Delete(SheetDto s)
        {
            classDb.RemoveSheet(s);
        }
    }
}
