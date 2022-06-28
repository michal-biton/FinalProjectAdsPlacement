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
    public class PageController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Page
        public RequestResult Get()
        {
            return classDb.GetAllPages();
        }

        // GET: api/Page/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Page
        public void Post(PageDto p)
        {
            classDb.AddPage(p);
        }

        // PUT: api/Page/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Page/5
        public void Delete(PageDto p)
        {
            classDb.RemovePage(p);
        }
    }
}
