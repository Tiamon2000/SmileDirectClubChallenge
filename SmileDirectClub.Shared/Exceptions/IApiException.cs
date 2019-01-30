using System.Net;

namespace SmileDirectClub.Shared.Exceptions
{
	public interface IApiException
    {
		HttpStatusCode StatusCode { get; }
		object Response { get; }
    }
}
