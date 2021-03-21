using System;
using System.Threading.Tasks;
using EzhaBy.Business.CateringFacilities;
using EzhaBy.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EzhaBy.Api.Controllers
{
    [Route("api/catering-facilities")]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    [ApiController]
    public class CateringFacilitiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CateringFacilitiesController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCateringFacility.Command command)
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
        public async Task<IActionResult> GetCateringFacilities()
        {
            try
            {
                return Ok(await mediator.Send(new GetCateringFacilities.Query()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCateringFacility(string id)
        {
            try
            {
                return Ok(await mediator.Send(new GetCateringFacility.Query(Guid.Parse(id))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
            

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCateringFacility.Command command)
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
        public async Task<IActionResult> SetStatus(string id, [FromBody] SetStatus.Command command)
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
        public async Task<IActionResult> UpdateIcon(string id, [FromBody] UpdateCateringFacilityIcon.Command command)
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
