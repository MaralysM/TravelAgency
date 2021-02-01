using TravelAgency.Entities.Abstractions;

namespace TravelAgency.Entities
{
    public class Travellers : EntityBase<int>
    {
        public int ID_Travellers { get; set; }
        public string TX_FirstName { get; set; }
        public string TX_SecondName { get; set; }
        public string TX_LastName { get; set; }
        public string TX_SecondLastName { get; set; }
        public string TX_Phone { get; set; }
        public string TX_IdentificationCard { get; set; }
        public string TX_Address { get; set; }
    }
}
