using System.Data.Entity;
using Musicstore.Server.Data.Mapping;
using Musicstore.Server.Models;
using Musicstore.Server.Models.Interfaces;

namespace Musicstore.Server.Data
{
    public class MusicstoreContext : DbContext, IIdentifier
    {
        public MusicstoreContext()
            : base("Name=MusicstoreConnectionString")
        {
            
        }

        //public MusicstoreContext(string nameOrConnectionString)
        //    : base(nameOrConnectionString)
        //{
        //}

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlbumMap());
            modelBuilder.Configurations.Add(new ArtistMap());
            modelBuilder.Configurations.Add(new SongMap());

            base.OnModelCreating(modelBuilder);
        }

        //public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        //{
        //    return base.Set<TEntity>();
        //}

        public int Id { get; set; }
    }
}
