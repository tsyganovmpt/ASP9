using ApiAsp0.DTO.DTOcart;
using ApiAsp0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAsp0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly AspfisrtContext _context;

        public CartsController(AspfisrtContext context)
        {
            _context = context;
        }

        private static CartDto ToDto(Cart cart) => new CartDto
        {
            IdCart = cart.IdCart,
            UsersId = cart.UsersId,
            CreationDate = cart.CreationDate,

        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var cart = _context.Carts
                .OrderBy(p => p.IdCart)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(cart);
        }

        [HttpGet("{idC}")]
        public IActionResult GetById(int idC)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.IdCart == idC);
            if (cart is null) return NotFound();
            return Ok(ToDto(cart));
        }

        [HttpPost]
        public IActionResult Create(CreateCartDto dto)
        {
            var cart = new Cart
            {
                UsersId = dto.UsersId,
                CreationDate = DateTime.UtcNow,
            };

            _context.Carts.Add(cart);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { idcart = cart.IdCart });
        }

        [HttpPut("{idC}")]
        public IActionResult Update(int idC, UpdateCartDto dto)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.IdCart == idC);
            if (cart is null) return NotFound();

            cart.UsersId = dto.UsersId;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{idC}")]
        public IActionResult Delete(int idC)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.IdCart == idC);
            if (cart is null) return NotFound();

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{idC}")]
        public IActionResult Patch(int idC, PatchCartDto dto)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.IdCart == idC);
            if (cart is null) return NotFound();

            if (dto.UsersId is not null) cart.UsersId = dto.UsersId.Value;

            _context.SaveChanges();
            return NoContent();
        }
    }
}

