namespace hotel_site.Models
{
    public class HistoryRecord
    {
        public int Id { get; set; }
        public float Cost { get; set; }
        public long Timestamp { get; set; }
        public int? HistoryActionId { get; set; }
        public virtual HistoryAction HistoryAction { get; set; }
        public string HistoryActionName { get; set; } //на случай пропажи HistoryAction из базы данных
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public string UserName { get; set; } //на случай пропажи User из базы данных
        public int? RoomId { get; set; }
        public virtual Room Room { get; set; }
        public string RoomNumber { get; set; } //на случай пропажи Room из базы данных
        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public string ServiceName { get; set; } //на случай пропажи Service из базы данных

        public HistoryRecord(int id, float cost, long timestamp)
        {
            Id = id;
            Cost = cost;
            Timestamp = timestamp;
        }
    }
}
