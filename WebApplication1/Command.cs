namespace WebApplication1
{
    public class Command
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 10;
        public string Filter { get; set; }
        public OrderSubject OrderSubject { get; set; }
        public OrderDirection OrderDirection { get; set; }

        public Command() { }

        public Command(int pageIndex, int pageSize, string filter, OrderSubject orderSubject, OrderDirection orderDirection)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Filter = filter;
            OrderSubject = orderSubject;
            OrderDirection = orderDirection;
        }

        private Command Copied()
        {
            return new Command(PageIndex, PageSize, Filter, OrderSubject, OrderDirection);
        }

        public Command OrderedBy(OrderSubject orderSubject)
        {
            var copy = Copied();

            // todo: not sure this is the best place for this logic
            if (OrderSubject == orderSubject)
            {
                copy.OrderDirection = OrderDirection == OrderDirection.Ascending ? OrderDirection.Descending : OrderDirection.Ascending;
            }
            else
            {
                copy.OrderSubject = orderSubject;
            }
            return copy;
        }

        public Command ChangingPage(int pageIndex)
        {
            var copy = Copied();
            copy.PageIndex = pageIndex;
            return copy;
        }
    }
}