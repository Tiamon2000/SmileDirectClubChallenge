using SmileDirectClub.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SmileDirectClub.Shared.DomainModels
{
    public class LaunchPadQuery
    {
		public string Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }

		public void ValidateRequest()
		{
			// demonstrate BadRequest exception handling
			var badRequest = new BadRequest();

			// constrain the name filter to be at least 3 characters
			if (this.Name != null)
			{
				if (this.Name.Length < 3)
					badRequest.AddField("name", "name must be at least 3 characters");
			}

			// constrain the status filter to one of the allowed values
			if (this.Status != null)
			{
				if (this.Status.ToLowerInvariant() != "active" && this.Status.ToLowerInvariant() != "retired")
					badRequest.AddField("status", "status must be one of: active | retired");
			}

			if (badRequest.Fields.Count > 0)
				throw new BadRequestException(badRequest);
		}

		public IEnumerable<LaunchPad> ApplyFilters(IEnumerable<LaunchPad> launchPads)
		{
			// for a production application with more requests and request filters, I again would probably do something a  more creative
			// maybe add attributes to the properties to specify the type of filter to apply (e.g. exact or contains) 
			// and have a single method that could reflect on the properties to build up the where clause instead of having an ApplyFilters method in each query class
			// something similar could be done for the ValidateRequest method above
			// but since there are only three fields I am just going to hard code the filters

			// Exact match, case sensitive
			if (!String.IsNullOrEmpty(this.Id))
				launchPads = launchPads.Where(x => x.Id == this.Id);

			// Contains, case insensitive
			if (!String.IsNullOrEmpty(this.Name))
				launchPads = launchPads.Where(x => x.Name != null && x.Name.IndexOf(this.Name, StringComparison.OrdinalIgnoreCase) >= 0);

			// Exact match, case insensitive
			if (!String.IsNullOrEmpty(this.Status))
				launchPads = launchPads.Where(x => String.Compare(x.Status, this.Status, true) == 0);

			return launchPads;
		}
	}
}
