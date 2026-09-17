namespace ApiAsp0.DTO.DTOorderItem
{
    public class UpdateOrderItemDto
    {
        public int CartItemId { get; set; }

        public decimal OrderPrice { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }
    }
}
