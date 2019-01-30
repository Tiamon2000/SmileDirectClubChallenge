using SmileDirectClub.Shared.Contracts;
using SmileDirectClub.Shared.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmileDirectClub.Business
{
	public class LaunchPadManager : ILaunchPadManager
	{
		private readonly ILaunchPadService _launchPadService;

		public LaunchPadManager(ILaunchPadService launchPadService)
		{
			_launchPadService = launchPadService;
		}

		public async Task<IEnumerable<LaunchPad>> GetLaunchPads(LaunchPadQuery query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			query.ValidateRequest();

			var launchPads = await _launchPadService.GetLaunchPads();

			return query.ApplyFilters(launchPads).ToList();
		}
	}
}
