using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyMarket
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = SetupNewApp();

            var myCandy = new Candy("candyYum", "sweetums", FlavorType.HardCandy, DateTime.Now, 1234);
            var myCandy2 = new Candy("sweets", "mars", FlavorType.Sour, DateTime.Now, 1235);
            var myCandy3 = new Candy("coolCandy", "pedigree", FlavorType.Stretchy, DateTime.Now, 1236);
            var myCandy4 = new Candy("coolCandy", "sweetums", FlavorType.HardCandy, DateTime.Now, 1234);
            var myCandy5 = new Candy("sweets", "mars", FlavorType.Sour, DateTime.Now, 1235);
            var myCandy6 = new Candy("candyYum", "pedigree", FlavorType.Stretchy, DateTime.Now, 1236);

            var MainOwner = new Owner("Owner1", new List<Candy> { myCandy, myCandy4 });
            var Bob = new Owner("Owner2", new List<Candy> { myCandy2, myCandy5 });
            var Daphne = new Owner("Owner3", new List<Candy> { myCandy3, myCandy6 });
            //var myTrade = new Trade("Trade1", 1234, 1235, "Owner2");

            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(db, userInput, MainOwner, Bob, Daphne);
            }

            //Console.WriteLine($"{myTrade.TradeId} gives {myTrade.MainOwnerId} " +
                //$"{myTrade.ReceivingCandyId} and gives {myTrade.OtherOwnerId} " +
                //$"{myTrade.TradingCandyId}");

            //foreach (var candy in MainOwner.CandyList)
            //{
              //Console.WriteLine($"{ MainOwner.OwnerId} has {candy.Name} " +
                    //$"that has flavor of {candy.Flavor} that was Manufactured " +
                    //$"at {candy.Manufacture} and received on {candy.RecievedDate}");
            //}

            Console.ReadLine();

        }


        internal static CandyStorage SetupNewApp()
        {
            Console.Title = "Cross Confectioneries Incorporated";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            var db = new CandyStorage();

            return db;
        }


        internal static ConsoleKeyInfo MainMenu()
        {
            View mainMenu = new View()
                    .AddMenuOption("Do you want to add CandyYum?")
                    .AddMenuOption("Do you want to eat some candy? Take it here.")
                    .AddMenuOption("Do you want to trade candy? Trade here.")    
                    .AddMenuText("Press Esc to exit.");
            Console.Write(mainMenu.GetFullMenu());
            var userOption = Console.ReadKey();
            return userOption;
        }

        private static bool TakeActions(CandyStorage db, ConsoleKeyInfo userInput, Owner mainOwner, Owner Bob, Owner Daphne)
        {
            Console.Write(Environment.NewLine);

            if (userInput.Key == ConsoleKey.Escape)
                return true;

            var selection = userInput.KeyChar.ToString();
            switch (selection)
            {
                case "1":
                    AddNewCandy(db);
                    break;
                case "2":
                    EatCandy(mainOwner.CandyList);
                    break;
                case "4":
                    Console.WriteLine("Who do you want to Trade with?");
                    var tradingOwnerName = Console.ReadLine();
                    var tradingOwner = new Owner("default", new List<Candy>());
                        if (tradingOwnerName.ToUpper() == "BOB")
                    {
                        tradingOwner = Bob;
                        StartTrade(mainOwner, tradingOwner);
                    } else if (tradingOwnerName.ToUpper() == "DAPHNE")
                    {
                        tradingOwner = Daphne;
                        StartTrade(mainOwner, tradingOwner);
                    } else
                    {
                        Console.WriteLine("There is no user by that name.");
                    }
                    break;
                default: return false;
            }
            Console.ReadLine();
            return false;
        }

        internal static void AddNewCandy(CandyStorage db)
        {
            

            //var savedCandy = db.SaveNewCandy(newCandy);
           // Console.WriteLine($"Now you own the candy {savedCandy.Name}");
        }

        private static void EatCandy(List<Candy> candies)
        {
            throw new NotImplementedException();
        }

        private static void StartTrade(Owner mainOwner, Owner tradingOwner)
        {
            var tradingOwnerCandyList = "";
            foreach (var candy in tradingOwner.CandyList)
            {
                tradingOwnerCandyList += $"{candy.Name},";
            }
            Console.WriteLine($"What candy do you want to receive? {tradingOwnerCandyList.TrimEnd(',')}");
            var receivingCandyInput = Console.ReadLine();
            var filterCandy = tradingOwner.CandyList.Where(candy => candy.Name == receivingCandyInput).ToList();
            var receivingCandy = filterCandy.First();
            var mainOwnerCandyList = "";
            foreach (var candy in mainOwner.CandyList)
            {
                mainOwnerCandyList += $"{candy.Name},";
            }
            Console.WriteLine($"What candy do you want to trade? {mainOwnerCandyList.TrimEnd(',')}");
            var tradingCandyInput = Console.ReadLine();
            var filterCandies = mainOwner.CandyList.Where(candy => candy.Name == tradingCandyInput).ToList();
            var tradingCandy = filterCandies.First();
            mainOwner.CandyList.Remove(tradingCandy);
            mainOwner.CandyList.Add(receivingCandy);
            tradingOwner.CandyList.Remove(receivingCandy);
            tradingOwner.CandyList.Add(tradingCandy);
            var newTradingOwnerCandyList = "";
            var newMainOwnerCandyList = "";
            foreach (var candy in tradingOwner.CandyList)
            {
                newTradingOwnerCandyList += $"{candy.Name},";
            }
            foreach (var candy in mainOwner.CandyList)
            {
                newMainOwnerCandyList += $"{candy.Name},";
            }
            Console.WriteLine($"You have traded {tradingCandy.Name} for {receivingCandy.Name}");
            Console.WriteLine($"You now have these candies: {newMainOwnerCandyList.TrimEnd(',')}");
            Console.WriteLine($"{tradingOwner.OwnerId} now has these candies: {newTradingOwnerCandyList.TrimEnd(',')}");

        }
    }
    }

