namespace Final_Project.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public DateTime Time { get; set; }
        
        public String ImageUrl { get; set; }
        public String MadeInCountry { get; set; } 
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<BasketItems> BasketItems { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
