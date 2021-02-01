using TravelAgency.Entities.Abstractions;

namespace TravelAgency.Entities
{
    public class Requests : EntityBase<int>
    {
        public int ID_Requests { get; set; }
        public Travellers travellers { get; set; }
        public Travels travels { get; set; }
}
}
