using ApiAsp0.DTO.DTOorderItem;
using ApiAsp0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAsp0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly AspfisrtContext _context;

        public OrderItemsController(AspfisrtContext context)
        {
            _context = context;
        }

        private static OrderItemDto ToDto(OrderItem orderitem) => new OrderItemDto
        {
            IdOrderItem = orderitem.IdOrderItem,
            CartItemId = orderitem.CartItemId,
            OrderPrice = orderitem.OrderPrice,
            Quantity = orderitem.Quantity,
            OrderId = orderitem.OrderId,
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var orderitem = _context.OrderItems
                .OrderBy(p => p.IdOrderItem)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(orderitem);
        }

        [HttpGet("{idOI}")]
        public IActionResult GetById(int idOI)
        {
            var orderitem = _context.OrderItems.FirstOrDefault(oi => oi.IdOrderItem == idOI);
            if (orderitem is null) return NotFound();
            return Ok(ToDto(orderitem));
        }

        [HttpPost]
        public IActionResult Create(CreateOrderItemDto dto)
        {
            var orderitem = new OrderItem
            {
                CartItemId = dto.CartItemId,
                OrderPrice = dto.OrderPrice,
                Quantity = dto.Quantity,
                OrderId = dto.OrderId,
            };

            _context.OrderItems.Add(orderitem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { idOI = orderitem.IdOrderItem }, ToDto(orderitem));
        }

        [HttpPut("{idOI}")]
        public IActionResult Update(int idOI, UpdateOrderItemDto dto)
        {
            var orderitem = _context.OrderItems.FirstOrDefault(oi => oi.IdOrderItem == idOI);
            if (orderitem is null) return NotFound();

            orderitem.CartItemId = dto.CartItemId;
            orderitem.OrderPrice = dto.OrderPrice;
            orderitem.Quantity = dto.Quantity;
            orderitem.OrderId = dto.OrderId;


            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{idOI}")]
        public IActionResult Delete(int idOI)
        {
            var orderitem = _context.OrderItems.FirstOrDefault(oi => oi.IdOrderItem == idOI);
            if (orderitem is null) return NotFound();

            _context.OrderItems.Remove(orderitem);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{idOI}")]
        public IActionResult Patch(int idOI, PatchOrderItemDto dto)
        {
            var orderitem = _context.OrderItems.FirstOrDefault(oi => oi.IdOrderItem == idOI);
            if (orderitem is null) return NotFound();

            if (dto.CartItemId is not null) orderitem.CartItemId = dto.CartItemId.Value;
            if (dto.OrderPrice is not null) orderitem.OrderPrice = dto.OrderPrice.Value;
            if (dto.Quantity is not null) orderitem.Quantity = dto.Quantity.Value;
            if (dto.OrderId is not null) orderitem.OrderId = dto.OrderId.Value;

            _context.SaveChanges();
            return Ok();
        }
    }
}
