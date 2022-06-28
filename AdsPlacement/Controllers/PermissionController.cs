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
    public class PermissionController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Permission
        public RequestResult Get()
        {
            return classDb.GetAllPermissions();
        }

        // GET: api/Permission/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Permission
        public void Post(PermissionDto p)
        {
            classDb.AddPermission(p);
        }

        // PUT: api/Permission/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Permission/5
        public void Delete(PermissionDto p)
        {
            classDb.RemovePermission(p);
        }
    }
}
