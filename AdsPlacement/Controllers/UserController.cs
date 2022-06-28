using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Bll;
using Dto;

namespace AdsPlacement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/User
        public RequestResult Get()
        {
            return classDb.GetAllUsers();
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public void Post(UserDto u)
        {
            classDb.AddUser(u);
        }

        [HttpGet]
        [Route("api/User/{password}/{name}")]
        public UserDto Get(string password, string name)
        {
            UserDto u = classDb.FindUser(password, name);
            return u;
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(UserDto u)
        {
            classDb.RemoveUser(u);
        }
    }
}
