using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyMarket
{
    class Program
    {
        static void Main(string[] args)
        {
            var myCandy = new Candy("snickers", "sweetyum", FlavorType.chocolate, DateTime.Now, 1234);
            var myCandy2 = new Candy("sour patch", "Allen Candy Company", FlavorType.sour, DateTime.Now, 1235);
            var myCandy3 = new Candy("taffy", "pedigree", FlavorType.stretchy, DateTime.Now, 1236);
            var myCandy4 = new Candy("jawbreaker", "sweetums", FlavorType.hardCandy, DateTime.Now, 1237);
            var myCandy5 = new Candy("Milkyway", "hersheys", FlavorType.chocolate, DateTime.Now, 1238);
            var myCandy6 = new Candy("kisses", "pedigree", FlavorType.chocolate, DateTime.Now, 1239);
            var EatenCandies = new List<Candy>();
            var MainOwner = new Owner("Owner1", new List<Candy> { myCandy, myCandy2 });
            var Bob = new Owner("Owner2", new List<Candy> { myCandy3, myCandy4 });
            var Daphne = new Owner("Owner3", new List<Candy> { myCandy5,myCandy6 });
            var db = SetupNewApp();
            var candyCounter = new List<int> {1234, 1235, 1236};

            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(userInput, MainOwner, myCandy, myCandy2, myCandy3, candyCounter, EatenCandies, Bob, Daphne);
            }
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
                    .AddMenuOption("Did you want to add candyYum? Add it here.")
                    .AddMenuOption("Did you want to add sweets? Add it here.")
                    .AddMenuOption("Did you want to add coolCandy? Add it here.")
                    .AddMenuOption("Do you want to eat some candy? Take it here.")
                    .AddMenuOption("Do you want to eat random flavor candy? Take it here.")
                    .AddMenuOption("Do you want to trade candy? Trade here.")   
                    .AddMenuText("Press Esc to exit.");
            Console.Write(mainMenu.GetFullMenu());
            var userOption = Console.ReadKey();
            return userOption;
        }
  
        private static bool TakeActions(ConsoleKeyInfo userInput, Owner mainOwner, Candy candyYum, Candy sweets, Candy coolCandy, List<int> candyCounter, List<Candy> EatenCandies,  Owner Bob, Owner Daphne)
        {
            Console.Write(Environment.NewLine);

            if (userInput.Key == ConsoleKey.Escape)
                return true;

            var selection = userInput.KeyChar.ToString();
            switch (selection)
            {
                case "1":
                    AddNewCandy(mainOwner, candyYum, candyCounter);
                    break;
                case "2":
                    AddNewCandy(mainOwner, sweets, candyCounter);
                    break;
                case "3":
                    AddNewCandy(mainOwner, coolCandy, candyCounter);
                    break;
                case "4":
                    EatCandy(mainOwner, EatenCandies);
                    break;
                case "5":
                    EatRandomizeCandy(mainOwner, EatenCandies);
                    break;
                case "6":
                    Console.WriteLine("Who do you want to Trade with?");
                    var tradingOwnerName = Console.ReadLine();
                    var tradingOwner = new Owner("default", new List<Candy>());
                    if (tradingOwnerName.ToUpper() == "BOB")
                    {
                        tradingOwner = Bob;
                        StartTrade(mainOwner, tradingOwner);
                    }
                    else if (tradingOwnerName.ToUpper() == "DAPHNE")
                    {
                        tradingOwner = Daphne;
                        StartTrade(mainOwner, tradingOwner);
                    }
                    else
                    {
                        Console.WriteLine("There is no user by that name.");
                    }
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
            myCandy.RecievedDate = dateTime;
            myCandy.CandyId = maxId;
            candyCounter.Add(maxId);
            MainOwner.CandyList.Add(myCandy);
            Console.WriteLine($"{myCandy.CandyId}, {myCandy.Name}, {myCandy.RecievedDate}");
            foreach (var item in MainOwner.CandyList)
            {
                Console.WriteLine(item.Name);
            }
       
        }

        private static void EatCandy(Owner mainOwner, List<Candy> EatenCandies)
        {
            try
            {

                var listOfCandies = "";
                foreach (var candy in mainOwner.CandyList)
                {
                    listOfCandies += $"{candy.Name}" + ",";
                }
                Console.WriteLine($"Please Select Which Candy You want to Eat :{listOfCandies.TrimEnd(',')}");
                var UserSelection = Console.ReadLine().ToLower();

                var FiltercandyByName = mainOwner.CandyList.Where(candy => candy.Name == UserSelection).ToList();
                FiltercandyByName.Sort((x, y) => (x.RecievedDate.CompareTo(y.RecievedDate)));
                var OldCandy = FiltercandyByName.First();
                Console.WriteLine($"You can eat {OldCandy.Name} which is received on {OldCandy.RecievedDate}");

                mainOwner.CandyList.Remove(OldCandy);
                EatenCandies.Add(OldCandy);
                var listOfReamainingCandies = " ";
                var listOfEatenCandies = " ";
                foreach (var candy in EatenCandies)
                {
                    listOfEatenCandies += $"{candy.Name}" + ",";
                }
                Console.WriteLine($"Candy that I ate: {listOfEatenCandies.TrimEnd(',')} ");
                foreach (var candy in mainOwner.CandyList)
                {
                    listOfReamainingCandies += $"{candy.Name}" + ",";
                }
                Console.WriteLine($"Remaining list of candies after I ate: {listOfReamainingCandies.TrimEnd(',')}");
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e);
                throw;
            }

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

        public static void EatRandomizeCandy(Owner mainOwner, List<Candy> EatenCandies) {
            try
            {
                var listOfCandies = "";
                foreach (var candy in mainOwner.CandyList)
                {
                    listOfCandies += $"{candy.Flavor}" + ",";
                }
                Console.WriteLine($"Please select flavor of Candy that you want to eat :{listOfCandies.TrimEnd(',')}");
                var UserFlavorSelection = Console.ReadLine().ToLower();

                var FilterCandyByFlavor = mainOwner.CandyList.Where(candy => candy.Flavor.ToString() == UserFlavorSelection).Select(candy => candy).ToList();
                Random random = new Random();
                int randFlavor = random.Next(FilterCandyByFlavor.Count);
                var randSelectedCandy = FilterCandyByFlavor[randFlavor].Name;
                var checkSameCandies = FilterCandyByFlavor.Where(candy => candy.Name == randSelectedCandy).First();
                Console.WriteLine($"Here is the random candy {checkSameCandies.Name} which is received on {checkSameCandies.RecievedDate}");
                mainOwner.CandyList.Remove(checkSameCandies);
                EatenCandies.Add(checkSameCandies);
                var listOfReamainingCandies = " ";
                var listOfEatenCandies = " ";
                foreach (var candy in EatenCandies)
                {
                    listOfEatenCandies += $"{candy.Name}" + ",";
                }
                Console.WriteLine($"Random flavor candy that I can eat: {listOfEatenCandies.TrimEnd(',')}");

                foreach (var candy in mainOwner.CandyList)
                {
                    listOfReamainingCandies += $"{candy.Name}" + ",";
                }
                Console.WriteLine($"List of remaining Candies: {listOfReamainingCandies.TrimEnd(',')}");
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}