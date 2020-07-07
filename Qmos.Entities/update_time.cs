using Qmos.Entities.Abstractions;

namespace Qmos.Entities
{
    public class update_time : EntityBase<short>
    {
        public string time_refresh { get; set; }
    }
}
