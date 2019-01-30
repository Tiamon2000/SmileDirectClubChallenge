using AutoMapper;
using SmileDirectClub.Api.Dtos;
using SmileDirectClub.Shared.DomainModels;

namespace SmileDirectClub.Api.AutoMapper
{
	public class DomainToDtoProfile : Profile
    {
		public DomainToDtoProfile()
		{
			CreateMap<LaunchPad, LaunchPadDto>();
		}
	}
}
