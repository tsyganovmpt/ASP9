namespace ApiAsp0.DTO.DTOuser
{
    public class CreateUserDto
    {
        public string LoginUser { get; set; } = null!;

        public string PasswordUser { get; set; } = null!;

        public int RoleId { get; set; }
    }
}
