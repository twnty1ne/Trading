namespace Trading.Exchange.Markets.Core.Instruments.Positions;

public enum PositionResult
{
    Unspecified = 1,
    HitAllTakeProfits = 2,
    HitTakeProfitsThenStopLoss = 3,
    HitStopLoss = 4
}