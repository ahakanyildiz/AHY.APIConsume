namespace AHY.MVC.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<string> products { get; set; }
    }

    public class Root
    {
        public int id { get; set; }
        public string name { get; set; }
        public int stock { get; set; }
        public int price { get; set; }
        public DateTime createdDate { get; set; }
        public string imagePath { get; set; }
        public int categoryId { get; set; }
        public Category category { get; set; }
    }


}
