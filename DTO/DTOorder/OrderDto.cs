namespace ApiAsp0.DTO.DTOorder
{
    public class OrderDto
    {
        public int IdOrder { get; set; }

        public int UsersId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}
