using System;
using System.Runtime.Serialization;

namespace TravelAgency.Data
{
    [Serializable]
    public class UniqueKeyException: ApplicationException
    {
        public UniqueKeyException() { }
        public UniqueKeyException(string message) : base(message) { }
        public UniqueKeyException(string message, Exception inner) : base(message, inner) { }
        protected UniqueKeyException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
