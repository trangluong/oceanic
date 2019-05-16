using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oceanic.Core;

namespace Oceanic.Infrastructure.Mapping
{
    public class GoodsTypeMap : IEntityTypeConfiguration<GoodsType>
    {
        public void Configure(EntityTypeBuilder<GoodsType> builder)
        {
            // Primary Key
            builder.HasKey(t => new { t.Id });

            // Table & Column Mappings
            builder.ToTable("GOODSTYPE");
            builder.Property(t => t.Code).HasColumnName("CODE");
            builder.Property(t => t.Name).HasColumnName("NAME");
        }
    }
}
