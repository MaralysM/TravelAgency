using Qmos.Entities.Abstractions;

namespace Qmos.Entities
{
    public class ReferenceParameters : EntityBase<short>
    {
        public int id_element { get; set; }
        public short id_child { get; set; }
        public string reference { get; set; }
        public string refmin { get; set; }
        public string refmax { get; set; }
        public string name_element { get; set; }
        public ChildElement childElement { get; set; }
    }
}
