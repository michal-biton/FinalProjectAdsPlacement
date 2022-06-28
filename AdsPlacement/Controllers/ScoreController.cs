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
    public class ScoreController : ApiController
    {
        ClassDb classDb = new ClassDb();
        // GET: api/Score
        public RequestResult Get()
        {
            return classDb.GetAllScores();
        }
        // GET: api/Score/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Score
        public void Post(ScoreDto s)
        {
            classDb.AddScore(s);
        }

        // PUT: api/Score/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Score/5
        public void Delete(int id)
        {
        }
    }
}
