using System;
using System.Collections.Generic;
using System.Text;

namespace CandyMarket
{
    class Owner
    {
        public string OwnerId { get; set; }
        public List<Candy> CandyList { get; set; }

        public Owner(string ownerId, List<Candy> candylist)
        {
            OwnerId = ownerId;
            CandyList = candylist;
        }
    }
}
