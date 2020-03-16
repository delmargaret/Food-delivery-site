using EzhaBy.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EzhaBy.Infrastructure
{
    public interface IDataContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<CateringFacility> Cafes { get; set; }
        DbSet<CateringFacilityTag> CateringFacilityTags { get; set; }
        DbSet<CafeSection> CafeSections { get; set; }
        DbSet<Complaint> Complaints { get; set; }
        DbSet<Courier> Couriers { get; set; }
        DbSet<CourierRequest> CourierRequests { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Dish> Dishes { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderLine> OrderLines { get; set; }
        DbSet<PartnerRequest> PartnerRequests { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
