namespace AHY.MVC.Models
{
    public class ProductCreateModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ImagePath { get; set; } = null;
        public int CategoryId { get; set; } = 1;
        public Category category = null;
       
  }
}

