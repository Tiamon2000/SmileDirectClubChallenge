using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmileDirectClub.Shared.DomainModels;
using SmileDirectClub.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmileDirectClub.UnitTests.Shared.DomainModels
{
	[TestClass]
	public class LaunchPadQueryTests
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
		public void ValidateRequest_Valid()
		{
			var query = new LaunchPadQuery();

			query.ValidateRequest();

			query = new LaunchPadQuery() { Id = "1", Name = "Test", Status = "active" };

			query.ValidateRequest();

			// no exceptions thrown
		}

		[TestMethod]
		public void ValidateRequest_NameLessThanThreeCharacters()
		{
			var query = new LaunchPadQuery() { Name = "2c" };

			var ex = Assert.ThrowsException<BadRequestException>(() =>
			{
				query.ValidateRequest();
			});

			Assert.AreEqual(1, ex.Content.Fields.Count);
			Assert.AreEqual("name", ex.Content.Fields.First().Field);
		}

		[TestMethod]
		public void ValidateRequest_StatusCanBeOnlyActiveOrRequired()
		{
			var query = new LaunchPadQuery() { Status = "active" };

			query.ValidateRequest();

			query.Status = "retired";

			query.ValidateRequest();

			query.Status = "invalid";

			var ex = Assert.ThrowsException<BadRequestException>(() =>
			{
				query.ValidateRequest();
			});

			Assert.AreEqual(1, ex.Content.Fields.Count);
			Assert.AreEqual("status", ex.Content.Fields.First().Field);
		}

		[TestMethod]
		public void ApplyFilters_NoFilters()
		{
			var query = new LaunchPadQuery();

			var result = query.ApplyFilters(_launchPads).ToList();

			Assert.AreEqual(2, result.Count);
		}

		[TestMethod]
		public void ApplyFilters_StatusFilter()
		{
			var query = new LaunchPadQuery() { Status = "active" };

			var result = query.ApplyFilters(_launchPads).ToList();

			Assert.AreEqual(1, result.Count);
			Assert.IsTrue(result.All(x => x.Status == "active"));
		}

		[TestMethod]
		public void ApplyFilters_NameFilter()
		{
			var query = new LaunchPadQuery() { Name = "rocket" };

			var result = query.ApplyFilters(_launchPads).ToList();

			Assert.AreEqual(1, result.Count);
			Assert.IsTrue(result.All(x => x.Name.IndexOf("rocket", StringComparison.OrdinalIgnoreCase) >= 0));
		}
	}
}
