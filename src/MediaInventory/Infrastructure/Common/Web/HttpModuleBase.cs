using System.Web;

namespace Reachmail.Infrastructure.Framework.Web
{
    public abstract class HttpModuleBase : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            Init(new HttpApplicationWrapper(context));
        }

        public abstract void Init(IHttpApplication context);

        public virtual void Dispose() { }
    }
}
