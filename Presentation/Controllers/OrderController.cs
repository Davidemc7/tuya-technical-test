using Application.IServices;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    /// <summary>
    /// No era necesaria la creación de este controlador en la prueba técnica,
    /// pero se implementó para mostrar la forma de hacerlo.
    /// </summary>

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDTO>> GetById(int Id)
        {
            var result = await _service.GetByIdAsync(Id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<OrderDTO>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add(OrderDTO order)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _service.AddAsync(order);
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(OrderDTO order)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!order.OrderId.HasValue)
                return BadRequest();

            var result = await _service.GetByIdAsync(order.OrderId.Value);
            if (result == null)
                return NotFound();

            await _service.UpdateAsync(order);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById(int Id)
        {
            var result = await _service.GetByIdAsync(Id);
            if (result == null)
                return NotFound();

            await _service.DeleteByIdAsync(Id);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteLogicallyById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteLogicallyById(int Id)
        {
            var result = await _service.GetByIdAsync(Id);
            if (result == null)
                return NotFound();

            await _service.DeleteLogicallyByIdAsync(Id);
            return Ok();
        }
    }
}
