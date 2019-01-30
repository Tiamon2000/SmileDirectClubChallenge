using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmileDirectClub.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmileDirectClub.Api.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
		/* A NOTE ABOUT THE EXCEPTION HANDLING APPROACH
		 * 
		 * I know there are different opinions about how to return non-success results from an api
		 * and I don't have strong opinions one way or another
		 * 
		 * Here, I've chosing to do an exception flow
		 * Throwing an exception inheriting from IApiException allows lower-level code to control the Api's non-success response
		 *		i.e. status code and response message
		 * This approach allows different json responses for different status codes
		 * while keeping method return types in the business and service layers concerned only with the happy path
		 */

		public static void ConfigureExceptionHandling(this IApplicationBuilder app, ILogger logger)
		{
			app.UseExceptionHandler(error =>
			{
				error.Run(async context =>
				{
					var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
					var exception = exceptionHandlerFeature.Error;

					var apiException = exception as IApiException;

					string responseMessage = null;
					string logMessage = null;

					if (apiException != null)
					{
						context.Response.StatusCode = (int)apiException.StatusCode;

						logMessage = $"Global error handling caught an api exception with status code '{apiException.StatusCode}'. See exception for details.";

						if (apiException.Response != null)
						{
							// custom error message set by the coding throw an IApiException
							responseMessage = JsonConvert.SerializeObject(apiException.Response, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
						}
					}
					else
					{
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

						logMessage = "Global error handling caught an exception. See exception for details.";
					}

					// log warning for 4xx (client error) status codes
					// log error for other status codes
					if (context.Response.StatusCode / 100 == 4)
						logger.LogWarning(new EventId(), exception, logMessage);
					else
						logger.LogError(new EventId(), exception, logMessage);

					if (responseMessage == null)
					{
						// default error message
						responseMessage = JsonConvert.SerializeObject(new { success = false, message = "An error has occurred." });
					}

					context.Response.ContentType = "application/json";

					await context.Response.WriteAsync(responseMessage).ConfigureAwait(false);
				});
			});
		}
	}
}
