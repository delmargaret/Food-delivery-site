using EzhaBy.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EzhaBy.Infrastructure.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var courierUserId = Guid.NewGuid();
            var cafeUserId = Guid.NewGuid();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "user@user.com",
                Password = "user",
                UserRole = UserRoles.User,
                Name = "Иван",
                Surname = "Иванов",
                Phone = "+375333333333",
                Town = Towns.Minsk,
                Street = "Свердлова",
                HouseNumber = "13",
                FlatNumber = "24"
            };

            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "admin@admin.com",
                        Password = "admin",
                        UserRole = UserRoles.Admin
                    },
                    new User
                    {
                        Id = cafeUserId,
                        Email = "cafe@cafe.com",
                        Password = "cafe",
                        UserRole = UserRoles.CafeAdmin,
                        Name = "CafeName",
                        Surname = "CafeSurname",
                        Patronymic = "CafePatronymic",
                        Phone = "+375291111111"
                    },
                    new User
                    {
                        Id = courierUserId,
                        Email = "courier@courier.com",
                        Password = "courier",
                        UserRole = UserRoles.Courier,
                        Name = "CourierName",
                        Surname = "CourierSurname",
                        Patronymic = "CourierPatronymic",
                        Phone = "+375292222222"
                    },
                    user
                }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag[]
                {
                    new Tag{ Id = Guid.NewGuid(), TagName = "Выпечка" },
                    new Tag{ Id = Guid.NewGuid(), TagName = "Кофе" },
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
                        Town = Towns.Minsk,
                        Street = "Белорусская",
                        HouseNumber = "21",
                        CateringFacilityIconUrl = string.Empty
                    }               
                }
            );

            var categoryId = Guid.NewGuid();

            modelBuilder.Entity<Category>().HasData(
               new Category[]
               {
                    new Category
                    {
                        Id = categoryId,
                        CategoryName = "Бургеры",          
                    }
               }
           );

            var cateringFacilityCategoryId = Guid.NewGuid();

            modelBuilder.Entity<CateringFacilityCategory>().HasData(
               new CateringFacilityCategory[]
               {
                    new CateringFacilityCategory
                    {
                        Id = cateringFacilityCategoryId,
                        CategoryId = categoryId,
                        CateringFacilityId = cateringFacilityId
                    }
               }
           );

            var firstDishId = Guid.NewGuid();
            var secondDishId = Guid.NewGuid();
            modelBuilder.Entity<Dish>().HasData(
               new Dish[]
               {
                    new Dish
                    {
                        Id = firstDishId,
                        DishName = "Бургер 1",
                        DishIconUrl = string.Empty,
                        Description = "Бургер 1 описание",
                        Price = 4,
                        DishStatus = DishStatuses.InStock,
                        CateringFacilityCategoryId = cateringFacilityCategoryId
                    },
                    new Dish
                    {
                        Id = secondDishId,
                        DishName = "Бургер 2",
                        DishIconUrl = string.Empty,
                        Description = "Бургер 2 описание",
                        Price = 2,
                        DishStatus = DishStatuses.InStock,
                        CateringFacilityCategoryId = cateringFacilityCategoryId
                    }
               }
           );

            var courierId = Guid.NewGuid();
            modelBuilder.Entity<Courier>().HasData(
               new Courier[]
               {
                    new Courier
                    {
                        Id = courierId,
                        UserId = courierUserId,
                        Status = CourierStatuses.Away

                    }
               }
           );

            modelBuilder.Entity<CafeUser>().HasData(
               new CafeUser[]
               {
                    new CafeUser
                    {
                        Id = Guid.NewGuid(),
                        UserId = cafeUserId,
                        CateringFacilityId = cateringFacilityId

                    }
               }
           );

            var orderId = Guid.NewGuid();
            modelBuilder.Entity<Order>().HasData(
               new Order[]
               {
                    new Order
                    {
                        Id = orderId,
                        CourierId = courierId,
                        IsOrderAccepted = false,
                        Name = user.Name,
                        Surname = user.Surname,
                        UserId = user.Id,
                        Town = user.Town,
                        Street = user.Street,
                        HouseNumber = user.HouseNumber,
                        FlatNumber = user.FlatNumber,
                        Phone = user.Phone,
                        PaymentType = PaymentTypes.Cash,
                        OrderDateTime = DateTimeOffset.Now,
                        OrderStatus = OrderStatuses.New
                    }
               }
           );

            modelBuilder.Entity<OrderLine>().HasData(
               new OrderLine[]
               {
                    new OrderLine
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        DishId = firstDishId,
                        NumberOfDishes = 2
                    },
                    new OrderLine
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        DishId = secondDishId,
                        NumberOfDishes = 1
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
                        Name = "Марго",
                        Surname = "Деликатная",
                        Patronymic = "",
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
                        Name = "Марго",
                        Surname = "Деликатная",
                        Patronymic = "Михайловна",
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
                        Name = "Марго",
                        Surname = "Деликатная",
                        Patronymic = "Михайловна",
                        Email = "ri.tysik@mail.ru",
                        Phone = "80534271657",
                        RequestStatus = RequestStatuses.New
                    },
                    new PartnerRequest{
                        Id = Guid.NewGuid(),
                        CateringFacilityName = "New BK",
                        CateringFacilityType = CateringFacilityTypes.Restaurant,
                        Name = "Марго",
                        Surname = "Деликатная",
                        Patronymic = "",
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
                        Name = "Марго",
                        Surname = "Деликатная",
                        Patronymic = "",
                        VehicleType = VehicleTypes.Motorcycle,
                        FuelConsumption = 7,
                        Email = "ri.tysik@mail.ru",
                        Phone = "80534271657",
                        RequestStatus = RequestStatuses.New
                    },
                    new CourierRequest{
                        Id = Guid.NewGuid(),
                        Name = "Марго",
                        Surname = "Деликатная",
                        Patronymic = "",
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
