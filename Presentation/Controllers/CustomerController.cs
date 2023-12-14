using Application.IServices;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDTO>> GetById(int Id)
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
        public async Task<ActionResult<List<CustomerDTO>>> GetAll()
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
        public async Task<ActionResult> Add(CustomerDTO customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _service.AddAsync(customer);
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(CustomerDTO customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!customer.CustomerId.HasValue)
                return BadRequest();

            var result = await _service.GetByIdAsync(customer.CustomerId.Value);
            if (result == null)
                return NotFound();

            await _service.UpdateAsync(customer);
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
