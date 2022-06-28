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
    public class PageTypeController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/PageType
        public RequestResult Get()
        {
            return classDb.GetAllPageTypes();
        }

        // GET: api/PageType/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PageType
        public void Post(PageTypeDto pt)
        {
            classDb.AddPageType(pt);
        }

        // PUT: api/PageType/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PageType/5
        public void Delete(PageTypeDto pt)
        {
            classDb.RemovePageType(pt);
        }
    }
}
