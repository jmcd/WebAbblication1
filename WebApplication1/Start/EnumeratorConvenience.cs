using System.Collections.Generic;

namespace WebApplication1.Start {
    public static class EnumeratorConvenience
    {
        public static T Next<T>(this IEnumerator<T> enumerator)
        {
            if (!enumerator.MoveNext())
            {
                enumerator.Reset();
                enumerator.MoveNext();
            }
            return enumerator.Current;
        }
    }
}