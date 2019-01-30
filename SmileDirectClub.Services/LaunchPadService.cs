using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SmileDirectClub.Services.AutoMapper;
using SmileDirectClub.Services.Dtos;
using SmileDirectClub.Shared.Contracts;
using SmileDirectClub.Shared.DomainModels;
using SmileDirectClub.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmileDirectClub.Services
{
	public class LaunchPadService : ILaunchPadService
	{
		private SpaceXApiSettings _spaceXApiSettings;

		public LaunchPadService(IOptions<SpaceXApiSettings> spaceXApiSettings)
		{
			_spaceXApiSettings = spaceXApiSettings.Value;
		}

		public async Task<IEnumerable<LaunchPad>> GetLaunchPads()
		{
			var launchPadDtos = await ServiceClient.Get<List<LaunchPadDto>>(_spaceXApiSettings.LaunchPadsUri);

			return launchPadDtos.ProjectTo<LaunchPad>();
		}
	}
}
