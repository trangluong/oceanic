using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oceanic.Core;

namespace Oceanic.Infrastructure.Mapping
{
    public class SizeMap : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            // Primary Key
            builder.HasKey(t => new { t.Id });

            // Table & Column Mappings
            builder.ToTable("SIZE");
            builder.Property(t => t.Type).HasColumnName("TYPE");
            builder.Property(t => t.MaxHeight).HasColumnName("MAXHEIGHT");
            builder.Property(t => t.MaxDepth).HasColumnName("MAXDEPTH");
            builder.Property(t => t.MaxBreath).HasColumnName("MAXBREATH");
            builder.Property(t => t.CreatedDate).HasColumnName("CREATEDATE").ValueGeneratedOnAdd();
            builder.Property(t => t.CreatedBy).HasColumnName("CREATEBY");
        }
    }
}
