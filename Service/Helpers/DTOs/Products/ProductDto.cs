namespace Service.Helpers.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public List<string> ImageUrls { get; set; }
        public ICollection<string> ColorNames { get; set; }
        public ICollection<int> Discounts { get; set; }

    }
}
