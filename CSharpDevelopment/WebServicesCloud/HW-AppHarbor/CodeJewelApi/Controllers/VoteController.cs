using CodeJewelData;
using CodeJewelModels;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace CodeJewelApi.Controllers
{
    public class VoteController : BaseController
    {
        private const double MINVOTEVALUE = -1;
        [HttpPost]
        [ActionName("addvote")]
        public HttpResponseMessage addVote(int id, [FromBody] Vote vote)
        {
            var response = PerformOperation(() =>
            {
                var context = new CodeContext();
                using (context)
                {
                    var jewel = context.CodeJewels.FirstOrDefault(j => j.Id == id);
                    if (jewel == null)
                    {
                        throw new InvalidOperationException("The jewel does not exists!");
                    }
                    jewel.Votes.Add(vote);
                    context.Votes.Add(vote);
                    context.SaveChanges();
                    CheckForVeryLowAvgVote(id, vote,context);

                    return vote;
                }
            });
            return response;
        }

        private void CheckForVeryLowAvgVote(int id, Vote vote, CodeContext context)
        {
           // var context = new CodeContext();
            var jewel = context.CodeJewels.Include("Votes").FirstOrDefault(j => j.Id == id);
            if (jewel != null)
            {
                double avgVote = 0;
                if (jewel.Votes.Count > 0)
                {
                    avgVote = jewel.Votes.Average(v => v.VoteValue);
                }

                if (avgVote < MINVOTEVALUE)
                {
                    context.Votes.Remove(vote);
                    context.CodeJewels.Remove(jewel);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new InvalidOperationException("Trying to delete, but didn't find codeJewel");
            }

        }
    }
}
