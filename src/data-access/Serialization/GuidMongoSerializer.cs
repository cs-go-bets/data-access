using System;
using CSGOStats.Extensions.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace CSGOStats.Infrastructure.DataAccess.Serialization
{
    public class GuidMongoSerializer : IBsonSerializer
    {
        public static IBsonSerializer Instance { get; } = new GuidMongoSerializer();

        public Type ValueType => typeof(Guid);

        private GuidMongoSerializer()
        {
        }

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) =>
            context.Reader.ReadBinaryData().AsGuid;

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var data = new BsonBinaryData(value.OfType<Guid>(), GuidRepresentation.Standard);
            context.Writer.WriteBinaryData(data);
        }
    }
}