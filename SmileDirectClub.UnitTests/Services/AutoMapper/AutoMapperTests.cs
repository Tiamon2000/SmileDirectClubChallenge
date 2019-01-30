using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmileDirectClub.Services.AutoMapper;
using System;

namespace SmileDirectClub.UnitTests.Services.AutoMapper
{
	[TestClass]
	public class LaunchPadQueryTests
	{
		[TestMethod]
		public void TestMappingConfiguration()
		{
			try
			{
				Mappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
			}
			catch (Exception ex)
			{
				Assert.Fail($"Mappers.Mapper configuration does not pass validation.{Environment.NewLine}{ex.Message}");
			}
		}
	}
}
