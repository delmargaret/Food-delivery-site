﻿using EzhaBy.Business.Orders;
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
    }
}