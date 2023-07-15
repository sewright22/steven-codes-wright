using FJA_Pool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LeaderboardController : ApiController
    {
        [Authorize]
        public IHttpActionResult Get()
        {
            var retVal = new List<object>();

            using (var dbContext = new FjaPoolContext())
            {
                var bracketPicks = from b in dbContext.BracketPicks
                                   where b.IsMaster == false
                                   orderby b.Score descending, b.MaxScore descending, b.Name
                                   select new { b.Id, b.Name, b.Score, b.MaxScore };

                retVal.AddRange(bracketPicks.ToList());
            }

            return Ok(retVal);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}