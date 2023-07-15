using FJA_Pool.Data;
using FJA_Pool.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TeamController : ApiController
    {
        [Authorize]
        public IHttpActionResult Get(string season)
        {
            var retVal = new List<TeamEntity>();

            using (var dbContect = new FjaPoolContext())
            {
                retVal.AddRange(dbContect.Teams.Where(x=>x.Season==season).ToList());
            }

            return Ok(retVal);
        }

        // GET api/<controller>/5
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize]
        // POST api/<controller>
        public void Post([FromBody]TeamEntity team)
        {
            try
            {
                using (var dbContext = new FjaPoolContext())
                {
                    dbContext.Teams.Add(team);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // PUT api/<controller>/5
        [Authorize]
        public void Put(int id, [FromBody]TeamEntity team)
        {
            try
            {
                using (var dbContext = new FjaPoolContext())
                {
                    var fromDB = dbContext.Teams.Find(id);

                    if (fromDB != null)
                    {
                        dbContext.Entry(fromDB).CurrentValues.SetValues(team);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // DELETE api/<controller>/5
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}