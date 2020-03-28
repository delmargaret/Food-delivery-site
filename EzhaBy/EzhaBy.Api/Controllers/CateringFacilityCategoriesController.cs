using System;
using System.Threading.Tasks;
using EzhaBy.Business.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EzhaBy.Api.Controllers
{
    [Route("api/catering-facilities/{cateringFacilityId}/categories")]
    [ApiController]
    public class CateringFacilityCategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CateringFacilityCategoriesController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(string cateringFacilityId, [FromBody] CreateCategory.Command command)
        {
            command.CateringFacilityId = Guid.Parse(cateringFacilityId);
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(string cateringFacilityId) =>
            Ok(await mediator.Send(new GetCategories.Query(Guid.Parse(cateringFacilityId))));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryName(string id, [FromBody] UpdateCategoryName.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id) =>
            Ok(await mediator.Send(new DeleteCategory.Command(Guid.Parse(id))));
    }
}
