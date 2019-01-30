using System;

namespace SmileDirectClub.Shared.Settings
{
	public class SpaceXApiSettings
	{
		public string BaseUri { get; set; }
		public string LaunchPads { get; set; }

		public Uri LaunchPadsUri => new Uri(BaseUri + LaunchPads);
	}
}
