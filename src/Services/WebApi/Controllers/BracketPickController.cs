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
    public class BracketPickController : ApiController
    {
        [Authorize]
        public IHttpActionResult Get(string email, bool isMaster = false)
        {
            var retVal = new List<object>();

            if (isMaster)
            {
                using (var dbContect = new FjaPoolContext())
                {
                    var masterBracket = dbContect.BracketPicks.FirstOrDefault(x => x.Season == "2018" && x.IsMaster);

                    if (retVal == null)
                    {
                        return Ok(new BracketPickEntity() { Id = 0 });
                    }
                    else
                    {
                        return Ok(masterBracket);
                    }
                }
            }
            else
            {
                using (var dbContext = new FjaPoolContext())
                {
                    var bracketPicks = from b in dbContext.BracketPicks.AsNoTracking()
                                       join t in dbContext.Teams on b.SuperBowlWinnerId equals t.Id
                                       where b.UserEmail.ToUpper() == email.ToUpper()
                                       select new { b.Name, b.Id, b.Score, b.MaxScore, WinnerName = t.Name };

                    retVal.AddRange(bracketPicks.ToList());
                }
            }

            return Ok(retVal);
        }

        // GET api/<controller>/5
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var retVal = new BracketPickEntity();

            using (var dbContect = new FjaPoolContext())
            {
                retVal = dbContect.BracketPicks.FirstOrDefault(x => x.Id == id);
            }

            return Ok(retVal);
        }

        // POST api/<controller>
        [Authorize]
        public IHttpActionResult Post([FromBody]BracketPickEntity bracket)
        {
            try
            {
                using (var dbContext = new FjaPoolContext())
                {
                    var fromDB = dbContext.BracketPicks.FirstOrDefault(x => x.Id == bracket.Id);

                    if (fromDB != null)
                    {
                        fromDB.AFCWildCardGame1WinnerId = bracket.AFCWildCardGame1WinnerId;
                        fromDB.AFCWildCardGame2WinnerId = bracket.AFCWildCardGame2WinnerId;
                        fromDB.NFCWildCardGame1WinnerId = bracket.NFCWildCardGame1WinnerId;
                        fromDB.NFCWildCardGame2WinnerId = bracket.NFCWildCardGame2WinnerId;
                        fromDB.AFCDivisionalGame1WinnerId = bracket.AFCDivisionalGame1WinnerId;
                        fromDB.AFCDivisionalGame2WinnerId = bracket.AFCDivisionalGame2WinnerId;
                        fromDB.NFCDivisionalGame1WinnerId = bracket.NFCDivisionalGame1WinnerId;
                        fromDB.NFCDivisionalGame2WinnerId = bracket.NFCDivisionalGame2WinnerId;
                        fromDB.AFCChampionshipWinnerId = bracket.AFCChampionshipWinnerId;
                        fromDB.NFCChampionshipWinnerId = bracket.NFCChampionshipWinnerId;
                        fromDB.SuperBowlWinnerId = bracket.SuperBowlWinnerId;
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        dbContext.BracketPicks.Add(bracket);
                        dbContext.SaveChanges();
                    }
                }

                if (bracket.IsMaster)
                {
                    UpdateScores(bracket);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        private void UpdateScores(BracketPickEntity masterBracket)
        {
            var wildcardScore = 2;
            var conferenceSemiFinalScore = 3;
            var conferenceFinalScore = 5;
            var superBowlScore = 8;
            var maxPossibleScore = 4 * wildcardScore + 4 * conferenceSemiFinalScore + 2 * conferenceFinalScore + superBowlScore;

            using (var dbContext = new FjaPoolContext())
            {
                var brackets = dbContext.BracketPicks.AsNoTracking().Where(x => x.Season == masterBracket.Season && x.IsMaster == false).ToList();

                if (brackets.Any())
                {
                    foreach (var bracket in brackets)
                    {
                        var correctScore = 0;
                        var incorrectScore = 0;
                        var eliminatedTeams = new List<int>();

                        if (masterBracket.AFCWildCardGame1WinnerId.HasValue)
                        {
                            if (bracket.AFCWildCardGame1WinnerId == masterBracket.AFCWildCardGame1WinnerId)
                            {
                                correctScore += wildcardScore;
                            }
                            else
                            {
                                incorrectScore += wildcardScore;
                                eliminatedTeams.Add(bracket.AFCWildCardGame1WinnerId.Value);
                            }
                        }

                        if (masterBracket.AFCWildCardGame2WinnerId.HasValue)
                        {
                            if (bracket.AFCWildCardGame2WinnerId == masterBracket.AFCWildCardGame2WinnerId)
                            {
                                correctScore += wildcardScore;
                            }
                            else
                            {
                                incorrectScore += wildcardScore;
                                eliminatedTeams.Add(bracket.AFCWildCardGame2WinnerId.Value);
                            }
                        }

                        if (masterBracket.NFCWildCardGame1WinnerId.HasValue)
                        {
                            if (bracket.NFCWildCardGame1WinnerId == masterBracket.NFCWildCardGame1WinnerId)
                            {
                                correctScore += wildcardScore;
                            }
                            else
                            {
                                incorrectScore += wildcardScore;
                                eliminatedTeams.Add(bracket.NFCWildCardGame1WinnerId.Value);
                            }
                        }

                        if (masterBracket.NFCWildCardGame2WinnerId.HasValue)
                        {
                            if (bracket.NFCWildCardGame2WinnerId == masterBracket.NFCWildCardGame2WinnerId)
                            {
                                correctScore += wildcardScore;
                            }
                            else
                            {
                                incorrectScore += wildcardScore;
                                eliminatedTeams.Add(bracket.NFCWildCardGame2WinnerId.Value);
                            }
                        }

                        if (masterBracket.AFCDivisionalGame1WinnerId.HasValue && masterBracket.AFCDivisionalGame2WinnerId.HasValue)
                        {
                            if (bracket.AFCDivisionalGame1WinnerId == masterBracket.AFCDivisionalGame1WinnerId || 
                                bracket.AFCDivisionalGame1WinnerId == masterBracket.AFCDivisionalGame2WinnerId)
                            {
                                correctScore += conferenceSemiFinalScore;
                            }
                            else
                            {
                                incorrectScore += conferenceSemiFinalScore;
                                eliminatedTeams.Add(bracket.AFCDivisionalGame1WinnerId.Value);
                            }


                            if (bracket.AFCDivisionalGame2WinnerId == masterBracket.AFCDivisionalGame1WinnerId ||
                                bracket.AFCDivisionalGame2WinnerId == masterBracket.AFCDivisionalGame2WinnerId)
                            {
                                correctScore += conferenceSemiFinalScore;
                            }
                            else
                            {
                                incorrectScore += conferenceSemiFinalScore;
                                eliminatedTeams.Add(bracket.AFCDivisionalGame2WinnerId.Value);
                            }
                        }
                        else
                        {
                            if (masterBracket.AFCDivisionalGame1WinnerId.HasValue)
                            {
                                if (bracket.AFCDivisionalGame1WinnerId == masterBracket.AFCDivisionalGame1WinnerId)
                                {
                                    correctScore += conferenceSemiFinalScore;
                                }
                                else
                                {
                                    incorrectScore += conferenceSemiFinalScore;
                                    eliminatedTeams.Add(bracket.AFCDivisionalGame1WinnerId.Value);
                                }
                            }
                            else
                            {
                                if (eliminatedTeams.FirstOrDefault(x => x == bracket.AFCDivisionalGame1WinnerId) > 0)
                                {
                                    incorrectScore += conferenceSemiFinalScore;
                                }
                            }

                            if (masterBracket.AFCDivisionalGame2WinnerId.HasValue)
                            {
                                if (bracket.AFCDivisionalGame2WinnerId == masterBracket.AFCDivisionalGame2WinnerId)
                                {
                                    correctScore += conferenceSemiFinalScore;
                                }
                                else
                                {
                                    incorrectScore += conferenceSemiFinalScore;
                                    eliminatedTeams.Add(bracket.AFCDivisionalGame2WinnerId.Value);
                                }
                            }
                            else
                            {
                                if (eliminatedTeams.FirstOrDefault(x => x == bracket.AFCDivisionalGame2WinnerId) > 0)
                                {
                                    incorrectScore += conferenceSemiFinalScore;
                                }
                            }
                        }

                        if (masterBracket.NFCDivisionalGame1WinnerId.HasValue)
                        {
                            if (bracket.NFCDivisionalGame1WinnerId == masterBracket.NFCDivisionalGame1WinnerId)
                            {
                                correctScore += conferenceSemiFinalScore;
                            }
                            else
                            {
                                incorrectScore += conferenceSemiFinalScore;
                                eliminatedTeams.Add(bracket.NFCDivisionalGame1WinnerId.Value);
                            }
                        }
                        else
                        {
                            if (eliminatedTeams.FirstOrDefault(x => x == bracket.NFCDivisionalGame1WinnerId) > 0)
                            {
                                incorrectScore += conferenceSemiFinalScore;
                            }
                        }

                        if (masterBracket.NFCDivisionalGame2WinnerId.HasValue)
                        {
                            if (bracket.NFCDivisionalGame2WinnerId == masterBracket.NFCDivisionalGame2WinnerId)
                            {
                                correctScore += conferenceSemiFinalScore;
                            }
                            else
                            {
                                incorrectScore += conferenceSemiFinalScore;
                                eliminatedTeams.Add(bracket.NFCDivisionalGame2WinnerId.Value);
                            }
                        }
                        else
                        {
                            if (eliminatedTeams.FirstOrDefault(x => x == bracket.NFCDivisionalGame2WinnerId) > 0)
                            {
                                incorrectScore += conferenceSemiFinalScore;
                            }
                        }

                        if (masterBracket.AFCChampionshipWinnerId.HasValue)
                        {
                            if (bracket.AFCChampionshipWinnerId == masterBracket.AFCChampionshipWinnerId)
                            {
                                correctScore += conferenceFinalScore;
                            }
                            else
                            {
                                incorrectScore += conferenceFinalScore;
                                eliminatedTeams.Add(bracket.AFCChampionshipWinnerId.Value);
                            }
                        }
                        else
                        {
                            if (eliminatedTeams.FirstOrDefault(x => x == bracket.AFCChampionshipWinnerId) > 0)
                            {
                                incorrectScore += conferenceFinalScore;
                            }
                        }

                        if (masterBracket.NFCChampionshipWinnerId.HasValue)
                        {
                            if (bracket.NFCChampionshipWinnerId == masterBracket.NFCChampionshipWinnerId)
                            {
                                correctScore += conferenceFinalScore;
                            }
                            else
                            {
                                incorrectScore += conferenceFinalScore;
                                eliminatedTeams.Add(bracket.NFCChampionshipWinnerId.Value);
                            }
                        }
                        else
                        {
                            if (eliminatedTeams.FirstOrDefault(x => x == bracket.NFCChampionshipWinnerId) > 0)
                            {
                                incorrectScore += conferenceFinalScore;
                            }
                        }

                        if (masterBracket.SuperBowlWinnerId.HasValue)
                        {
                            if (bracket.SuperBowlWinnerId == masterBracket.SuperBowlWinnerId)
                            {
                                correctScore += superBowlScore;
                            }
                            else
                            {
                                incorrectScore += superBowlScore;
                                eliminatedTeams.Add(bracket.SuperBowlWinnerId.Value);
                            }
                        }
                        else
                        {
                            if (eliminatedTeams.FirstOrDefault(x => x == bracket.SuperBowlWinnerId) > 0)
                            {
                                incorrectScore += superBowlScore;
                            }
                        }

                        var fromDb = dbContext.BracketPicks.FirstOrDefault(x => x.Id == bracket.Id);

                        fromDb.Score = correctScore;
                        fromDb.MaxScore = maxPossibleScore - incorrectScore;

                        dbContext.SaveChanges();
                    }
                }
            }
        }

        // PUT api/<controller>/5
        [Authorize]
        public void Put(int id, [FromBody]BracketPickEntity bracket)
        {
            using (var dbContext = new FjaPoolContext())
            {
                var fromDB = dbContext.BracketPicks.FirstOrDefault(x => x.Id == id);

                if (fromDB != null)
                {
                    fromDB.AFCWildCardGame1WinnerId = bracket.AFCWildCardGame1WinnerId;
                    fromDB.AFCWildCardGame2WinnerId = bracket.AFCWildCardGame2WinnerId;
                    fromDB.NFCWildCardGame1WinnerId = bracket.NFCWildCardGame1WinnerId;
                    fromDB.NFCWildCardGame2WinnerId = bracket.NFCWildCardGame2WinnerId;
                    fromDB.AFCDivisionalGame1WinnerId = bracket.AFCDivisionalGame1WinnerId;
                    fromDB.AFCDivisionalGame2WinnerId = bracket.AFCDivisionalGame2WinnerId;
                    fromDB.NFCDivisionalGame1WinnerId = bracket.NFCDivisionalGame1WinnerId;
                    fromDB.NFCDivisionalGame2WinnerId = bracket.NFCDivisionalGame2WinnerId;
                    fromDB.AFCChampionshipWinnerId = bracket.AFCChampionshipWinnerId;
                    fromDB.NFCChampionshipWinnerId = bracket.NFCChampionshipWinnerId;
                    fromDB.SuperBowlWinnerId = bracket.SuperBowlWinnerId;
                    dbContext.SaveChanges();
                }
            }

            if (bracket.IsMaster)
            {
                UpdateScores(bracket);
            }
        }

        // DELETE api/<controller>/5
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}