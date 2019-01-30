using System;
using System.Net;

namespace SmileDirectClub.Shared.Exceptions
{
	public class ApiException : Exception, IApiException
    {
		public HttpStatusCode StatusCode { get; private set; }
		public object Response => null;

		public ApiException(HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = null, Exception innerException = null)
			: base(message, innerException)
		{
			StatusCode = statusCode;
		}
	}

	public class ApiException<T> : Exception, IApiException where T : class
	{
		public T Content { get; private set; }
		public HttpStatusCode StatusCode { get; private set; }
		public object Response => Content;

		public ApiException(T content = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string message = null, Exception innerException = null)
			: base(message, innerException)
		{
			Content = content;
			StatusCode = statusCode;
		}
	}
}
