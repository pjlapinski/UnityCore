using System;
using System.Collections.Generic;
using System.Linq;

namespace PJL.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        private static Random s_rng = new Random();

        public static void InitializeSeed(int seed) => s_rng = new Random(seed);

        #region Visit


        public static IEnumerable<TSource> Visit<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var element in source)
                action(element);

            return source;
        }

        public static IEnumerable<TSource> Visit<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func)
        {
            foreach (var element in source)
                func(element);

            return source;
        }

        public static IEnumerable<TSource> Visit<TSource>(this IList<TSource> source, Action<TSource> func)
        {
            for (var i = 0; i < source.Count; i++)
                func(source[i]);

            return source;
        }

        public static IEnumerable<TSource> Visit<TSource, TResult>(this IList<TSource> source, Func<TSource, TResult> func)
        {
            for (var i = 0; i < source.Count; i++)
                func(source[i]);

            return source;
        }

        public static IEnumerable<TSource> Visit<TSource>(this IList<TSource> source, Action<TSource, int> func)
        {
            for (var i = 0; i < source.Count; i++)
                func(source[i], i);

            return source;
        }

        public static IEnumerable<TSource> Visit<TSource, TResult>(this IList<TSource> source, Func<TSource, int, TResult> func)
        {
            for (var i = 0; i < source.Count; i++)
                func(source[i], i);

            return source;
        }


        #endregion

        #region Shuffle


        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source) =>
            source.OrderBy(_ => s_rng.NextDouble());

        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source, int seed)
        {
            var rng = new Random(seed);
            return source.OrderBy(_ => rng.NextDouble());
        }


        #endregion

        #region TakeRandom


        public static IEnumerable<TSource> TakeRandom<TSource>(this IEnumerable<TSource> source, int amount) =>
            source.Shuffle().Take(amount);

        public static IEnumerable<TSource> TakeRandom<TSource>(this IEnumerable<TSource> source, int amount, int seed) =>
            source.Shuffle(seed).Take(amount);

        public static TSource RandomOrDefault<TSource>(this IEnumerable<TSource> source) =>
            source.Shuffle().FirstOrDefault();

        public static TSource RandomOrDefault<TSource>(this IEnumerable<TSource> source, int seed) =>
            source.Shuffle(seed).FirstOrDefault();


        #endregion

        #region Zip

        public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(this IEnumerable<TFirst> source,
            IEnumerable<TSecond> other) => source.Zip(other, (first, second) => (first, second));

        public static IEnumerable<(T Value, int Index)> ZipWithIndex<T>(this IEnumerable<T> source) =>
            source.Select((el, idx) => (el, idx));

        #endregion

        #region Flatten

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source) => source.SelectMany(e => e);

        #endregion

        #region All

        public static bool All(this IEnumerable<bool> source) => source.All(b => b);

        #endregion

        #region IndexOf

        public static int IndexOf<T>(this IList<T> source, T element)
        {
            for (var i = 0; i < source.Count; i++)
                if (source[i].Equals(element))
                    return i;
            return -1;
        }

        public static int LastIndexOf<T>(this IList<T> source, T element)
        {
            for (var i = source.Count - 1; i >= 0; i++)
                if (source[i].Equals(element))
                    return i;
            return -1;
        }

        public static int FindIndexOf<T>(this IList<T> source, Func<T, bool> predicate)
        {
            for (var i = 0; i < source.Count; i++)
                if (predicate(source[i]))
                    return i;
            return -1;
        }

        public static int FindLastIndexOf<T>(this IList<T> source, Func<T, bool> predicate)
        {
            for (var i = source.Count - 1; i >= 0; i++)
                if (predicate(source[i]))
                    return i;
            return -1;
        }

        public static int FindIndexOfMax<T>(this IList<T> source) =>
            source.ZipWithIndex().MaxBy(item => item.Value).Index;

        public static int FindIndexOfMin<T>(this IList<T> source) =>
            source.ZipWithIndex().MinBy(item => item.Value).Index;

        #endregion

        #region MaxBy | MinBy

        public static TSource MinBy<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func) =>
            source.OrderBy(func).First();

        public static TSource MaxBy<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func) =>
            source.OrderByDescending(func).First();

        #endregion

        #region Empty

        public static bool Empty<T>(this IEnumerable<T> source) => !source.Any();

        #endregion

        #region Rotate

        public static IEnumerable<TSource> RotateLeft<TSource>(this IEnumerable<TSource> source, int count) => source
            .Skip(count)
            .Concat(source.Take(count));

        public static IEnumerable<TSource> RotateRight<TSource>(this IEnumerable<TSource> source, int count)
        {
            var sourceCount = source.Count();
            return source.Skip(sourceCount - count).Concat(source.Take(sourceCount - count));
        }

        #endregion
    }
}
