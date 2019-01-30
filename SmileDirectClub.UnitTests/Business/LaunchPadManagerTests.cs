using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmileDirectClub.Business;
using SmileDirectClub.Shared.Contracts;
using SmileDirectClub.Shared.DomainModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmileDirectClub.UnitTests.Business
{
	[TestClass]
	public class LaunchPadManagerTests
	{
		private readonly List<LaunchPad> _launchPads = new List<LaunchPad>()
		{
			new LaunchPad()
			{
				Id = "1",
				Status = "active",
				Name = "Super Rocket Complex"
			},
			new LaunchPad()
			{
				Id = "2",
				Status = "retired",
				Name = "NASA Starport"
			}
		};

		[TestMethod]
		public async Task GetLaunchPads_NoFilters()
		{
			var launchPadService = new Mock<ILaunchPadService>();
			launchPadService.Setup(x => x.GetLaunchPads()).ReturnsAsync(_launchPads);

			var launchPadManager = new LaunchPadManager(launchPadService.Object);
			var launchPadQuery = new LaunchPadQuery();

			var result = await launchPadManager.GetLaunchPads(launchPadQuery);

			Assert.AreEqual(2, result.Count());
			Assert.IsTrue(_launchPads.SequenceEqual(result));
		}
	}
}
