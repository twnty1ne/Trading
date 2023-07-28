﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Trading.Report.DAL;

#nullable disable

namespace Trading.Report.DAL.Migrations
{
    [DbContext(typeof(SessionContext))]
    partial class SessionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Trading.Report.Core.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Instruments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ETHUSDT"
                        },
                        new
                        {
                            Id = 2,
                            Name = "BNBUSDT"
                        },
                        new
                        {
                            Id = 3,
                            Name = "XRPUSDT"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ADAUSDT"
                        },
                        new
                        {
                            Id = 5,
                            Name = "SOLUSDT"
                        },
                        new
                        {
                            Id = 6,
                            Name = "LTCUSDT"
                        },
                        new
                        {
                            Id = 7,
                            Name = "UNIUSDT"
                        },
                        new
                        {
                            Id = 8,
                            Name = "LINKUSDT"
                        },
                        new
                        {
                            Id = 9,
                            Name = "ATOMUSDT"
                        },
                        new
                        {
                            Id = 10,
                            Name = "NEARUSDT"
                        },
                        new
                        {
                            Id = 11,
                            Name = "ETCUSDT"
                        },
                        new
                        {
                            Id = 12,
                            Name = "BTCUSDT"
                        });
                });

            modelBuilder.Entity("Trading.Report.Core.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CloseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EntryDateStringValue")
                        .HasColumnType("text");

                    b.Property<long>("EntryDateTicks")
                        .HasColumnType("bigint");

                    b.Property<decimal>("EntryPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("IMR")
                        .HasColumnType("numeric");

                    b.Property<decimal>("InitialMargin")
                        .HasColumnType("numeric");

                    b.Property<int>("InstrumentId")
                        .HasColumnType("integer");

                    b.Property<int>("Leverage")
                        .HasColumnType("integer");

                    b.Property<decimal>("ROE")
                        .HasColumnType("numeric");

                    b.Property<decimal>("RealizedPnl")
                        .HasColumnType("numeric");

                    b.Property<int>("Side")
                        .HasColumnType("integer");

                    b.Property<decimal>("Size")
                        .HasColumnType("numeric");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<decimal>("StopLoss")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TakeProfit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("UnrealizedPnL")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Trading.Report.Core.PositionPriceTick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("PositionPriceTicks");
                });

            modelBuilder.Entity("Trading.Report.Core.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Trading.Report.Core.Strategy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Strategies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Candle Volume",
                            Type = 1
                        });
                });

            modelBuilder.Entity("Trading.Report.Core.Timeframe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Timeframes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "5m",
                            Type = 5
                        },
                        new
                        {
                            Id = 2,
                            Name = "4H",
                            Type = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "1D",
                            Type = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "1H",
                            Type = 3
                        },
                        new
                        {
                            Id = 5,
                            Name = "1m",
                            Type = 6
                        },
                        new
                        {
                            Id = 6,
                            Name = "30m",
                            Type = 4
                        });
                });

            modelBuilder.Entity("Trading.Report.Core.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int?>("SessionId")
                        .HasColumnType("integer");

                    b.Property<int>("StrategyId")
                        .HasColumnType("integer");

                    b.Property<int>("TimeframeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("SessionId");

                    b.HasIndex("StrategyId");

                    b.HasIndex("TimeframeId");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("Trading.Report.Core.TradeCandle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Close")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Open")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OpenTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TradeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Volume")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TradeId");

                    b.ToTable("TradeCandles");
                });

            modelBuilder.Entity("Trading.Report.Core.Position", b =>
                {
                    b.HasOne("Trading.Report.Core.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instrument");
                });

            modelBuilder.Entity("Trading.Report.Core.PositionPriceTick", b =>
                {
                    b.HasOne("Trading.Report.Core.Position", null)
                        .WithMany("Ticks")
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("Trading.Report.Core.Trade", b =>
                {
                    b.HasOne("Trading.Report.Core.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trading.Report.Core.Session", null)
                        .WithMany("Trades")
                        .HasForeignKey("SessionId");

                    b.HasOne("Trading.Report.Core.Strategy", "Strategy")
                        .WithMany()
                        .HasForeignKey("StrategyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trading.Report.Core.Timeframe", "Timeframe")
                        .WithMany()
                        .HasForeignKey("TimeframeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("Strategy");

                    b.Navigation("Timeframe");
                });

            modelBuilder.Entity("Trading.Report.Core.TradeCandle", b =>
                {
                    b.HasOne("Trading.Report.Core.Trade", "Trade")
                        .WithMany()
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trade");
                });

            modelBuilder.Entity("Trading.Report.Core.Position", b =>
                {
                    b.Navigation("Ticks");
                });

            modelBuilder.Entity("Trading.Report.Core.Session", b =>
                {
                    b.Navigation("Trades");
                });
#pragma warning restore 612, 618
        }
    }
}
