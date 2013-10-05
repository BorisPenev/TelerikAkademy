using Musicstore.Server.Models;
using System.Data.Entity.ModelConfiguration;

namespace Musicstore.Server.Data.Mapping
{
    public class AlbumMap: EntityTypeConfiguration<Album>
    {
        public AlbumMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Title).IsRequired();
            this.Property(x => x.Title).HasMaxLength(256);
            this.Property(x => x.Producer).HasMaxLength(256);
            this.Property(x => x.Producer).IsOptional();
            this.Property(x => x.Year).IsOptional();
        }
    }
}
