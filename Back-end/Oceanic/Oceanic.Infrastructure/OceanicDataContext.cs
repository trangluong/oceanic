
using Microsoft.EntityFrameworkCore;
using Oceanic.Core;
using Oceanic.Infrastructure.Mapping;
using Oceanic.Infrastructure.Repository;

namespace Oceanic.Infrastructure
{
    public class OceanicDataContext : DataContext
    {
        public OceanicDataContext(DbContextOptions<OceanicDataContext> options) : base(options)
        {
        }

        public DbSet<City> City { get; set; }
        public DbSet<GoodsType> GoodsType { get; set; }
        public DbSet<ExtraFee> ExtraFee { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<TransportType> TransportTypes { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Order> Order { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityMap());
            modelBuilder.ApplyConfiguration(new GoodsTypeMap());
            modelBuilder.ApplyConfiguration(new ExtraFeeMap());
            modelBuilder.ApplyConfiguration(new RouteMap());
            modelBuilder.ApplyConfiguration(new SizeMap());
            modelBuilder.ApplyConfiguration(new TransportTypeMap());
            modelBuilder.ApplyConfiguration(new PriceMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}

