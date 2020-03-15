//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace EzhaBy.Api.Controllers
//{
//    [Route("api/orders")]
//    [ApiController]
//    public class OrdersController : ControllerBase
//    {
//        private readonly IMediator mediator;

//        public OrdersController(IMediator mediator) => this.mediator = mediator;

//        [HttpGet]
//        public async Task<IActionResult> GetOrders(GetOrders.Query query) => Ok(await mediator.Send(query));

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetOrder(GetOrder.Query query) => Ok(await mediator.Send(query));

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrder.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }

//        [HttpPut("{id}/status")]
//        public async Task<IActionResult> SetStatus([FromBody] SetOrderStatus.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }
//    }
//}
