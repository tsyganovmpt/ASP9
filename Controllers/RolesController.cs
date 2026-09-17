using ApiAsp0.DTO.DTOrole;
using ApiAsp0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAsp0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AspfisrtContext _context;

        public RolesController(AspfisrtContext context)
        {
            _context = context;
        }

        private static RoleDto ToDto(Role role) => new RoleDto
        {
            IdRole = role.IdRole,
            RoleName = role.RoleName,
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var role = _context.Roles
                .OrderBy(p => p.IdRole)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(role);
        }

        [HttpGet("{idR}")]
        public IActionResult GetById(int idR)
        {
            var role = _context.Roles.FirstOrDefault(u => u.IdRole == idR);
            if (role is null) return NotFound();
            return Ok(ToDto(role));
        }

        [HttpPost]
        public IActionResult Create(CreateRoleDto dto)
        {
            var role = new Role
            {
                RoleName = dto.RoleName,
            };

            _context.Roles.Add(role);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { idrole = role.IdRole });
        }

        [HttpPut("{idR}")]
        public IActionResult Update(int idR, UpdateRoleDto dto)
        {
            var role = _context.Roles.FirstOrDefault(r => r.IdRole == idR);
            if (role is null) return NotFound();

            role.RoleName = dto.RoleName;
            

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{idR}")]
        public IActionResult Delete(int idR)
        {
            var role = _context.Roles.FirstOrDefault(r => r.IdRole == idR);
            if (role is null) return NotFound();

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{idR}")]
        public IActionResult Patch(int idR, PatchRoleDto dto)
        {
            var role = _context.Roles.FirstOrDefault(u => u.IdRole == idR);
            if (role is null) return NotFound();

            if (dto.RoleName is not null) role.RoleName = dto.RoleName;

            _context.SaveChanges();
            return NoContent();
        }
    }
}
