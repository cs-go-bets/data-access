using System;

namespace CSGOStats.Infrastructure.DataAccess.Extensions
{
    public static class ValidationExtensions
    {
        [Obsolete("Should be replaced with corresponding package call.")]
        public static T NotNull<T>(this T instance, string argumentName)
            where T : class => instance ?? throw new ArgumentNullException(argumentName);
    }
}