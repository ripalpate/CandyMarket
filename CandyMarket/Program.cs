using System;
using System.Collections.Generic;

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

            var MainOwner = new Owner("Owner1", new List<Candy> { myCandy });
            var Bob = new Owner("Owner2", new List<Candy> { myCandy2 });
            var Daphne = new Owner("Owner3", new List<Candy> { myCandy3 });
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
                    .AddMenuOption("Did you just get some new candy? Add it here.")
                    .AddMenuOption("Do you want to eat some candy? Take it here.")
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
                case "3":
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
                default: return true;
            }
            Console.ReadLine();
            return true;
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
            Console.WriteLine("What candy do you want to receive?");
            var receivingCandyName = Console.ReadLine();


            Console.WriteLine("What candy do you want to trade?");
            var tradingCandyName = Console.ReadLine();
        }
    }
    }

