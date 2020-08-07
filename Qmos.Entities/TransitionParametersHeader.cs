using Qmos.Entities.Abstractions;

namespace Qmos.Entities
{
    public class TransitionParametersHeader : EntityBase<short>
    {
        public TransitionParametersDetails TransitionParametersDetails { get; set; }
    }
}
