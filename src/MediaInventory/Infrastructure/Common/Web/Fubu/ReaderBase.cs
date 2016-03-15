using System.Collections.Generic;
using FubuCore;
using FubuCore.Binding;
using FubuCore.Binding.InMemory;
using FubuMVC.Core.Http;
using FubuMVC.Core.Resources.Conneg;

namespace MediaInventory.Infrastructure.Common.Web.Fubu
{
    public abstract class ReaderBase<T> : IReader<T>
    {
        private readonly IStreamingData _data;
        private readonly IObjectResolver _objectResolver;
        private readonly IRequestData _requestData;
        private readonly IServiceLocator _serviceLocator;
        private readonly string[] _mimeTypes;

        protected ReaderBase(
            IStreamingData data,
            IObjectResolver objectResolver,
            IRequestData requestData,
            IServiceLocator serviceLocator,
            params string[] mimeTypes)
        {
            _data = data;
            _objectResolver = objectResolver;
            _requestData = requestData;
            _serviceLocator = serviceLocator;
            _mimeTypes = mimeTypes;
        }

        public IEnumerable<string> Mimetypes => _mimeTypes;

        public T Read(string mimeType)
        {
            var data = _data.InputText();
            var model = Deserialize(data, mimeType);
            _objectResolver.BindProperties(model, new BindingContext(_requestData, _serviceLocator, new NulloBindingLogger()));
            return model;
        }

        public abstract T Deserialize(string data, string mimeType);
    }
}