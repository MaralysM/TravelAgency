using Qmos.Entities.Abstractions;

namespace Qmos.Entities
{
    public class ReferenceParameters : EntityBase<short>
    {
        public int id_element { get; set; }
        public short cant_ref { get; set; }
        public decimal? ref1 { get; set; }
        public decimal? ref2 { get; set; }
        public string name_element { get; set; }
    }
}
