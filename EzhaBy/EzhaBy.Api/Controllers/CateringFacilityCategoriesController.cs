using System;
using System.Threading.Tasks;
using EzhaBy.Business.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string cateringFacilityId, [FromBody] CreateCategory.Command command)
        {
            try
            {
                command.CateringFacilityId = Guid.Parse(cateringFacilityId);
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
        public async Task<IActionResult> GetCategories(string cateringFacilityId)
        {
            try
            {
                return Ok(await mediator.Send(new GetCategories.Query(Guid.Parse(cateringFacilityId))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategoryName(string id, [FromBody] UpdateCategoryName.Command command)
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            try
            {
                return Ok(await mediator.Send(new DeleteCategory.Command(Guid.Parse(id))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
