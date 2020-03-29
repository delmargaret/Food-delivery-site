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
        }
    }
}
