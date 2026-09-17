namespace ApiAsp0.DTO.DTOuser
{
    public class UserDto
    {
        public int IdUsers { get; set; }

        public string LoginUser { get; set; } = null!;

        public string PasswordUser { get; set; } = null!;

        public bool IsActive { get; set; }

        public int RoleId { get; set; }
    }
}
