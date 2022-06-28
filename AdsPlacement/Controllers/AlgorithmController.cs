using Bll;
using Bll.AlgorithmD;
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

    public class AlgorithmController : ApiController
    {
        AlgoritemPlacement a = new AlgoritemPlacement();
        // GET: api/Algorithm
        public ToPlacement[] Get()
        {
            a.FindPossibleLocations();
            a.CopyPlacementsDic();
            a.StartPlacement();
            a.FillHole();
            a.db.SaveChanges();
            ToPlacement[] arr = a.OptimalPlacementSelection(a.Sheet.IDSheet);
            return arr;
        }

        // GET: api/Algorithm/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Algorithm
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Algorithm/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Algorithm/5
        public void Delete(int id)
        {
        }
    }
}
