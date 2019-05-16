
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
        //public DbSet<GoodsType> GoodsType { get; set; }
        //public DbSet<ExtraFee> ExtraFee { get; set; }
        //public DbSet<Route> Route { get; set; }
        //public DbSet<Size> Size { get; set; }
        //public DbSet<TransportType> TransportTypes { get; set; }
        //public DbSet<Price> Price { get; set; }
        //public DbSet<Order> Order { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityMap());
            //modelBuilder.ApplyConfiguration(new CustomerMap());
            //modelBuilder.ApplyConfiguration(new CustomerAnswerMap());
            //modelBuilder.ApplyConfiguration(new QuestionMap());
            //modelBuilder.ApplyConfiguration(new QuestionOptionMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

