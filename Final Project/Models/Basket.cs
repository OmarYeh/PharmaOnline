using System.Numerics;

namespace Final_Project.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<BasketItems> BasketItems { get; set; }

    }
}
