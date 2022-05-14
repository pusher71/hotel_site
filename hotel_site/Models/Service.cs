namespace hotel_site.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public Service(int id, string name, float price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
