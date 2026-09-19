using ApiAsp0.DTO.DTOproduct;
using ApiAsp0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAsp0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AspfisrtContext _context;

        public ProductsController(AspfisrtContext context)
        {
            _context = context;
        }

        private static ProductDto ToDto(Product product) => new ProductDto
        {
            IdProduct = product.IdProduct,
            ProductName = product.ProductName,
            ProductPrice = product.ProductPrice,
            Description = product.Description,
            ImagePath = product.ImagePath,
            CategoryId = product.CategoryId,
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var products = _context.Products
                .OrderBy(p => p.IdProduct)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(products);
        }

        [HttpGet("{idP}")]
        public IActionResult GetById(int idP) {
            var product = _context.Products.FirstOrDefault(p => p.IdProduct == idP);
            if (product is null) return NotFound();
            return Ok(ToDto(product));
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                ProductPrice = dto.ProductPrice,
                Description = dto.Description,
                ImagePath = dto.ImagePath,
                CategoryId = dto.CategoryId,
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { idP = product.IdProduct }, ToDto(product));
        }

        [HttpPut("{idP}")]
        public IActionResult Update(int idP, UpdateProductDto dto)
        {
            var product = _context.Products.FirstOrDefault(p =>p.IdProduct == idP);
            if (product is null) return NotFound();

            product.ProductName = dto.ProductName;
            product.ProductPrice = dto.ProductPrice;
            product.Description = dto.Description;
            product.ImagePath = dto.ImagePath;
            product.CategoryId = dto.CategoryId;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{idP}")]
        public IActionResult Delete(int idP)
        {
            var product = _context.Products.FirstOrDefault(p => p.IdProduct == idP);
            if (product is null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{idP}")]
        public IActionResult Patch(int idP, PatchProductDto dto)
        {
            var product = _context.Products.FirstOrDefault(p => p.IdProduct == idP);
            if (product is null) return NotFound();

            if (dto.ProductName is not null) product.ProductName = dto.ProductName;
            if (dto.ProductPrice is not null) product.ProductPrice = dto.ProductPrice.Value;
            if (dto.Description is not null) product.Description = dto.Description;
            if (dto.ImagePath is not null) product.ImagePath = dto.ImagePath;
            if (dto.CategoryId is not null) product.CategoryId = dto.CategoryId.Value;

            _context.SaveChanges();
            return Ok();
        }

    }
}
