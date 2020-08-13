using Qmos.Entities.Abstractions;

namespace Qmos.Entities
{
    public class ReferenceParameters : EntityBase<short>
    {
        public int id_element { get; set; }
        public string reference { get; set; }
        public string name_element { get; set; }
    }
}
