using EzhaBy.Business.Orders;
using EzhaBy.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EzhaBy.Api.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public OrdersController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrder.Command command)
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

        [HttpGet("")]
        [Authorize(Roles = AuthorizationRoles.User)]
        public async Task<IActionResult> GetUserOrders()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                return Ok(await mediator.Send(new GetUserOrders.Query(Guid.Parse(userId))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("cafe-orders")]
        [Authorize(Roles = AuthorizationRoles.CafeAdmin)]
        public async Task<IActionResult> GetCafeOrders()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                return Ok(await mediator.Send(new GetCafeOrders.Query(Guid.Parse(userId))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("courier-orders")]
        [Authorize(Roles = AuthorizationRoles.Courier)]
        public async Task<IActionResult> GetCourierOrders()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                return Ok(await mediator.Send(new GetCourierOrders.Query(Guid.Parse(userId))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = AuthorizationRoles.CafeAdminAndCourier)]
        public async Task<IActionResult> SetOrderStatus(string id, [FromBody] SetOrderStatus.Command command)
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

        [HttpPut("{id}/courier")]
        [Authorize(Roles = AuthorizationRoles.CafeAdmin)]
        public async Task<IActionResult> SetOrderCourier(string id, [FromBody] SetOrderCourier.Command command)
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

        [HttpPut("{id}/accept")]
        [Authorize(Roles = AuthorizationRoles.Courier)]
        public async Task<IActionResult> AcceptOrder(string id, [FromBody] AcceptOrder.Command command)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                command.OrderId = Guid.Parse(id);
                command.UserId = Guid.Parse(userId);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}/reject")]
        [Authorize(Roles = AuthorizationRoles.Courier)]
        public async Task<IActionResult> RejectOrder(string id, [FromBody] RejectOrder.Command command)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                command.OrderId = Guid.Parse(id);
                command.UserId = Guid.Parse(userId);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("courier-status")]
        [Authorize(Roles = AuthorizationRoles.Courier)]
        public async Task<IActionResult> GetCourierStatus()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                return Ok(await mediator.Send(new GetCourierStatus.Query(Guid.Parse(userId))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("courier-status")]
        [Authorize(Roles = AuthorizationRoles.Courier)]
        public async Task<IActionResult> SetCourierStatus([FromBody] SetCourierStatus.Command command)
        {
            try
            {
                var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
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