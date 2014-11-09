﻿using System;

namespace KingTides.Core.Extensions
{
    internal static class FunctionalExtensions
    {
        public static TResult Maybe<T, TResult>(
            this T _this,
            Func<T, TResult> func,
            TResult defaultReturnValue = default(TResult)) where T : class
        {
            return (_this == null) ? defaultReturnValue : func(_this);
        }
    }
}