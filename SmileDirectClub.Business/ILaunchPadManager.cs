using SmileDirectClub.Shared.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmileDirectClub.Business
{
	public interface ILaunchPadManager
	{
		Task<IEnumerable<LaunchPad>> GetLaunchPads(LaunchPadQuery query);
	}
}
