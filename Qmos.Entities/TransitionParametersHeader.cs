using Qmos.Entities.Abstractions;
using System.Collections.Generic;

namespace Qmos.Entities
{
    public class TransitionParametersHeader : EntityBase<short>
    {
        public TransitionParametersDetails TransitionParametersDetailsEntity { get; set; }
        public IList<TransitionParametersDetails> transitionParametersDetails { get; set; }
        public TransitionParametersHeader()
        {
            TransitionParametersDetailsEntity = new TransitionParametersDetails();
            transitionParametersDetails = new List<TransitionParametersDetails>();
        }
    }
}
