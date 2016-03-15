using System;
using System.Net;
using System.Reflection;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using MediaInventory.Infrastructure.Common;
using MediaInventory.Infrastructure.Common.Exceptions;
using MediaInventory.Infrastructure.Common.Reflection;
using MediaInventory.Infrastructure.Common.Web;
using MediaInventory.Infrastructure.Common.Web.Exceptions;
using ApplicationException = MediaInventory.Infrastructure.Common.Exceptions.ApplicationException;

namespace MediaInventory.Infrastructure.Application.Web
{
    public class RestExceptionHandlerBehavior : IActionBehavior
    {
        private static readonly string[] TestUrls = { "/Rest/test", "/test" };

        private readonly IActionBehavior _innerBehavior;
        private readonly IOutputWriter _outputWriter;
        //private readonly ISession _session;

        private readonly IHttpStatus _httpStatus;
        private readonly IResponseHeaders _responseHeaders;
        private readonly IRequestHeaders _requestHeaders;

        public RestExceptionHandlerBehavior(
            IActionBehavior innerBehavior,
            IOutputWriter outputWriter,
            IRequestHeaders requestHeaders,
            IHttpStatus httpStatus,
            IResponseHeaders responseHeaders)
        {
            _innerBehavior = innerBehavior;
            _outputWriter = outputWriter;
            //_session = session;
            _httpStatus = httpStatus;
            _responseHeaders = responseHeaders;
            _requestHeaders = requestHeaders;
            ReturnError = Assembly.GetExecutingAssembly().IsInDebugMode();
        }

        public bool ReturnError { get; set; }

        public void Invoke()
        {
            try
            {
                _innerBehavior.Invoke();
            }
            catch (Exception exception)
            {
                if (exception is ValidationException) SetStatus(HttpStatusCode.BadRequest, exception.Message);
                else if (exception is NotFoundException)
                {
                    var notFoundException = (NotFoundException) exception;
                    SetStatus(HttpStatusCode.NotFound, "The {0} id '{1}' you requested does not exist.".ToFormat(notFoundException.Name, notFoundException.Key));
                }
                else LogUnhandledException(exception, true);

                if (ReturnError)
                    _outputWriter.Write(MimeType.Text, exception.ToString());
            }
        }

        private void LogUnhandledException(Exception exception, bool systemError = false)
        {
            var applicationException = exception is ApplicationException ? (ApplicationException)exception :
                new UnhandledWebException("An unhandled Fubu endpoint exception has occurred.", _requestHeaders, exception);
            if (systemError) SetStatus(HttpStatusCode.InternalServerError, "A system error has occurred ({0}).".ToFormat(applicationException.Id));
            //_logger.Write(applicationException);
        }

        public void InvokePartial()
        {
            _innerBehavior.InvokePartial();
        }

        private void SetStatus(HttpStatusCode code, string description)
        {
            _httpStatus.Set(code, description);
            _responseHeaders["status-text"] = description;
        }
    }
}
