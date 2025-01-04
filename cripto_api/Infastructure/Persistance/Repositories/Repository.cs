using Application.Dtos;
using Application.Mapping;
using Application.Mappings;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZL.DDD.Base;

namespace Infastructure.Persistance.Repositories;
public class Repository : IRepository
{
    IMapperFacade mapper;
    ApplicationDbContext _context { get; set; }
    public Repository(ApplicationDbContext context, IMapperFacade mapper)
    {
        _context = context;
        this.mapper = mapper;
    }


    Crypto _select(Crypto x)
    {
        return new Crypto
        {
            Name = x.Name,
            Image = x.Image,
            market_cap = x.market_cap,
            ath_change_percentage = x.ath_change_percentage,
            ath = x.ath,
            ath_date = x.ath_date,
            atl = x.atl,
            atl_change_percentage = x.atl_change_percentage,
            atl_date = x.atl_date,
            circulating_supply = x.circulating_supply,
            max_supply = x.max_supply,
            Price = x.Price,
            Symbol = x.Symbol,
            total_supply = x.total_supply,
            Tickers = x.Tickers.Select(t =>

                new Ticker
                {
                    High24H = t.High24H,
                    Low24H = t.Low24H,
                    Created = t.Created,
                    TimeStamp = t.TimeStamp,
                    Candle = t.Candle,
                }).ToList()
        };
    }

    public async Task<IEnumerable<Crypto>> GetTickers(int criptoId, DateTime dateTime)
    {
        var entities = await _context.Cryptos.Include(x => x.Tickers.Where(t => t.Created >= dateTime)).Where(x => x.Id == criptoId).Select(x =>
        _select(x)
         ).ToListAsync();
        return entities;
    }
    public async Task<IEnumerable<Crypto>> GetCriptosWithTickers(DateTime dateTime)
    {
        var entities = await _context.Cryptos.Include(x => x.Tickers.Where(t => t.Created >= dateTime)).Select(x =>
          new Crypto
          {
              Name = x.Name,
              Image = x.Image,
              market_cap = x.market_cap,
              ath_change_percentage = x.ath_change_percentage,
              ath = x.ath,
              ath_date = x.ath_date,
              atl = x.atl,
              atl_change_percentage = x.atl_change_percentage,
              atl_date = x.atl_date,
              circulating_supply = x.circulating_supply,
              max_supply = x.max_supply,
              Price = x.Price,
              Symbol = x.Symbol,
              total_supply = x.total_supply,
              Tickers = x.Tickers.Select(t =>

                  new Ticker
                  {
                      High24H = t.High24H,
                      Low24H = t.Low24H,
                      Created = t.Created,
                      TimeStamp = t.TimeStamp,
                      Candle = t.Candle,
                  }).ToList()
          }
          ).ToListAsync();
        return entities;
    }

    public async Task<Crypto> GetCryptoWithTickers(int criptoId, DateTime dateTime)
    {
        var entity = await _context.Cryptos.Include(x => x.Tickers.Where(t => t.Created >= dateTime)).FirstOrDefaultAsync(x => x.Id == criptoId);
        if (entity == null) throw new Exception("bu id de bir cripto bulunamadı");
        return _select(entity);
    }
}
