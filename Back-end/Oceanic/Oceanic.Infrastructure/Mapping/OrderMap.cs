using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oceanic.Core;

namespace Oceanic.Infrastructure.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Primary Key
            builder.HasKey(t => new { t.Id });

            // Table & Column Mappings
            builder.ToTable("ORDERDELIVERY");
            builder.Property(t => t.FromCityId).HasColumnName("FROMCITYID");
            builder.Property(t => t.ToCityId).HasColumnName("TOCITYID");
            builder.Property(t => t.GoodsTypeId).HasColumnName("GOODSTYPEID");
            builder.Property(t => t.StartDate).HasColumnName("STARTDATE");
            builder.Property(t => t.ArrivalDate).HasColumnName("ARIVALDATE");
            builder.Property(t => t.TotalFee).HasColumnName("TOTALFEE");
            builder.Property(t => t.RouteIDs).HasColumnName("ROUTEIDS");
        }
    }
}
