using System.Data.Entity.ModelConfiguration;
using Musicstore.Server.Models;

namespace Musicstore.Server.Data.Mapping
{
    public class ArtistMap : EntityTypeConfiguration<Artist>
    {
        public ArtistMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).IsRequired();
            this.Property(x => x.Name).IsUnicode(true);
            this.Property(x => x.Name).HasMaxLength(256);
            this.Property(x => x.Country).HasMaxLength(256);
            this.Property(x => x.DateOfBirth).IsOptional();
        }
    }
}
