using TravelAgency.Entities.Abstractions;

namespace TravelAgency.Entities
{
    public class Travels : EntityBase<int>
    {
        public int ID_Travels { get; set; }
        public long NU_TravelCode { get; set; }
        public int NU_NumberOfPlace { get; set; }
        public string TX_Destination { get; set; }
        public string TX_Origin { get; set; }
        public decimal NU_Price { get; set; }
    }
}
