using AutoMapper;
using SaleStatistics.Application.Queries.GetTopSaleObjects;
using SaleStatistics.Application.Services.Sales;
using SaleStatistics.Infrastructure.Services.Sales;
using SaleStatistics.Web.Models;

namespace SaleStatistics.Web
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FundaObject, Sale>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.AgentName));

            CreateMap<GetTopSaleObjectsQueryResponseItem, SaleStatisticsItem>()
                .ForMember(dest => dest.Agent, opt => opt.MapFrom(src => src.RealEstateAgent))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.SalesCount));
        }
    }
}
