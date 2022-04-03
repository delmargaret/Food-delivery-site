using EzhaBy.Business.Orders.Dto;
using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Orders
{
    public static class CreateOrder
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                string name,
                string surname,
                string patronymic,
                string phone,
                Towns town,
                string street,
                string houseNumber,
                string flatNumber,
                PaymentTypes paymentType,
                string comment,
                double totalPrice,
                string userId,
                OrderLineDto[] orderDishes)
            {
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Phone = phone;
                Town = town;
                Street = street;
                HouseNumber = houseNumber;
                FlatNumber = flatNumber;
                PaymentType = paymentType;
                Comment = comment;
                TotalPrice = totalPrice;
                UserId = userId;
                OrderDishes = orderDishes;
            }

            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public string Phone { get; set; }
            public Towns Town { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string FlatNumber { get; set; }
            public PaymentTypes PaymentType { get; set; }
            public string Comment { get; set; }
            public double TotalPrice { get; set; }
            public string UserId { get; set; }
            public OrderLineDto[] OrderDishes { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var orderId = Guid.NewGuid();
                Guid? id = null;
                if (Guid.TryParse(request.UserId, out var userId))
                {
                    id = userId;
                }

                var order = new Order
                {
                    Id = orderId,
                    Name = request.Name,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic,
                    Phone = request.Phone,
                    Town = request.Town,
                    Street = request.Street,
                    HouseNumber = request.HouseNumber,
                    FlatNumber = request.FlatNumber,
                    PaymentType = request.PaymentType,
                    UserId = id,
                    Comment = request.Comment,
                    IsOrderAccepted = false,
                    OrderStatus = OrderStatuses.New,
                    TotalPrice = Convert.ToDecimal(request.TotalPrice),
                    OrderDateTime = DateTimeOffset.Now,
                    CourierId = context.Couriers.FirstOrDefault(courier
                    => courier.Status == CourierStatuses.Active)?.Id,
                    OrderDishes = request.OrderDishes.Select(x => new OrderLine
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        DishId = Guid.TryParse(x.DishId, out var dishId) ? dishId : throw new Exception($"Dish with id {dishId} not found."),
                        NumberOfDishes = x.NumberOfDishes
                    }).ToList(),
                };

                context.Orders.Add(order);
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
