using AutoMapper;

namespace DeliveryApp.Api.Adapters.Http.Contract.src.OpenApi.Mapping;

// C#
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Core.Domain.Model.CourierAggregate.Courier, Contract.OpenApi.Models.Courier>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

        CreateMap<Core.Domain.Model.SharedKernel.Location, Contract.OpenApi.Models.Location>()
            .ForMember(d => d.X, o => o.MapFrom(s => s.X))
            .ForMember(d => d.Y, o => o.MapFrom(s => s.Y));

        CreateMap<Core.Domain.Model.OrderAggegate.Order, Contract.OpenApi.Models.Order>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}
