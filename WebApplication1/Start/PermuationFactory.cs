using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Start {
    public static class PermuationFactory
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IList<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(t => new[] {t});
            }

            return Enumerable.SelectMany<IEnumerable<T>, T, IEnumerable<T>>(GetPermutations(list, length - 1), t => list.Where(e => !Enumerable.Contains(t, e)), (t1, t2) => Enumerable.Concat<T>(t1, new[] {t2}));
        }
    }
}