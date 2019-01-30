using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmileDirectClub.Api.AutoMapper;
using System;

namespace SmileDirectClub.UnitTests.Api.AutoMapper
{
	[TestClass]
	public class AutoMapperTests
	{
		[TestMethod]
		public void TestMappingConfiguration()
		{
			try
			{
				var mapper = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<DomainToDtoProfile>();
				}).CreateMapper();

				mapper.ConfigurationProvider.AssertConfigurationIsValid();
			}
			catch (Exception ex)
			{
				Assert.Fail($"Api DomainToDtoProfile configuration does not pass validation.{Environment.NewLine}{ex.Message}");
			}
		}
	}
}
