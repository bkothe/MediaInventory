using System;
using System.Web;

namespace MediaInventory.Infrastructure.Common.Web
{
    public interface IHttpApplication
    {
        void OnBeginRequest(Action<IHttpContext> handler);
        void OnError(Action<IHttpContext, Exception> handler);
        void OnEndRequest(Action<IHttpContext> handler);
    }

    public class HttpApplicationWrapper : IHttpApplication
    {
        private readonly HttpApplication _context;

        public HttpApplicationWrapper(HttpApplication context)
        {
            _context = context;
        }

        public void OnBeginRequest(Action<IHttpContext> handler)
        {
            _context.BeginRequest += handler.ToEventHandler();
        }

        public void OnError(Action<IHttpContext, Exception> handler)
        {
            _context.Error += (sender, args) => handler(new HttpContextWrapper(), 
                HttpContext.Current.Server.GetLastError());
        }

        public void OnEndRequest(Action<IHttpContext> handler)
        {
            _context.EndRequest += handler.ToEventHandler();
        }
    }
}
