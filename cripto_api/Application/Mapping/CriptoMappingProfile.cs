using Application.Dtos;
using Application.Services.InternalServices.CryptoHttpRequestService;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class CriptoMappingProfile : Profile
    {
        public CriptoMappingProfile()
        {


            CreateMap<CryptoData, Ticker>()
                .ForMember(x => x.Id, o => o.MapFrom(y => 0))
                .ForMember(x => x.Price, o => o.MapFrom(y => y.current_price))
                .ForMember(x => x.TimeStamp, o => o.MapFrom(y => new DateTimeOffset(y.last_updated).ToUnixTimeSeconds()))
                 .ForMember(x => x.High24H, o => o.MapFrom(y => y.high_24h))
                 .ForMember(x => x.Low24H, o => o.MapFrom(y => y.low_24h))
                .ReverseMap();

            CreateMap<CryptoData, Crypto>()
                    .ForMember(x => x.Id, o => o.MapFrom(y => 0))
                .ForMember(x => x.atl, o => o.MapFrom(y => y.atl))
                .ForMember(x => x.Price, o => o.MapFrom(x => x.current_price))
                .ForMember(x => x.Image, o => o.MapFrom(x => x.image))
                .ForMember(x => x.Name, o => o.MapFrom(x => x.name))
                .ReverseMap();


            CreateMap<Crypto, CryptoDto>().ReverseMap();
            CreateMap<Crypto, CryptoDto>()
            .ForMember(dest => dest.tickers, opt => opt.MapFrom(src => src.Tickers)) // Cascade Mapping
            .ReverseMap();

            CreateMap<Ticker, TickerDto>().ReverseMap();


        }
    }
}
