using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1
{
    public class HomeIndexModel
    {
        private readonly Func<Command, string> urlFactory;
        public List<Thing> Things { get; }
        public Command Command { get; }
        public List<(int PageIndex, string Url)> IndexedPageUrls { get; }

        public HomeIndexModel((int TotalCount, IEnumerable<Thing> Things) countedThings, Command command, Func<Command, string> urlFactory)
        {
            var (totalCount, things) = countedThings;

            var numberOfPages = (totalCount + command.PageSize - 1) / command.PageSize;
            IndexedPageUrls = Enumerable.Range(0, numberOfPages)
                .Select(command.ChangingPage)
                .Select(newCommand => (newCommand.PageIndex, urlFactory(newCommand)))
                .ToList();

            Things = things.ToList();

            Command = command;
            this.urlFactory = urlFactory;
        }

        public string Url(Func<Command, Command> commandFactory)
        {
            var newCommand = commandFactory(Command);
            return urlFactory(newCommand);
        }
    }
}