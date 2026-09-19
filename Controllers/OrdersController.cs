using ApiAsp0.DTO.DTOorder;
using ApiAsp0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAsp0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AspfisrtContext _context;

        public OrdersController(AspfisrtContext context)
        {
            _context = context;
        }

        private static OrderDto ToDto(Order order) => new OrderDto
        {
            IdOrder = order.IdOrder,
            UsersId = order.UsersId,
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate,
            
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var orders = _context.Orders
                .OrderBy(p => p.IdOrder)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(orders);
        }

        [HttpGet("{idO}")]
        public IActionResult GetById(int idO)
        {
            var order = _context.Orders.FirstOrDefault(o => o.IdOrder == idO);
            if (order is null) return NotFound();
            return Ok(ToDto(order));
        }

        [HttpPost]
        public IActionResult Create(CreateOrderDto dto)
        {
            var order = new Order
            {
                TotalPrice = dto.TotalPrice,
                OrderDate = DateTime.UtcNow,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { idO = order.IdOrder }, ToDto(order));
        }

        [HttpPut("{idO}")]
        public IActionResult Update(int idO, UpdateOrderDto dto)
        {
            var order = _context.Orders.FirstOrDefault(o => o.IdOrder == idO);
            if (order is null) return NotFound();

            order.TotalPrice = dto.TotalPrice;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{idO}")]
        public IActionResult Delete(int idO)
        {
            var order = _context.Orders.FirstOrDefault(o => o.IdOrder == idO);
            if (order is null) return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{idO}")]
        public IActionResult Patch(int idO, PatchOrderDto dto)
        {
            var order = _context.Orders.FirstOrDefault(o => o.IdOrder == idO);
            if (order is null) return NotFound();

            if (dto.TotalPrice is not null) order.TotalPrice = dto.TotalPrice.Value;

            _context.SaveChanges();
            return Ok();
        }
    }
}
