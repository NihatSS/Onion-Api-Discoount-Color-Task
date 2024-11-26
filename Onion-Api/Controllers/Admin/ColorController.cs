using Microsoft.AspNetCore.Mvc;
using Repository.Exceptions;
using Service.Helpers.DTOs.Colors;
using Service.Services.Interfaces;

namespace Onion_Api.Controllers.Admin
{
    public class ColorController : BaseController
    {
        private readonly IColorService _service;
        public ColorController(IColorService service)
        {
            _service = service;
        }


        [ProducesResponseType(typeof(ColorDto), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [ProducesResponseType(typeof(ColorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ColorDto), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ColorCreateDto request)
        {
            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Color successfully created");
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id,[FromBody] ColorEditDto request)
        {
            try
            {
                await _service.EditAsync(id, request);
                return Ok("Successfully Updated");
            }
            catch (NotFoundException)
            {

                return NotFound();
            }
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [ProducesResponseType(typeof(ColorDto), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            return Ok(await _service.SearchByNameAsync(name));
        }
    }
}
