using System;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue = default(TValue))
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            TValue result;
            return dictionary.TryGetValue(key, out result) ? result : defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary, 
            TKey key,
            TValue defaultValue = default(TValue))
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return dictionary.GetValueOrDefaultForReadOnly(key, defaultValue);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue = default(TValue))
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return dictionary.GetValueOrDefaultForReadOnly(key, defaultValue);
        }

        public static TValue GetValueOrDefaultForReadOnly<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue = default(TValue))
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            TValue result;
            return dictionary.TryGetValue(key, out result) ? result : defaultValue;
        }

        public static TValue? GetNullableValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : struct
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            TValue result;
            return dictionary.TryGetValue(key, out result) ? result : (TValue?)null;
        }

        public static TValue? GetNullableValueOrNull<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary, 
            TKey key)
            where TValue : struct
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return dictionary.GetNullableValueOrNullForReadOnly(key);
        }

        public static TValue? GetNullableValueOrNull<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
            where TValue : struct
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return dictionary.GetNullableValueOrNullForReadOnly(key);
        }

        public static TValue? GetNullableValueOrNullForReadOnly<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key)
            where TValue : struct
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            TValue result;
            return dictionary.TryGetValue(key, out result) ? result : (TValue?)null;
        }
    }
}
