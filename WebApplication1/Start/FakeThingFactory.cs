using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Start
{
    public class FakeThingFactory
    {
        public static IEnumerable<Thing> Construct(int numberOfThings)
        {
            var letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            const int stringLength = 3;
            var strings0 = ConstructPermutationEnumerator(letters, stringLength);
            var strings1 = ConstructPermutationEnumerator(letters.Reverse().ToList(), stringLength);

            var things = Enumerable.Range(0, numberOfThings).Select(_ => new Thing(strings0.Next(), strings1.Next()));
            return things;
        }

        private static IEnumerator<string> ConstructPermutationEnumerator(IList<char> characters, int stringLength)
        {
            return PermuationFactory.GetPermutations(characters, stringLength).Select(cs => cs.Aggregate("", (s, c) => s + c)).GetEnumerator();
        }
    }
}