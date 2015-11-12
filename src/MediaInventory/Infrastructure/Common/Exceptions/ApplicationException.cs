using System;
using System.Runtime.Serialization;

namespace MediaInventory.Infrastructure.Common.Exceptions
{
    public class ApplicationException : Exception
    {
        private string _id;

        public ApplicationException() { Init(null); }
        public ApplicationException(string message) : base(message) { Init(null); }
        public ApplicationException(Exception exception) : base(exception.Message, exception) { Init(exception); }
        public ApplicationException(string message, Exception innerException) : base(message, innerException) { Init(innerException); }
        public ApplicationException(string format, params string[] values) : base(string.Format(format, values)) { Init(null); }
        public ApplicationException(Exception innerException, string format, params string[] values) : base(string.Format(format, values), innerException) { Init(innerException); }
        public ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context) { Init(null); }

        public virtual string Id { get { return _id; } }

        private void Init(Exception innerException)
        {
            // If there is an inner exception check to see if there
            // is another ExceptionBase derived exception somewhere
            // along the line. If so we want to use the existing 
            // exception id not create a new one.
            if (innerException != null)
            {
                Exception currentException = innerException;
                do
                {
                    if (currentException is ApplicationException)
                    {
                        _id = ((ApplicationException)currentException).Id;
                        return;
                    }

                    currentException = currentException.InnerException;
                } while (currentException != null);
            }

            _id = DateTime.Now.ToString("hhmmss-fff");
        }
    }
}