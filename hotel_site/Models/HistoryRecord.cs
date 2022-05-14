namespace hotel_site.Models
{
    public class HistoryRecord
    {
        public int Id { get; set; }
        public float Cost { get; set; }
        public long Timestamp { get; set; }
        public int? HistoryActionId { get; set; }
        public virtual HistoryAction HistoryAction { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public int? RoomId { get; set; }
        public virtual Room Room { get; set; }
        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public HistoryRecord(int id, float cost, long timestamp)
        {
            Id = id;
            Cost = cost;
            Timestamp = timestamp;
        }
    }
}
