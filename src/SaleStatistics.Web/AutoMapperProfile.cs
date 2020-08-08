using AutoMapper;
using SaleStatistics.Application.Queries.GetStatistics;
using SaleStatistics.Application.Repositories.SaleStatistics;
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

            CreateMap<GetStatisticsQueryResponse, SaleStatisticsViewModel>()
                .ForMember(dest => dest.Statistics, opt => opt.MapFrom(src => src.Statistics));

            CreateMap<GetStatisticsQueryResponseStatistic, SaleStatisticViewModel>()
                .ForMember(dest => dest.Statistics, opt => opt.MapFrom(src => src.Statistics))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DateUpdated))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<GetStatisticsQueryResponseItem, SaleStatisticItemViewModel>()
                .ForMember(dest => dest.Agent, opt => opt.MapFrom(src => src.RealEstateAgent))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.SalesCount));

            CreateMap<SalesStatistic, GetStatisticsQueryResponseStatistic>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripiton))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DateUpdated))
                .ForMember(dest => dest.Statistics, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleStatisticItem, GetStatisticsQueryResponseItem>()
                .ForMember(dest => dest.RealEstateAgent, opt => opt.MapFrom(src => src.RealEstateAgent))
                .ForMember(dest => dest.SalesCount, opt => opt.MapFrom(src => src.SalesCount));
        }
    }
}
