using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmileDirectClub.Api.Dtos;
using SmileDirectClub.Business;
using SmileDirectClub.Shared.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmileDirectClub.Api.Controllers
{
	[Route("api/launchpads")]
    public class LaunchPadController : Controller
	{
		private readonly ILaunchPadManager _launchPadManager;
		private readonly IMapper _mapper;

		public LaunchPadController(ILaunchPadManager launchPadManager, IMapper mapper)
		{
			_launchPadManager = launchPadManager;
			_mapper = mapper;
		}

		// GET api/launchpads
		[HttpGet]
        public async Task<IEnumerable<LaunchPadDto>> Get([FromQuery]LaunchPadQuery query)
        {
			var launchPads = await _launchPadManager.GetLaunchPads(query);

			// it is a bit redundant to have separate domain and api (dto) models, especially when there is only one and the properties are the same
			// however, having separate models would allow the business domain or api to change independently from each other
			// with the mapping to bridge the changes

			// I also realize I used the save class name LaunchPadDto both in this project and in SmileDirectClub.Services
			// however, the way the projects are structured, we should never have to reference both types of dto models in the same place

			return _mapper.Map<IEnumerable<LaunchPadDto>>(launchPads);
        }
    }
}
