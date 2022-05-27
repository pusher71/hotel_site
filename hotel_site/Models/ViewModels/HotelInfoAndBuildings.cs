using System.Collections.Generic;

namespace hotel_site.Models.ViewModels
{
    public class HotelInfoAndBuildings
    {
        public HotelInfo HotelInfo { get; set; }
        public IEnumerable<HotelBuilding> HotelBuildings { get; set; }
    }
}
