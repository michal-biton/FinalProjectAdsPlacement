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
    public class SheetTypeController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/SheetType
        public RequestResult Get()
        {
            return classDb.GetAllSheetTypes();
        }

        // GET: api/SheetType/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SheetType
        public void Post(SheetTypeDto s)
        {
            classDb.AddSheetType(s);
        }

        // PUT: api/SheetType/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SheetType/5
        public void Delete(SheetTypeDto st)
        {
            classDb.RemoveSheetType(st);
        }
    }
}
