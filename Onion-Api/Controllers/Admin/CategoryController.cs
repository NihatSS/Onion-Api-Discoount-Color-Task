using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Exceptions;
using Service.Helpers.DTOs.Categories;
using Service.Services.Interfaces;

namespace Onion_Api.Controllers.Admin
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }


        [ProducesResponseType(typeof(CategoryDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CategoryDto),StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                return Ok(await _categoryService.GetByIdAsync(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }


        [ProducesResponseType(typeof(CategoryCreateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CategoryCreateDto), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto request)
        {
            await _categoryService.CreateAsync(request);
            return CreatedAtAction(nameof(Create),"Successfully Created");
        }


        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CategoryCreateDto), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CategoryEditDto request)
        {
            try
            {
                await _categoryService.EditAsync(id, request);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [ProducesResponseType(typeof(CategoryCreateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CategoryCreateDto), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            return Ok(await _categoryService.SearchAsync(searchText));
        }
    }
}
