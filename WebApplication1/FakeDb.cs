using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1
{
    public class FakeDb : IDb
    {
        private static readonly ISet<Thing> persistentStorage = new HashSet<Thing>();

        public void Add(Thing thing)
        {
            persistentStorage.Add(thing);
        }

        public (int TotalCount, IEnumerable<Thing> Things) Get(Command command)
        {
            var things = (IEnumerable<Thing>) persistentStorage;

            if (!string.IsNullOrWhiteSpace(command.Filter))
            {
                things = things.Where(thing =>
                    FilterableProperties(thing).Any(attribute => attribute.Contains(command.Filter))
                );
            }

            var totalCount = things.Count();

            var orderProperty = OrderProperty(command.OrderSubject);
            var orderingFunc = OrderingFunc(orderProperty, command.OrderDirection);
            things = orderingFunc(things);

            things = things.Skip(command.PageIndex * command.PageSize).Take(command.PageSize);

            return (totalCount,things);
        }

        private static Func<IEnumerable<Thing>, IOrderedEnumerable<Thing>> OrderingFunc(Func<Thing, string> orderProperty, OrderDirection direction)
        {
            switch (direction)
            {
                case OrderDirection.Ascending:
                    return ts => ts.OrderBy(orderProperty);
                case OrderDirection.Descending:
                    return ts => ts.OrderByDescending(orderProperty);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private static Func<Thing, string> OrderProperty(OrderSubject orderSubject)
        {
            switch (orderSubject)
            {
                case OrderSubject.Attribute0:
                    return x => x.Attribute0;
                case OrderSubject.Attribute1:
                    return x => x.Attribute1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderSubject), orderSubject, null);
            }
        }

        private static IEnumerable<string> FilterableProperties(Thing thing)
        {
            return new[] {thing.Attribute0, thing.Attribute1};
        }
    }
}