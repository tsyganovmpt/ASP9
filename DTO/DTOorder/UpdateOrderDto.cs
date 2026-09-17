namespace ApiAsp0.DTO.DTOorder
{
    public class UpdateOrderDto
    {
        public int UsersId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}
