using System;
using System.Runtime.Serialization;

namespace CSGOStats.Infrastructure.DataAccess.Entities
{
    [Serializable]
    public class EntityNotFound : Exception
    {
        public string Type { get; }

        public object Id { get; }

        public EntityNotFound(string type, object id)
        {
            Type = type;
            Id = id;
        }

        protected EntityNotFound(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
