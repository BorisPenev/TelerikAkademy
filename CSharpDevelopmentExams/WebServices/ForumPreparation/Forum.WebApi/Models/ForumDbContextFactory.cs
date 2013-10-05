using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Forum.Data;

namespace Forum.WebApi.Models
{
    public class ForumDbContextFactory:IDbContextFactory<DbContext>
    {
        public DbContext Create()
        {
            return new ForumContext();
        }
    }
}