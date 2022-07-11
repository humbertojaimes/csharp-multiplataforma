using System;
using DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SpeakersApiClient
{
	public class SpeakersApiClient : ApiClient<Speaker>
	{
		public SpeakersApiClient(ILogger logger, IConfiguration configuration)
            : base(logger, configuration["SpeakersEndpoint"])
		{
		}

		public SpeakersApiClient(IConfiguration configuration)
			: base(null!, configuration["SpeakersEndpoint"])
		{
		}

		public SpeakersApiClient(string uri)
			: base(null!, uri)
		{
		}

	}
}

