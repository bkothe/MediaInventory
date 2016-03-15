using System;
using System.Collections.Generic;

namespace MediaInventory.Tests.Acceptance.Common.Http
{
    public class ContentTypeSelector
    {
        private readonly Request _context;

        public ContentTypeSelector(Request context)
        {
            _context = context;
        }

        public AcceptSelector SendNothing()
        {
            return GetSelector(Request.DataFormat.None);
        }

        public AcceptSelector Send(Request.DataFormat contentType)
        {
            return GetSelector(contentType);
        }

        public Response<T> Get<T>(Request.DataFormat format)
        {
            return SendNothing().Accept(format).Get().Execute<T>();
        }

        public Response<string> Get(Request.DataFormat format)
        {
            return SendNothing().Accept(format).Get().Execute();
        }

        public Response<string> Post(Request.DataFormat format, string data)
        {
            return Send(format).Accept(format).Post().Execute(data);
        }

        public Response<string> Post<TRequest>(Request.DataFormat format, TRequest data)
        {
            return Send(format).IgnoreResponse().Post().Execute<TRequest, string>(data);
        }

        public Response<TResponse> Post<TRequest, TResponse>(Request.DataFormat format, TRequest data)
        {
            return Send(format).Accept(format).Post().Execute<TRequest, TResponse>(data);
        }

        public Response<string> Put<TRequest>(Request.DataFormat format, TRequest data)
        {
            return Send(format).IgnoreResponse().Put().Execute<TRequest, string>(data);
        }

        public Response<TResponse> Put<TRequest, TResponse>(Request.DataFormat format, TRequest data)
        {
            return Send(format).Accept(format).Put().Execute<TRequest, TResponse>(data);
        }

        public Response<string> Delete()
        {
            return SendNothing().IgnoreResponse().Delete().Execute();
        }

        public Response<string> Delete<TRequest>(Request.DataFormat format, TRequest data)
        {
            return SendNothing().IgnoreResponse().Delete().Execute<TRequest, string>(data);
        }

        // Text

        public AcceptSelector SendText()
        {
            return GetSelector(Request.DataFormat.Text);
        }

        public Response<string> GetText()
        {
            return SendNothing().AcceptText().Get().Execute<string>();
        }

        // Json

        public AcceptSelector SendJson()
        {
            return GetSelector(Request.DataFormat.Json);
        }

        public Response<T> GetJson<T>(Dictionary<string, string> headers = null)
        {
            return SendNothing().AcceptJson().Get().Execute<T>(headers: headers);
        }

        public Response<TResponse> PostJson<TRequest, TResponse>(TRequest data)
        {
            return SendJson().AcceptJson().Post().Execute<TRequest, TResponse>(data);
        }

        public Response<string> PostJson<TRequest>(TRequest data)
        {
            return SendJson().AcceptJson().Post().Execute<TRequest, string>(data);
        }

        public Response<TResponse> PostJson<TRequest, TResponse>(
            Action<TRequest> configure, Dictionary<string, string> headers = null)
            where TRequest : new()
        {
            var data = new TRequest();
            configure(data);
            return SendJson().AcceptJson().Post()
                .Execute<TRequest, TResponse>(data, headers: headers);
        }

        public Response<string> PutJson<TRequest>(TRequest data)
        {
            return SendJson().IgnoreResponse().Put().Execute<TRequest, string>(data);
        }

        // Xml

        public AcceptSelector SendXml()
        {
            return GetSelector(Request.DataFormat.Xml);
        }

        public Response<T> GetXml<T>()
        {
            return SendNothing().AcceptXml().Get().Execute<T>();
        }

        // Binary

        public AcceptSelector SendBinary()
        {
            return GetSelector(Request.DataFormat.Binary);
        }

        public Response<byte[]> GetBinary()
        {
            return SendNothing().AcceptBinary().Get().Execute<byte[]>();
        }

        // Form

        public AcceptSelector SendForm()
        {
            return GetSelector(Request.DataFormat.Form);
        }

        private AcceptSelector GetSelector(Request.DataFormat contentType)
        {
            var context = _context.Clone();
            context.ContentType = contentType;
            return new AcceptSelector(context);
        }
    }
}