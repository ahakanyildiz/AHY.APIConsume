using System.ComponentModel.DataAnnotations;

namespace AHY.WebApi.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
