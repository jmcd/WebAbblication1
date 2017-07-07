using System.Collections.Generic;

namespace WebApplication1
{
    public interface IDb
    {
        void Add(Thing thing);
        (int TotalCount, IEnumerable<Thing> Things) Get(Command command);
    }
}