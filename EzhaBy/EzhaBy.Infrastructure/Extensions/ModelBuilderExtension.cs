using EzhaBy.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EzhaBy.Infrastructure.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasData(
                new Tag[]
                {
                    new Tag{ Id = Guid.NewGuid(), TagName = "First" },
                    new Tag{ Id = Guid.NewGuid(), TagName = "Second" },
                }
            );

            var cateringFacilityId = Guid.NewGuid();

            modelBuilder.Entity<CateringFacility>().HasData(
                new CateringFacility[]
                {
                    new CateringFacility
                    {
                        Id = cateringFacilityId,
                        CateringFacilityName = "KFC",
                        CateringFacilityStatus = CateringFacilityStatuses.Active,
                        CateringFacilityType = CateringFacilityTypes.Cafe,
                        DeliveryPrice = 2,
                        DeliveryTime = "30-45 минут",
                        WorkingHours = "10.00 - 20.00",
                        CateringFacilityIconUrl = string.Empty
                    }               
                }
            );

            modelBuilder.Entity<Feedback>().HasData(
                new Feedback[]
                {
                    new Feedback
                    {
                        Id = Guid.NewGuid(),
                        CateringFacilityId = cateringFacilityId,
                        FeedbackCategory = FeedbackCategories.Complaint,
                        Name = "Margo",
                        Surname = "Del",
                        Patronymic = "Mih",
                        Email = "ri.tysik@mail.ru",
                        FeedbackStatus = FeedbackStatuses.New,
                        Text = "Жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба " +
                        "жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба " +
                        "жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба жалоба "
                    },
                    new Feedback
                    {
                        Id = Guid.NewGuid(),
                        CateringFacilityId = cateringFacilityId,
                        FeedbackCategory = FeedbackCategories.Review,
                        Name = "Margo",
                        Surname = "Del",
                        Patronymic = "Mih",
                        Email = "ri.tysik@mail.ru",
                        FeedbackStatus = FeedbackStatuses.New,
                        Text = "Отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв " +
                        "отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв " +
                        "отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв отзыв "
                    }
                }
            );

            modelBuilder.Entity<PartnerRequest>().HasData(
                new PartnerRequest[]
                {
                    new PartnerRequest{
                        Id = Guid.NewGuid(),
                        CateringFacilityName = "New KFC",
                        CateringFacilityType = CateringFacilityTypes.Cafe,
                        Name = "Margo",
                        Surname = "Del",
                        Patronymic = "Mih",
                        Email = "ri.tysik@mail.ru",
                        Phone = "80534271657",
                        RequestStatus = RequestStatuses.New
                    },
                    new PartnerRequest{
                        Id = Guid.NewGuid(),
                        CateringFacilityName = "New BK",
                        CateringFacilityType = CateringFacilityTypes.Restaurant,
                        Name = "Margo",
                        Surname = "Delikat",
                        Patronymic = "Mihail",
                        Email = "ri.tysik@mail.ru",
                        Phone = "80094856257",
                        RequestStatus = RequestStatuses.New
                    },
                }
            );

            modelBuilder.Entity<CourierRequest>().HasData(
                new CourierRequest[]
                {
                    new CourierRequest{
                        Id = Guid.NewGuid(),
                        Name = "Margo",
                        Surname = "Del",
                        Patronymic = "Mih",
                        VehicleType = VehicleTypes.Motorcycle,
                        FuelConsumption = 7,
                        Email = "ri.tysik@mail.ru",
                        Phone = "80534271657",
                        RequestStatus = RequestStatuses.New
                    },
                    new CourierRequest{
                        Id = Guid.NewGuid(),
                        Name = "Margo",
                        Surname = "Delikat",
                        Patronymic = "Mihail",
                        VehicleType = VehicleTypes.Bike,
                        FuelConsumption = 0,
                        Email = "ri.tysik@mail.ru",
                        Phone = "80534271657",
                        RequestStatus = RequestStatuses.New
                    },
                }
            );
        }
    }
}
