using System;

namespace hotel_site.Models.ViewModels
{
    public class BookRequirements
    {
        public DateTime MomentStart { get; }
        public DateTime MomentEnd { get; }
        public int PersonCount { get; }
        public int NightCount { get; }

        public BookRequirements()
        {
        }

        public BookRequirements(DateTime momentStart, DateTime momentEnd, int personCount)
        {
            MomentStart = momentStart;
            MomentEnd = momentEnd;
            PersonCount = personCount;
            NightCount = (momentEnd - momentStart).Days;
        }
    }
}
