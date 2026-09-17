namespace ApiAsp0.DTO.DTOproduct
{
    public class UpdateProductDto
    {
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public int CategoryId { get; set; }
    }
}
