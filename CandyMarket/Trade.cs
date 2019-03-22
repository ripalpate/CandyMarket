using System;
using System.Collections.Generic;
using System.Text;

namespace CandyMarket
{
    class Trade
    {
        public string TradeId { get; set; }
        public Owner MainOwner { get; set; }
        public Owner OtherOwner { get; set; }
        public Candy TradingCandy { get; set; }
        public Candy ReceivingCandy { get; set; }

        public Trade(string tradeId, Candy tradingCandy, Candy receivingCandy, Owner otherOwner, Owner mainOwner)
        {
            TradeId = tradeId;
            MainOwner = mainOwner;
            OtherOwner = otherOwner;
            TradingCandy = tradingCandy;
            ReceivingCandy = receivingCandy;
        }

    }
}
