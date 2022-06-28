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
    public class AdController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Ad
        public RequestResult Get()
        {
            return classDb.GetAllAdsNEmb();
        }
        // GET: api/Ad/5
        [HttpGet]
        [Route("api/Ad/{idUser}")]
        public RequestResult Get(int idUser)
        {
            return classDb.FindAdToUser(idUser);
        }
        // POST: api/Ad
        public void Post(AdDto a)
        {
            classDb.AddAd(a);
        }
        // PUT: api/Ad/5
        [HttpPut]
        [Route("api/Ad")]
        public void Put([FromBody]AdDto a)
        {
            classDb.UpdateAd(a);
        }

        // DELETE: api/Ad/5
        [HttpDelete]
        [Route("api/Ad/{a}")]
        public void Delete(int a)
        {
            classDb.RemoveAd(a);
        }
    }
}
