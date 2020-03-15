//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace EzhaBy.Api.Controllers
//{
//    [Route("api/food")]
//    [ApiController]
//    public class FoodController : ControllerBase
//    {
//        private readonly IMediator mediator;

//        public FoodController(IMediator mediator) => this.mediator = mediator;

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] CreateDish.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetDishes(GetDishes.Query query) => Ok(await mediator.Send(query));

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetDish(GetDish.Query query) => Ok(await mediator.Send(query));

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateDish([FromBody] UpdateDish.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }

//        [HttpPut("{id}/status")]
//        public async Task<IActionResult> SetStatus([FromBody] SetDishStatus.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }

//        [HttpPut("{id}/icon")]
//        public async Task<IActionResult> UpdateDishIcon([FromBody] UpdateDishIcon.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }

//        [HttpDelete("{id}/icon")]
//        public async Task<IActionResult> DeleteDishIcon(DeleteDishIcon.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }
//    }
//}
