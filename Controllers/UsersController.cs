using ApiAsp0.DTO.DTOuser;
using ApiAsp0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAsp0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AspfisrtContext _context;

        public UsersController(AspfisrtContext context)
        {
            _context = context;
        }

        private static UserDto ToDto(User user) => new UserDto
        {
            IdUsers = user.IdUsers,
            LoginUser = user.LoginUser,
            PasswordUser = user.PasswordUser,
            IsActive = user.IsActive,
            RoleId = user.RoleId,
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var users = _context.Users
                .OrderBy(p => p.IdUsers)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(users);
        }

        [HttpGet("{idU}")]
        public IActionResult GetById(int idU)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUsers == idU);
            if (user is null) return NotFound();
            return Ok(ToDto(user));
        }

        [HttpPost]
        public IActionResult Create(CreateUserDto dto)
        {
            var user = new User
            {
                LoginUser = dto.LoginUser,
                PasswordUser = dto.PasswordUser,
                RoleId = dto.RoleId,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { idU = user.IdUsers }, ToDto(user));
        }

        [HttpPut("{idU}")]
        public IActionResult Update(int idU, UpdateUserDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUsers == idU);
            if (user is null) return NotFound();

            user.LoginUser = dto.LoginUser;
            user.PasswordUser = dto.PasswordUser;
            user.RoleId = dto.RoleId;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{idU}")]
        public IActionResult Delete(int idU)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUsers == idU);
            if (user is null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{idU}")]
        public IActionResult Patch(int idU, PatchUserDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.IdUsers == idU);
            if (user is null) return NotFound();

            if (dto.LoginUser is not null) user.LoginUser = dto.LoginUser;
            if (dto.PasswordUser is not null) user.PasswordUser = dto.PasswordUser;
            if (dto.RoleId is not null) user.RoleId = dto.RoleId.Value;

            _context.SaveChanges();
            return Ok();
        }
    }
}
