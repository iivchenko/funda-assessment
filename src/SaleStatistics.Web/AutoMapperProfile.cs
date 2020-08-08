using AutoMapper;
using SaleStatistics.Application.Queries.GetTopSaleObjects;
using SaleStatistics.Web.Models;

namespace SaleStatistics.Web
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GetTopSaleObjectsQueryResponseItem, SaleStatisticsItem>()
                .ForMember(dest => dest.Agent, opt => opt.MapFrom(src => src.RealEstateAgent))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.SalesCount));
        }
    }
}
