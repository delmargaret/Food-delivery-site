using EzhaBy.Entities;
using EzhaBy.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EzhaBy.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CateringFacility> Cafes { get; set; }
        public DbSet<CateringFacilityTag> CateringFacilityTags { get; set; }
        public DbSet<CafeSection> CafeSections { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<CourierRequest> CourierRequests { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<PartnerRequest> PartnerRequests { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<User> Users { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
