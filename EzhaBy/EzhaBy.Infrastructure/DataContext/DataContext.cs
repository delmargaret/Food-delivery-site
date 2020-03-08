using EzhaBy.Entities;
using System.Data.Entity;

namespace EzhaBy.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<CafeDescription> CafeDescriptions { get; set; }
        public DbSet<CafeSection> CafeSections { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<CourierRequest> CourierRequests { get; set; }
        public DbSet<DescriptionKeyword> KeyWords { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<PartnerRequest> PartnerRequests { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<User> Users { get; set; }

        static DataContext()
        {
            Database.SetInitializer(new ProjectDbInitializer());
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class ProjectDbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext db)
        {
            
        }
    }
}
