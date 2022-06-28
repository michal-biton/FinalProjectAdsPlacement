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
    public class CategoryController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Category
        public RequestResult Get()
        {
            return classDb.GetAllCategorys();
        }

        // GET: api/Category/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Category
        public void Post(CategoryDto c)
        {
            classDb.AddCategory(c);
        }

        // PUT: api/Category/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Category/5
        public void Delete(CategoryDto c)
        {
            classDb.RemoveCategory(c);
        }
    }
}
