using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oceanic.Core;

namespace Oceanic.Infrastructure.Mapping
{
    public class RouteMap : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            // Primary Key
            builder.HasKey(t => new { t.Id });

            // Table & Column Mappings
            builder.ToTable("ROUTE");
            builder.Property(t => t.FromCityId).HasColumnName("FROMCITYID");
            builder.Property(t => t.ToCityId).HasColumnName("TOCITYID");
            builder.Property(t => t.TransportType).HasColumnName("TRANSPORTTYPEID");
            builder.Property(t => t.LongHour).HasColumnName("HOURS");
            builder.Property(t => t.Segments).HasColumnName("SEGMENTS");
            builder.Property(t => t.IsActive).HasColumnName("ISACTIVE");
        }
    }
}
