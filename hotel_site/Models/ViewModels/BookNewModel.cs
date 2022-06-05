using System;

namespace hotel_site.Models.ViewModels
{
    public class BookNewModel
    {
        public Room Room { get; set; }
        public DateTime MomentStart { get; set; }
        public DateTime MomentEnd { get; set; }
        public int PersonCount { get; set; }
    }
}
