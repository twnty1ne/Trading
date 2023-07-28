using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Trading.DAL.Extentions;
using Trading.Report.Core;

namespace Trading.Report.DAL
{
    public class SessionContext : DbContext
    {
        public SessionContext()
        {
            Database.Migrate();
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Timeframe> Timeframes { get; set; }
        public DbSet<Strategy> Strategies { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeCandle> TradeCandles { get; set; }
        public DbSet<PositionPriceTick> PositionPriceTicks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-N8JSEMD;Database=Session;Trusted_Connection=True")
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SetDecimalPrecision(18, 6);
            
            builder
                .Entity<Instrument>()
                .HasData(new List<Instrument>()
                {
                    new Instrument
                    {
                        Id = 1,
                        Name = "ETHUSDT"
                    },
                    new Instrument
                    {
                        Id = 2,
                        Name = "BNBUSDT"
                    },
                    new Instrument
                    {
                        Id = 3,
                        Name = "XRPUSDT"
                    },
                    new Instrument
                    {
                        Id = 4,
                        Name = "ADAUSDT"
                    },
                    new Instrument
                    {
                        Id = 5,
                        Name = "SOLUSDT"
                    },
                    new Instrument
                    {
                        Id = 6,
                        Name = "LTCUSDT"
                    },
                    new Instrument
                    {
                        Id = 7,
                        Name = "UNIUSDT"
                    },
                    new Instrument
                    {
                        Id = 8,
                        Name = "LINKUSDT"
                    },
                    new Instrument
                    {
                        Id = 9,
                        Name = "ATOMUSDT"
                    },
                    new Instrument
                    {
                        Id = 10,
                        Name = "NEARUSDT"
                    },
                    new Instrument
                    {
                        Id = 11,
                        Name = "ETCUSDT"
                    },
                });
            
            builder
                .Entity<Timeframe>()
                .HasData(new List<Timeframe>()
                {
                    new Timeframe
                    {
                        Id = 1,
                        Name = "5m",
                        Type = Exchange.Markets.Core.Instruments.Timeframes.Timeframes.FiveMinutes
                    },
                    new Timeframe
                    {
                        Id = 2,
                        Name = "4H",
                        Type = Exchange.Markets.Core.Instruments.Timeframes.Timeframes.FourHours
                    },
                    new Timeframe
                    {
                        Id = 3,
                        Name = "1D",
                        Type = Exchange.Markets.Core.Instruments.Timeframes.Timeframes.OneDay
                    },
                    new Timeframe
                    {
                        Id = 4,
                        Name = "1H",
                        Type = Exchange.Markets.Core.Instruments.Timeframes.Timeframes.OneHour
                    },
                    new Timeframe
                    {
                        Id = 5,
                        Name = "1m",
                        Type = Exchange.Markets.Core.Instruments.Timeframes.Timeframes.OneMinute
                    },
                    new Timeframe
                    {
                        Id = 6,
                        Name = "30m",
                        Type = Exchange.Markets.Core.Instruments.Timeframes.Timeframes.ThirtyMinutes
                    },
                });

            builder
                .Entity<Strategy>()
                .HasData(new List<Strategy>()
                {
                    new Strategy
                    {
                        Id = 1,
                        Name = "Candle Volume",
                        Type = Bot.Strategies.Strategies.CandleVolume
                    }
                });

            builder
                .Entity<Position>()
                .Property(b => b.EntryDateTicks).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
               .Entity<Position>()
               .Property(b => b.EntryDateStringValue).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
