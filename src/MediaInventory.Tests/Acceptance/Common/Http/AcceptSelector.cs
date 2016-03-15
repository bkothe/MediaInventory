namespace MediaInventory.Tests.Acceptance.Common.Http
{
    public class AcceptSelector
    {
        private readonly Request _context;

        public AcceptSelector(Request context)
        {
            _context = context;
        }

        public VerbSelector IgnoreResponse()
        {
            return GetSelector(Request.DataFormat.None);
        }

        public VerbSelector AcceptJson()
        {
            return GetSelector(Request.DataFormat.Json);
        }

        public VerbSelector AcceptBinary()
        {
            return GetSelector(Request.DataFormat.Binary);
        }

        public VerbSelector AcceptText()
        {
            return GetSelector(Request.DataFormat.Text);
        }

        public VerbSelector AcceptXml()
        {
            return GetSelector(Request.DataFormat.Xml);
        }

        public VerbSelector Accept(Request.DataFormat acceptType)
        {
            return GetSelector(acceptType);
        }

        private VerbSelector GetSelector(Request.DataFormat acceptType)
        {
            var context = _context.Clone();
            context.AcceptType = acceptType;
            return new VerbSelector(context);
        }
    }
}
