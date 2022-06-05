using System.Collections.Generic;

namespace hotel_site.Models.ViewModels
{
    public class RoomsViewData
    {
        public HotelBuilding HotelBuilding { get; }
        public BookRequirements BookRequirements { get; }
        public ISet<string> RoomFloors { get; }
        public Dictionary<string, List<Room>> Rooms { get; }

        public RoomsViewData(HotelBuilding hotelBuilding, BookRequirements bookRequirements, IEnumerable<Book> books)
        {
            RoomFloors = new HashSet<string>();
            Rooms = new Dictionary<string, List<Room>>();
            HotelBuilding = hotelBuilding;
            BookRequirements = bookRequirements;
            foreach (Room room in hotelBuilding.Rooms)
            {
                if (room.IsAvailable &&
                    bookRequirements.PersonCount <= room.MaxPersonCount)
                {
                    bool free = true;
                    foreach (Book existingBook in books)
                        if (existingBook.RoomId == room.Id && existingBook.MomentStart < bookRequirements.MomentEnd && existingBook.MomentEnd > bookRequirements.MomentStart)
                            free = false;

                    if (free)
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
}
