using EzhaBy.Business.Orders.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Orders
{
    public static class GetCourierOrders
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
                var courier = context.Couriers.FirstOrDefault(user => user.UserId == request.UserId);
                if (courier == null)
                {
                    throw new Exception("unknown courier");
                }

                var orders = courier.Orders.AsQueryable()
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
                        OrderDishes = order.OrderDishes
                            .Select(dish => new OrderDishDto
                            {
                                Id = dish.DishId,
                                DishName = dish.Dish.DishName,
                                Description = dish.Dish.Description,
                                DishIconUrl = dish.Dish.DishIconUrl,
                                Price = dish.Dish.Price,
                                NumberOfDishes = dish.NumberOfDishes,
                                CateringFacilityName = dish.Dish.CateringFacilityCategory.CateringFacility.CateringFacilityName
                            }).ToList()
                    });

                return Task.FromResult(orders);
            }
        }
    }
}
