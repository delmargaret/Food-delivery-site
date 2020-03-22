using System;
using System.Threading.Tasks;
using EzhaBy.Business.CateringFacilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EzhaBy.Api.Controllers
{
    [Route("api/catering-facilities")]
    [ApiController]
    public class CateringFacilitiesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CateringFacilitiesController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCateringFacility.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCateringFacilities() => Ok(await mediator.Send(new GetCateringFacilities.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCateringFacility(string id) =>
            Ok(await mediator.Send(new GetCateringFacility.Query(Guid.Parse(id))));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCateringFacility.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SetStatus(string id, [FromBody] SetStatus.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }

        //[HttpPut("{id}/icon")]
        //public async Task<IActionResult> UpdateIcon([FromBody] UpdateCateringFacilityIcon.Command command)
        //{
        //    await mediator.Send(command);
        //    return Ok();
        //}

        //[HttpDelete("{id}/icon")]
        //public async Task<IActionResult> DeleteIcon(DeleteCateringFacilityIcon.Command command)
        //{
        //    await mediator.Send(command);
        //    return Ok();
        //}
    }
}
