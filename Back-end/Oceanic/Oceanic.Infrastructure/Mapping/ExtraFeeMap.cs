using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oceanic.Core;

namespace Oceanic.Infrastructure.Mapping
{
    public class ExtraFeeMap : IEntityTypeConfiguration<ExtraFee>
    {
    
        public void Configure(EntityTypeBuilder<ExtraFee> builder)
        {
            //Primary Key
                builder.HasKey(t => new { t.Id });

            // Table & Column Mappings
            builder.ToTable("EXTRAFEE");
            builder.Property(t => t.Id).HasColumnName("ID");
            builder.Property(t => t.GoodsTypeId).HasColumnName("GOODSTYPEID");
            builder.Property(t => t.ExtraPercent).HasColumnName("EXTRAFEE");
            builder.Property(t => t.CreatedDate).HasColumnName("CREATEDATE").ValueGeneratedOnAdd();
            builder.Property(t => t.CreatedBy).HasColumnName("CREATEBY");
        }
    }
}
