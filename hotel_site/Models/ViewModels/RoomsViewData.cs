using System.Collections.Generic;

namespace hotel_site.Models.ViewModels
{
    public class RoomsViewData
    {
        public HotelBuilding HotelBuilding { get; }
        public ISet<string> RoomFloors { get; }
        public Dictionary<string, List<Room>> Rooms { get; }

        public RoomsViewData(HotelBuilding hotelBuilding)
        {
            RoomFloors = new HashSet<string>();
            Rooms = new Dictionary<string, List<Room>>();
            HotelBuilding = hotelBuilding;
            foreach (Room room in hotelBuilding.Rooms)
            {
                if (room.IsAvailable)
                {
                    RoomFloors.Add(room.Floor);
                    if (!Rooms.ContainsKey(room.Floor))
                        Rooms.Add(room.Floor, new List<Room>());
                    Rooms[room.Floor].Add(room);
                }
            }
        }
    }
}
