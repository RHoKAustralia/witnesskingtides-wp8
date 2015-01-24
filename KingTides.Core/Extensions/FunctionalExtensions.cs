using System;

namespace KingTides.Core.Extensions
{
    public static class FunctionalExtensions
    {
        public static TResult Maybe<T, TResult>(
            this T _this,
            Func<T, TResult> func,
            TResult defaultReturnValue = default(TResult)) where T : class
        {
            return (_this == null) ? defaultReturnValue : func(_this);
        }
    }

    public static class StringExtensions
    {
        public static string NullIfEmptyOrWhiteSpace(this string _this)
        {
            if (string.IsNullOrWhiteSpace(_this)) return null;
            return _this;
        }
    }
}