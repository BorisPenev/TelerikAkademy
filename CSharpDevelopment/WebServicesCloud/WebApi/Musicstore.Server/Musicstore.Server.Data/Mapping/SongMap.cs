using System.Data.Entity.ModelConfiguration;
using Musicstore.Server.Models;

namespace Musicstore.Server.Data.Mapping
{
    public class SongMap : EntityTypeConfiguration<Song>
    {
        public SongMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Title).HasMaxLength(256);
            this.Property(x => x.Genre).IsOptional();
            this.Property(x => x.Genre).HasMaxLength(256);
            this.Property(x => x.Year).IsOptional();
        }
    }
}
