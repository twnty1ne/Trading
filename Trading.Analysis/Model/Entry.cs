using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trady.Analysis;
using Trady.Core.Infrastructure;

namespace Trading.Analysis.Model
{
    internal class Entry : IEntry
    {
        private readonly decimal _slTreshold = 0.01m;
        private readonly decimal _mathExpectation = 2m;

        public Entry(IIndexedOhlcv ic, Position position)
        {
            
            Price = ic.Open;
            StopLoss = CalculateStopLoss();
            TakeProfit = CalculateTakeProfit();
            Date = ic.Next.DateTime;
            State = IsSucces(ic);
            Index = ic.Index;
            Position = position;

        }

        public decimal TakeProfit { get; private set; }
        public int Index { get; private set; }
        public decimal Price { get; private set; }
        public decimal StopLoss { get; private set; }
        public EntryState State { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public Position Position { get; private set; }

        private EntryState IsSucces(IIndexedOhlcv ic) 
        {

            var hitTp = GetTakeProfitCandle(ic);
            var hitSl = GetStopLossCandle(ic);
            if (ShoulbBeSkipped(ic)) return EntryState.Skipped;
            if (hitTp != null && (hitSl is null || hitSl.DateTime > hitTp.DateTime)) return EntryState.HitTakeProfit;
            if (hitSl != null && (hitTp is null || hitSl.DateTime <= hitTp.DateTime)) return EntryState.HitStopLoss;
            return EntryState.InProgress;

        }


        private IOhlcv GetTakeProfitCandle(IIndexedOhlcv ic) 
        {
            var nextCandle = ic.Next;
            var allCandlesAfter = nextCandle.BackingList.Where(x => x.DateTime > nextCandle.DateTime).OrderBy(x => x.DateTime);
            if(Position == Position.Long) return allCandlesAfter.FirstOrDefault(x => x.High >= TakeProfit);
            return allCandlesAfter.FirstOrDefault(x => x.Low <= TakeProfit); ;
        }

        private IOhlcv GetStopLossCandle(IIndexedOhlcv ic)
        {
            var nextCandle = ic.Next;
            var allCandlesAfter = nextCandle.BackingList.Where(x => x.DateTime > nextCandle.DateTime).OrderBy(x => x.DateTime);
            if (Position == Position.Long) return allCandlesAfter.FirstOrDefault(x => x.Low <= StopLoss);
            return allCandlesAfter.FirstOrDefault(x => x.High >= StopLoss);
        }

        private bool ShoulbBeSkipped(IIndexedOhlcv ic)
        {
            if(Position == Position.Long) return StopLoss > ic.Low;
            return StopLoss < ic.High;
        }

        private decimal CalculateStopLoss() 
        {
            if(Position == Position.Short) return Price + (Price * _slTreshold);
            return Price - (Price * _slTreshold);

        }

        private decimal CalculateTakeProfit()
        {
            var riskAbs = Math.Abs(StopLoss - Price);
            if (Position == Position.Short) return Price - _mathExpectation * riskAbs;
            return Price + _mathExpectation * riskAbs;
        }

    }
}
