using System;
using System.Collections.Generic;
using System.Text;

namespace CandyMarket
{
    class Trade
    {
        public string TradeId { get; set; }
        public string MainOwnerId { get; set; }
        public string OtherOwnerId { get; set; }
        public int TradingCandyId { get; set; }
        public int ReceivingCandyId { get; set; }

        public Trade(string tradeId, int tradingCandyId, int receivingCandyId, string otherOwnerId, string mainOwnerId = "Owner1")
        {
            TradeId = tradeId;
            MainOwnerId = mainOwnerId;
            OtherOwnerId = otherOwnerId;
            TradingCandyId = tradingCandyId;
            ReceivingCandyId = receivingCandyId;
        }
    }
}
