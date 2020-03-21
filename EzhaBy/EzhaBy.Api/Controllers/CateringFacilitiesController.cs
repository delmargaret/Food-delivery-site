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

        //[HttpGet]
        //public async Task<IActionResult> GetCateringFacilities(GetCateringFacilities.Query query) =>
        //    Ok(await mediator.Send(query));

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetCateringFacility(GetCateringFacility.Query query) =>
        //    Ok(await mediator.Send(query));

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update([FromBody] UpdateCateringFacility.Command command)
        //{
        //    await mediator.Send(command);
        //    return Ok();
        //}

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

        //[HttpPut("{id}/tag")]
        //public async Task<IActionResult> AddTag([FromBody] AddTag.Command command)
        //{
        //    await mediator.Send(command);
        //    return Ok();
        //}

        //[HttpDelete("{id}/tag")]
        //public async Task<IActionResult> DeleteTag(DeleteTag.Command command)
        //{
        //    await mediator.Send(command);
        //    return Ok();
        //}

        //[HttpGet("{id}/tags")]
        //public async Task<IActionResult> GetTags(GetTags.Query query) =>
        //    Ok(await mediator.Send(query));

        //[HttpPut("{id}/status")]
        //public async Task<IActionResult> SetStatus([FromBody] SetStatus.Command command)
        //{
        //    await mediator.Send(command);
        //    return Ok();
        //}
    }
}
