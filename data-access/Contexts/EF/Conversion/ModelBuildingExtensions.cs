using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace CSGOStats.Infrastructure.DataAccess.Contexts.EF.Conversion
{
    public static class ModelBuildingExtensions
    {
        public static PropertyBuilder<OffsetDateTime> OffsetDateTime(this PropertyBuilder<OffsetDateTime> x) =>
            x.HasConversion(new OffsetDateTimeConverter());
    }
}