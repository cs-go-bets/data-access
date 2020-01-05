using System;
using CSGOStats.Extensions.Extensions;
using MongoDB.Bson.Serialization;
using NodaTime;

namespace CSGOStats.Infrastructure.DataAccess.Serialization
{
    public class NullableOffsetDateTimeMongoSerializer : IBsonSerializer
    {
        public static IBsonSerializer Instance { get; } = new NullableOffsetDateTimeMongoSerializer();

        public Type ValueType => typeof(OffsetDateTime?);

        private NullableOffsetDateTimeMongoSerializer()
        {
        }

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) =>
            context.Reader.ReadString().Deserialize();

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value) =>
            context.Writer.WriteString(value.OfType<OffsetDateTime?>().Value.Serialize());
    }
}