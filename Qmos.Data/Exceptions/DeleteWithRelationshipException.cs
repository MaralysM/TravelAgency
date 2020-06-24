using System;
using System.Runtime.Serialization;

namespace Qmos.Data
{
    [Serializable]
    public class DeleteWithRelationshipException: ApplicationException
    {
        public DeleteWithRelationshipException() { }
        public DeleteWithRelationshipException(string message) : base(message) { }
        public DeleteWithRelationshipException(string message, Exception inner) : base(message, inner) { }
        protected DeleteWithRelationshipException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
