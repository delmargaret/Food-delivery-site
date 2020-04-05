using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzhaBy.Business.Dishes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EzhaBy.Api.Controllers
{
    [Route("api/dishes")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IMediator mediator;

        public DishesController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDish.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetDishes(string cateringFacilityId) 
            => Ok(await mediator.Send(new GetDishes.Query(Guid.Parse(cateringFacilityId))));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDish(string id) => 
            Ok(await mediator.Send(new GetDish.Query(Guid.Parse(id))));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDish(string id, [FromBody] UpdateDish.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SetStatus(string id, [FromBody] SetDishStatus.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}/icon")]
        public async Task<IActionResult> UpdateDishIcon(string id, [FromBody] UpdateDishIcon.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }
    }
}
