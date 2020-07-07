using Qmos.Entities.Abstractions;

namespace Qmos.Entities
{
    public class UpdateTime : EntityBase<short>
    {
        public string time_refresh { get; set; }
    }
}
