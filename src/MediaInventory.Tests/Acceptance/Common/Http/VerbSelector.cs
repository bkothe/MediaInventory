namespace MediaInventory.Tests.Acceptance.Common.Http
{
    public class VerbSelector
    {
        private readonly Request _context;

        public VerbSelector(Request context)
        {
            _context = context;
        }

        public Client Get()
        {
            return GetSelector(Request.RequestVerb.Get);
        }

        public Client Post()
        {
            return GetSelector(Request.RequestVerb.Post);
        }

        public Client Put()
        {
            return GetSelector(Request.RequestVerb.Put);
        }

        public Client Delete()
        {
            return GetSelector(Request.RequestVerb.Delete);
        }

        private Client GetSelector(Request.RequestVerb verb)
        {
            var context = _context.Clone();
            context.Verb = verb;
            return new Client(context);
        }
    }
}