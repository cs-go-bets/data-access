using CSGOStats.Infrastructure.DataAccess.Contexts.Mongo;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities;
using MongoDB.Bson.Serialization;

namespace CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Contexts
{
    public static class BsonMappings
    {
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<TestDocumentBase>(mapper => mapper.MapGuid(x => x.Id));

            BsonClassMap.RegisterClassMap<TestDocument>(mapper =>
            {
                mapper.MapOptional(x => x.Inner);
                mapper.MapRequired(x => x.Version);
                mapper.MapOffsetDateTime(x => x.UpdatedOn);
                mapper.MapCreator(x => new TestDocument(x.Id, x.Inner, x.Version, x.UpdatedOn));
            });

            BsonClassMap.RegisterClassMap<InnerDocument>(mapper =>
            {
                mapper.MapRequired(x => x.Data);
                mapper.MapRequired(x => x.Count);
                mapper.MapOffsetDateTime(x => x.LockDate);
            });
        }
    }
}