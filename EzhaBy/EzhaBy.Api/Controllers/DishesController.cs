using System;
using System.Threading.Tasks;
using EzhaBy.Business.Dishes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateDish.Command command)
        {
            try
            {
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDishes(string cateringFacilityId)
        {
            try
            {
                return Ok(await mediator.Send(new GetDishes.Query(Guid.Parse(cateringFacilityId))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDish(string id)
        {
            try
            {
                return Ok(await mediator.Send(new GetDish.Query(Guid.Parse(id))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDish(string id, [FromBody] UpdateDish.Command command)
        {
            try
            {
                command.Id = Guid.Parse(id);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetStatus(string id, [FromBody] SetDishStatus.Command command)
        {
            try
            {
                command.Id = Guid.Parse(id);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}/icon")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDishIcon(string id, [FromBody] UpdateDishIcon.Command command)
        {
            try
            {
                command.Id = Guid.Parse(id);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
