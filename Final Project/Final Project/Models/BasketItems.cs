namespace Final_Project.Models
{
    public class BasketItems
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
 
