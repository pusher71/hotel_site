namespace hotel_site.Models
{
    public class HistoryAction
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HistoryAction(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
