using AutoMapper;
using DeliveryApp.Core.Application.UseCases.Queries.GetNotCompletedOrdersQuery;

namespace DeliveryApp.Api.Adapters.Http.Contract.src.OpenApi.Mapping;

// C#
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Core.Application.UseCases.Queries.GetAllCouriers.GetAllCouriersQueryDto, Contract.OpenApi.Models.Courier>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CourierId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

        CreateMap<Core.Domain.Model.SharedKernel.Location, Contract.OpenApi.Models.Location>()
            .ForMember(d => d.X, o => o.MapFrom(s => s.X))
            .ForMember(d => d.Y, o => o.MapFrom(s => s.Y));

        CreateMap<GetNotCompletedOrdersQueryDto, Contract.OpenApi.Models.Order>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}
