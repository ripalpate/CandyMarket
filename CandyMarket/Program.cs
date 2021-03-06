﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyMarket
{
    class Program
    {
        static void Main(string[] args)
        {
            var myCandy = new Candy("snickers", "sweetyum", FlavorType.chocolate, new DateTime(2019, 01, 15, 13, 00, 23, 430), 1234);
            var myCandy2 = new Candy("sour patch", "Allen Candy Company", FlavorType.sour, new DateTime(2019, 01, 20, 15, 10, 30, 530), 1235);
            var myCandy3 = new Candy("taffy", "pedigree", FlavorType.stretchy, new DateTime(2019, 02, 25, 11, 30, 23, 630), 1236);
            var myCandy4 = new Candy("jawbreaker", "sweetums", FlavorType.hardCandy, new DateTime(2019, 03, 07, 17, 00, 30, 730), 1237);
            var myCandy5 = new Candy("milkyway", "hersheys", FlavorType.chocolate, new DateTime(2019, 03, 01, 14, 25, 50, 330), 1238);
            var myCandy6 = new Candy("kisses", "pedigree", FlavorType.chocolate, new DateTime(2019, 02, 28, 18, 20, 23, 730), 1239);

            var EatenCandies = new List<Candy>();
            var MainOwner = new Owner("Owner1", new List<Candy> { myCandy, myCandy2 });
            var Bob = new Owner("bob", new List<Candy> { myCandy3, myCandy4 });
            var Daphne = new Owner("daphne", new List<Candy> { myCandy5,myCandy6 });
            var OwnerList = new List<Owner> { MainOwner, Bob, Daphne };
            var db = SetupNewApp();
            var candyList = new List<Candy> {myCandy, myCandy2, myCandy3, myCandy4, myCandy5, myCandy6};
            var candyCounter = new List<int> {1234, 1235, 1236, 1237, 1238, 1239};
            var tradeList = new List<Trade>();
            var tradeCounter = new List<int>();


            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(userInput, OwnerList, candyList, candyCounter, EatenCandies, tradeList, tradeCounter);
            }
        }


        internal static CandyStorage SetupNewApp()
        {
            Console.Title = "Cross Confectioneries Incorporated";
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;

            var db = new CandyStorage();

            return db;
        }


        internal static ConsoleKeyInfo MainMenu()
        {
            View mainMenu = new View()
                    .AddMenuOption("Did you want to add some candy? Get some here.")
                    .AddMenuOption("Do you want to eat some candy? Take it here.")
                    .AddMenuOption("Do you want to eat random flavor candy? Take it here.")
                    .AddMenuOption("Do you want to trade candy? Trade here.")   
                    .AddMenuOption("Do you want to see past trades? Print them here.")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(mainMenu.GetFullMenu());
            var userOption = Console.ReadKey();
            return userOption;
        }

        private static bool TakeActions(ConsoleKeyInfo userInput, List<Owner> OwnerList, List<Candy> candyList, List<int> candyCounter, List<Candy> EatenCandies, List<Trade> tradeList, List<int> tradeCounter)
        {
            Console.Write(Environment.NewLine);

            if (userInput.Key == ConsoleKey.Escape)
                return true;
            var mainOwner = OwnerList.Where(owner => owner.OwnerId == "Owner1").ToList().First();
            var selection = userInput.KeyChar.ToString();
            switch (selection)
            {
                case "1":
                    Console.WriteLine("What candy do you want to add? Choose from snickers, sour patch, taffy, jawbreaker, milkyway, kisses");
                    var candyPicked = Console.ReadLine();

                    var candyFilter = candyList.Where(candy => candy.Name == candyPicked).ToList().First();
                    AddNewCandy(mainOwner, candyFilter, candyCounter);
                    break;
                case "2":
                    EatCandy(mainOwner, EatenCandies);
                    break;
                case "3":
                    EatRandomizeCandy(mainOwner, EatenCandies);
                    break;
                case "4":
                    Console.WriteLine("Who do you want to Trade with? Bob or Daphne?");
                    var tradingOwnerName = Console.ReadLine();
                    var tradingOwner = OwnerList.Where(owner => owner.OwnerId == tradingOwnerName.ToLower()).ToList().First();
                    StartTrade(mainOwner, tradingOwner, tradeList, tradeCounter);
                    break;
                case "5":
                    printTrade(tradeList);
                    break;
                default: return false;
            }
            Console.ReadLine();
            return false;
        }

        public static void AddNewCandy(Owner MainOwner, Candy myCandy, List<int> candyCounter)
        {
            var dateTime = DateTime.Now;
            var maxId = 0;
            foreach (var Id in candyCounter)
            {
                if (Id > maxId)
                {
                    maxId = Id;
                }
            }

            maxId++;
            candyCounter.Add(maxId);
            var newCandy = new Candy(myCandy.Name, myCandy.Manufacture, myCandy.Flavor, dateTime, maxId);
            MainOwner.CandyList.Add(newCandy);
            Console.WriteLine($"You added:{newCandy.Name} candy which is received on {newCandy.RecievedDate} with {newCandy.CandyId} id");
            foreach (var item in MainOwner.CandyList)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static void EatCandy(Owner mainOwner, List<Candy> EatenCandies)
        {
            try
            {
                //iterates over candies list to display candy options for user
                var listOfCandies = "";
                foreach (var candy in mainOwner.CandyList)
                {
                    listOfCandies += $"{candy.Name}" + ",";
                }
                Console.WriteLine($"Please select which candy you want to eat :{listOfCandies.TrimEnd(',')}");
                var UserSelection = Console.ReadLine().ToLower();

                //filters the candy by name to match same name candy and sort it by old date
                var FiltercandyByName = mainOwner.CandyList.Where(candy => candy.Name == UserSelection).ToList();
                FiltercandyByName.Sort((x, y) => (x.RecievedDate.CompareTo(y.RecievedDate)));
                var OldCandy = FiltercandyByName.First();
                Console.WriteLine($"You can eat {OldCandy.Name} which is received on {OldCandy.RecievedDate}");

                //removes eaten candy from the candies list and adds to eatenCandies list
                mainOwner.CandyList.Remove(OldCandy);
                EatenCandies.Add(OldCandy);
                PrintCandies(mainOwner, EatenCandies);
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private static void StartTrade(Owner mainOwner, Owner tradingOwner, List<Trade> tradeList, List<int> tradeCounter)
        {
            //prints and selects trading owner's candy
            var tradingOwnerCandyList = "";
            foreach (var candy in tradingOwner.CandyList)
            {
                tradingOwnerCandyList += $"{candy.Name},";
            }
            Console.WriteLine($"What candy do you want to receive? {tradingOwnerCandyList.TrimEnd(',')}");
            var receivingCandyInput = Console.ReadLine();
            var filterCandy = tradingOwner.CandyList.Where(candy => candy.Name == receivingCandyInput).ToList();
            var receivingCandy = filterCandy.First();

            //prints and selects main owner's candy
            var mainOwnerCandyList = "";
            foreach (var candy in mainOwner.CandyList)
            {
                mainOwnerCandyList += $"{candy.Name},";
            }
            Console.WriteLine($"What candy do you want to trade? {mainOwnerCandyList.TrimEnd(',')}");
            var tradingCandyInput = Console.ReadLine();
            var filterCandies = mainOwner.CandyList.Where(candy => candy.Name == tradingCandyInput).ToList();
            var tradingCandy = filterCandies.First();

            //find max trade id
            var maxId = 0;
            foreach (var Id in tradeCounter)
            {
                if (Id > maxId)
                {
                    maxId = Id;
                }
            }
            maxId++;
            tradeCounter.Add(maxId);

            //instantiates new trade and addes it to trade list.
            var newTrade = new Trade(maxId, tradingCandy, receivingCandy, tradingOwner, mainOwner);
            tradeList.Add(newTrade);

            //trades candies
            mainOwner.CandyList.Remove(tradingCandy);
            mainOwner.CandyList.Add(receivingCandy);
            tradingOwner.CandyList.Remove(receivingCandy);
            tradingOwner.CandyList.Add(tradingCandy);

            //print results
            tradingOwnerCandyList = "";
            mainOwnerCandyList = "";
            foreach (var candy in tradingOwner.CandyList)
            {
                tradingOwnerCandyList += $"{candy.Name},";
            }
            foreach (var candy in mainOwner.CandyList)
            {
                mainOwnerCandyList += $"{candy.Name},";
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"You have traded {tradingCandy.Name} for {receivingCandy.Name}");
            Console.WriteLine($"You now have these candies: {mainOwnerCandyList.TrimEnd(',')}");
            Console.WriteLine($"{tradingOwner.OwnerId} now has these candies: {tradingOwnerCandyList.TrimEnd(',')}");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public static void printTrade(List<Trade> tradeList)
        {
            foreach (var trade in tradeList)
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine($"TradeId: {trade.TradeId}");
                Console.WriteLine($"You traded your {trade.TradingCandy.Name} for {trade.OtherOwner.OwnerId}'s {trade.ReceivingCandy.Name}");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            }
        }

        public static void EatRandomizeCandy(Owner mainOwner, List<Candy> EatenCandies)
        {
            try
            {
                //iterates over candies list to display candy falvor to users.
                var listOfCandies = "";
                foreach (var candy in mainOwner.CandyList)
                {
                    listOfCandies += $"{candy.Flavor}" + ",";
                }
                Console.WriteLine($"Please select flavor of Candy that you want to eat :{listOfCandies.TrimEnd(',')}");
                var UserFlavorSelection = Console.ReadLine().ToLower();

                //filters candy by flavor and selects that candy
                var FilterCandyByFlavor = mainOwner.CandyList.Where(candy => candy.Flavor.ToString() == UserFlavorSelection).Select(candy => candy).ToList();

                // randomize candy by flavor and filters the candy by name to see if similar candies exists in the list and finds old candy
                Random random = new Random();
                int randFlavor = random.Next(FilterCandyByFlavor.Count);
                var randSelectedCandy = FilterCandyByFlavor[randFlavor].Name;
                var checkSameCandies = FilterCandyByFlavor.Where(candy => candy.Name == randSelectedCandy).First();
                Console.WriteLine($"Here is the random candy {checkSameCandies.Name} which is received on {checkSameCandies.RecievedDate}");

                //Removes eaaten candy from the candies list and adds that candy to EatenCandies list.
                mainOwner.CandyList.Remove(checkSameCandies);
                EatenCandies.Add(checkSameCandies);
                PrintCandies(mainOwner, EatenCandies);
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static bool PrintCandies(Owner mainOwner, List<Candy> EatenCandies)
        {
            //iterates over EatenCandies List to print individual candy
            var listOfReamainingCandies = " ";
            var listOfEatenCandies = " ";
            foreach (var candy in EatenCandies)
            {
                listOfEatenCandies += $"{candy.Name}" + ",";
            }
            Console.WriteLine($"Candy that ate: {listOfEatenCandies.TrimEnd(',')}");

            //iterates over candies list to display remaining candies in the candies list.
            foreach (var candy in mainOwner.CandyList)
            {
                listOfReamainingCandies += $"{candy.Name}" + ",";
            }
            Console.WriteLine($"List of remaining Candies: {listOfReamainingCandies.TrimEnd(',')}");
            return true;
        }

    }
}