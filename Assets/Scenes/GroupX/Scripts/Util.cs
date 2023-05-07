using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;

namespace GroupX
{
    /// <summary>
    /// Miscellaneous utility functions
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Removes last {amount} items from list.
        /// </summary>
        /// <param name="count">How many items to remove</param>
        public static void RemoveLast<T>(this List<T> list, int amount)
        {
            if (amount > list.Count)
                amount = list.Count;

            list.RemoveRange(list.Count - amount, amount);
        }

        /// <summary>
        /// Remaps value from between iMin & iMax to be between oMin & oMax instead, at the same position
        /// </summary>
        /// <param name="iMin">Original minimum</param>
        /// <param name="iMax">Original maximum</param>
        /// <param name="oMin">New minimum</param>
        /// <param name="oMax">New maximum</param>
        /// <param name="v">Value (between original minimum & maximum)</param>
        /// <returns></returns>
        public static float Remap(float iMin, float iMax, float oMin, float oMax, float v)
        {
            float t = Mathf.InverseLerp(iMin, iMax, v);
            return Mathf.Lerp(oMin, oMax, t);
        }

        // https://stackoverflow.com/questions/15905515/getting-indexes-of-all-matching-items
        public static IEnumerable<int> IndecesWhere<T>(this IList<T> source, Func<T, bool> predicate)
        {
            for (int i = 0; i < source.Count; i++)
                if (predicate(source[i]))
                    yield return i;
        }

        public static T PickRandomItem<T>(this IEnumerable<T> source)
        {
            int randomIndex = Random.Range(0, source.Count());
            return source.ElementAt(randomIndex);
        }
    }
}