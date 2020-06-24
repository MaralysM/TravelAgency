using System;
using Qmos.Entities.Enums;

namespace Qmos.Entities
{
  public  class LoggerActions
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long UserId { get; set; }
        public DateTime Time { get; set; }
        public TypeActions  TypeAction  { get; set; }
        public string Message { get; set; }
    }
}
