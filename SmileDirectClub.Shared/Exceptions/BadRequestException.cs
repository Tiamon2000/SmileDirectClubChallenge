using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SmileDirectClub.Shared.Exceptions
{
    public class BadRequestException : ApiException<BadRequest>
    {
		public BadRequestException(BadRequest content = null, string message = null, Exception innerException = null)
			: base(content, HttpStatusCode.BadRequest, message, innerException)
		{

		}
	}
}
