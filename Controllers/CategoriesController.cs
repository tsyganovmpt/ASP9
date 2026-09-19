using ApiAsp0.DTO.DTOcategory;
using ApiAsp0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAsp0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AspfisrtContext _context;

        public CategoriesController(AspfisrtContext context)
        {
            _context = context;
        }

        private static CategoryDto ToDto(Category category) => new CategoryDto
        {
            IdCategory = category.IdCategory,
            CategoryName = category.CategoryName,

        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var categories = _context.Categories
                .OrderBy(c => c.IdCategory)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(categories);
        }

        [HttpGet("{idC}")]
        public IActionResult GetById(int idC)
        {
            var category = _context.Categories.FirstOrDefault(c => c.IdCategory == idC);
            if (category is null) return NotFound();
            return Ok(ToDto(category));
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { idC = category.IdCategory }, ToDto(category));
        }

        [HttpPut("{idC}")]
        public IActionResult Update(int idC, UpdateCategoryDto dto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.IdCategory == idC);
            if (category is null) return NotFound();

            category.CategoryName = dto.CategoryName;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{idC}")]
        public IActionResult Delete(int idC)
        {
            var category = _context.Categories.FirstOrDefault(c => c.IdCategory == idC);
            if (category is null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{idC}")]
        public IActionResult Patch(int idC, PatchCategoryDto dto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.IdCategory == idC);
            if (category is null) return NotFound();

            if (dto.CategoryName is not null) category.CategoryName = dto.CategoryName;

            _context.SaveChanges();
            return Ok();
        }
    }
}
