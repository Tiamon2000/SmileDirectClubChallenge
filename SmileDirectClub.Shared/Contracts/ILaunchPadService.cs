using SmileDirectClub.Shared.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmileDirectClub.Shared.Contracts
{
	public interface ILaunchPadService
    {
		Task<IEnumerable<LaunchPad>> GetLaunchPads();
    }
}
