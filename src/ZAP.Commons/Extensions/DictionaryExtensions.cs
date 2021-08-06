using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAP.Commons.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrModify<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }

        public static T GetSelfOrNearestAncestor<T>(this Dictionary<Type, T> dict, Type ctype)
        {
            Type matchingType = null;
            T matchingResult = default;
            foreach (var item in dict)
            {
                if (ctype.IsAssignableFrom(item.Key) && (matchingType == null || matchingType.IsAssignableFrom(item.Key)))
                {
                    matchingResult = item.Value;
                    matchingType = item.Key;
                }
            }

            return matchingResult;
        }

        public static void Concat<T, K>(this Dictionary<T, K> @base, Dictionary<T, K> dictToConcat)
        {
            foreach (var item in dictToConcat)
            {
                @base.AddOrModify(item.Key, item.Value);
            }
        }
    }
}
