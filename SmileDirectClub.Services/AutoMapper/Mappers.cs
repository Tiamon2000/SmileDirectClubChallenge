using AutoMapper;
using SmileDirectClub.Services.Dtos;
using SmileDirectClub.Shared.DomainModels;
using System.Collections;
using System.Collections.Generic;

namespace SmileDirectClub.Services.AutoMapper
{
	public static class Mappers
    {
		public static IMapper Mapper = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile<DtoToDomainProfile>();
		}).CreateMapper();

		public static TDestination Map<TDestination>(object source)
		{
			if (source == null)
				return default(TDestination);

			return Mapper.Map<TDestination>(source);
		}

		public static IEnumerable<TDestination> ProjectTo<TDestination>(this IEnumerable source)
		{
			if (source == null)
				return null;

			return Mapper.Map<IEnumerable<TDestination>>(source);
		}

		public static TDestination ProjectTo<TDestination>(this object source)
		{
			return Map<TDestination>(source);
		}

		private class DtoToDomainProfile : Profile
		{
			public DtoToDomainProfile()
			{
				CreateMap<LaunchPadDto, LaunchPad>()
					.ForMember(dest => dest.Id, src => src.MapFrom(x => x.id))
					.ForMember(dest => dest.Name, src => src.MapFrom(x => x.full_name))
					.ForMember(dest => dest.Status, src => src.MapFrom(x => x.status));
			}
		}
    }
}
