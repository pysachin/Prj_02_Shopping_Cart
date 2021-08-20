﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController: ControllerBase
    {
        private readonly IMediator _mediatR;

        public OrderController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet("userName", Name = "GetOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersListQuery(userName);
            var orders = await _mediatR.Send(query);
            return Ok(orders);

        } 

        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result = await _mediatR.Send(command);
            return Ok(result);
        }


        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            var result = await _mediatR.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}",Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand() { Id = id };
            var result = await _mediatR.Send(command);
            return NoContent();
        }


    }
}
