using ApiAsp0.DTO.DTOcartItem;
using ApiAsp0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAsp0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly AspfisrtContext _context;

        public CartItemsController(AspfisrtContext context)
        {
            _context = context;
        }

        private static CartItemDto ToDto(CartItem cartitem) => new CartItemDto
        {
            IdCartItem = cartitem.IdCartItem,
            ProductId = cartitem.ProductId,
            CartId = cartitem.ProductId,
            Quantity = cartitem.Quantity,

        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var cartitem = _context.CartItems
                .OrderBy(ci => ci.IdCartItem)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(cartitem);
        }

        [HttpGet("{idCI}")]
        public IActionResult GetById(int idCI)
        {
            var cartitem = _context.CartItems.FirstOrDefault(ci => ci.IdCartItem == idCI);
            if (cartitem is null) return NotFound();
            return Ok(ToDto(cartitem));
        }

        [HttpPost]
        public IActionResult Create(CreateCartItemDto dto)
        {
            var cartitem = new CartItem
            {
                ProductId = dto.ProductId,
                CartId= dto.CartId,
                Quantity = dto.Quantity,
            };

            _context.CartItems.Add(cartitem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { idcartitem = cartitem.IdCartItem });
        }

        [HttpPut("{idCI}")]
        public IActionResult Update(int idCI, UpdateCartItemDto dto)
        {
            var cartitem = _context.CartItems.FirstOrDefault(ci => ci.IdCartItem == idCI);
            if (cartitem is null) return NotFound();

            cartitem.ProductId = dto.ProductId;
            cartitem.CartId = dto.CartId;
            cartitem.Quantity = dto.Quantity;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{idCI}")]
        public IActionResult Delete(int idCI)
        {
            var cartitem = _context.CartItems.FirstOrDefault(ci => ci.IdCartItem == idCI);
            if (cartitem is null) return NotFound();

            _context.CartItems.Remove(cartitem);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{idCI}")]
        public IActionResult Patch(int idCI, PatchCartItemDto dto)
        {
            var cartitem = _context.CartItems.FirstOrDefault(ci => ci.IdCartItem == idCI);
            if (cartitem is null) return NotFound();

            if (dto.ProductId is not null) cartitem.ProductId = dto.ProductId.Value;
            if (dto.CartId is not null) cartitem.CartId = dto.CartId.Value;
            if (dto.Quantity is not null) { cartitem.Quantity = dto.Quantity.Value; }

            _context.SaveChanges();
            return NoContent();
        }
    }
}
