using EzhaBy.Business.Orders.Dto;
using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Orders
{
    public static class GetCafeOrders
    {
        public class Query : IRequest<IQueryable<CafeOrderDto>>
        {
            public Query(Guid userId)
            {
                UserId = userId;
            }

            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IQueryable<CafeOrderDto>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public Task<IQueryable<CafeOrderDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cateringFacilityId = context.CafeUsers.FirstOrDefault(user => user.UserId == request.UserId)?.CateringFacilityId;
                if (!cateringFacilityId.HasValue)
                {
                    throw new Exception("unknown cafe user");
                }

                var orders = context.Orders.Where(order
                    => order.OrderDishes.Any(dish => dish.Dish.CateringFacilityCategory.CateringFacilityId == cateringFacilityId.Value))
                    .AsNoTracking()
                    .Select(order => new CafeOrderDto
                    {
                        Id = order.Id,
                        Name = order.Name,
                        Surname = order.Surname,
                        Patronymic = order.Patronymic,
                        Phone = order.Phone,
                        Town = order.Town,
                        Street = order.Street,
                        HouseNumber = order.HouseNumber,
                        FlatNumber = order.FlatNumber,
                        PaymentType = order.PaymentType,
                        Comment = order.Comment,
                        OrderStatus = order.OrderStatus,
                        OrderDateTime = order.OrderDateTime,
                        TotalPrice = order.TotalPrice,
                        IsOrderAccepted = order.IsOrderAccepted,
                        OrderDishes = order.OrderDishes.Where(dish 
                            => dish.Dish.CateringFacilityCategory.CateringFacilityId == cateringFacilityId.Value)
                            .Select(dish => new OrderDishDto
                            {
                                Id = dish.DishId,
                                DishName = dish.Dish.DishName,
                                Description = dish.Dish.Description,
                                DishIconUrl = dish.Dish.DishIconUrl,
                                Price = dish.Dish.Price,
                                NumberOfDishes = dish.NumberOfDishes
                            }).ToList()
                    });

                if (orders.Count() == 0)
                {
                    throw new Exception("cafe orders not found");
                }

                return Task.FromResult(orders);
            }
        }
    }
}
