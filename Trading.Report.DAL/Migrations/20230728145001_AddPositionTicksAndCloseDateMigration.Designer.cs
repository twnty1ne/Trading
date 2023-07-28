﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trading.Report.DAL;

namespace Trading.Report.DAL.Migrations
{
    [DbContext(typeof(SessionContext))]
    [Migration("20230728145001_AddPositionTicksAndCloseDateMigration")]
    partial class AddPositionTicksAndCloseDateMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Trading.Report.Core.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

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
                        });
                });

            modelBuilder.Entity("Trading.Report.Core.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CloseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CurrentPrice")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EntryDateStringValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EntryDateTicks")
                        .HasColumnType("bigint");

                    b.Property<decimal>("EntryPrice")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("IMR")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("InitialMargin")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<int>("InstrumentId")
                        .HasColumnType("int");

                    b.Property<int>("Leverage")
                        .HasColumnType("int");

                    b.Property<decimal>("ROE")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("RealizedPnl")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<int>("Side")
                        .HasColumnType("int");

                    b.Property<decimal>("Size")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<decimal>("StopLoss")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("TakeProfit")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("UnrealizedPnL")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

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
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Trading.Report.Core.Strategy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

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
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

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
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<int?>("SessionId")
                        .HasColumnType("int");

                    b.Property<int>("StrategyId")
                        .HasColumnType("int");

                    b.Property<int>("TimeframeId")
                        .HasColumnType("int");

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
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("High")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("Low")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("Open")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime>("OpenTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TradeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Volume")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

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
                        .WithMany("Candles")
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

            modelBuilder.Entity("Trading.Report.Core.Trade", b =>
                {
                    b.Navigation("Candles");
                });
#pragma warning restore 612, 618
        }
    }
}
