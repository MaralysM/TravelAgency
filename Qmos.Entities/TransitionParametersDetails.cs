using Qmos.Entities.Abstractions;

namespace Qmos.Entities
{
    public class TransitionParametersDetails : EntityBase<short>
    {
        public short id_transition_parameters_header { get; set; }
        public string time_transition { get; set; }
        public short order_transition { get; set; }
        public int id_element { get; set; }
    }
}
