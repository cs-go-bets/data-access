using CSGOStats.Infrastructure.DataAccess.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace CSGOStats.Infrastructure.DataAccess.Contexts.EF.Conversion
{
    public class OffsetDateTimeConverter : ValueConverter<OffsetDateTime, string>
    {
        public OffsetDateTimeConverter()
            : base(
                offsetDateTime => offsetDateTime.Serialize(),
                @string => @string.Deserialize())
        {
        }
    }
}