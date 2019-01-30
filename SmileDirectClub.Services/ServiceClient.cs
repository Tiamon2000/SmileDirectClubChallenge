using Newtonsoft.Json;
using SmileDirectClub.Shared.Exceptions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmileDirectClub.Services
{
	public static class ServiceClient
	{
		private static readonly HttpClient _client = new HttpClient();

		public static async Task<T> Get<T>(Uri uri)
		{
			try
			{
				var response = await _client.GetAsync(uri);
				var content = await response.Content.ReadAsStringAsync();
				
				if (!response.IsSuccessStatusCode)
					throw new ApiException(response.StatusCode, content);

				return JsonConvert.DeserializeObject<T>(content);
			}
			catch (Exception ex)
			{
				throw new ApiException(HttpStatusCode.InternalServerError, "HttpClient.GetAsync threw an exception. See inner exception for details.", ex);
			}
		}
	}
}
