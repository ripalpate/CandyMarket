using System;
using System.Collections.Generic;
using System.Text;

namespace CandyMarket
{
    public class Candy
    {
        public string Name { get; set; }
        public string Manufacture { get; set; }
        public FlavorType Flavor { get; set; }
        public DateTime RecievedDate { get; set; }
        public int CandyId { get; set; }


        public Candy(string name, string manufacture, FlavorType flavor, DateTime recievedDate, int candyId)
        {
            Name = name;
            Manufacture = manufacture;
            Flavor = flavor;
            RecievedDate = recievedDate;
            CandyId = candyId;

        }

    }

    public enum FlavorType
    {
        Chocolate,
        HardCandy,
        Caramel,
        Sour,
        Stretchy,
        Nut

    }
}
