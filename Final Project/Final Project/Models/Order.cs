namespace Final_Project.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string PaymentType { get; set; }
        public string TelephoneNumber { get; set;}

        public ICollection<Item> Items { get; set; }

    }
}
