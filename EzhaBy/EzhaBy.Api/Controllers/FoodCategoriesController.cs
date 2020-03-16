//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace EzhaBy.Api.Controllers
//{
//    [Route("api/food-categories")]
//    [ApiController]
//    public class FoodCategoriesController : ControllerBase
//    {
//        private readonly IMediator mediator;

//        public FoodCategoriesController(IMediator mediator) => this.mediator = mediator;

//        [HttpPost("isAssigned")]
//        public async Task<IActionResult> IsAssigned(IsCategoryAssigned.Query query) =>
//            Ok(await mediator.Send(query));


//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] CreateCategory.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetCategories() => Ok(await mediator.Send(new GetCategories.Query()));

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetCategory(GetCategory.Query query) => Ok(await mediator.Send(query));

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateCategoryName([FromBody] UpdateCategoryName.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }
        
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCategory(DeleteCategory.Command command)
//        {
//            await mediator.Send(command);
//            return Ok();
//        }

//        [HttpGet("food-number")]
//        public async Task<IActionResult> GetFoodNumber(GetFoodNumber.Query query) =>
//            Ok(await mediator.Send(query));
//    }
//}
