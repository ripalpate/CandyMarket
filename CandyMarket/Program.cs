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

            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(db, userInput);
            }

            var myCandy = new Candy("candyYum", "sweetums", FlavorType.HardCandy, DateTime.Now , 1234);
            var myCandy2 = new Candy("sweets", "mars", FlavorType.Sour, DateTime.Now, 1235);
            var myCandy3 = new Candy("coolCandy", "pedigree", FlavorType.Stretchy, DateTime.Now, 1236);

            var MainOwner = new Owner("Owner1", new List<Candy> {myCandy});
            var Bob = new Owner("Owner2", new List<Candy> {myCandy2});
            var Daphne = new Owner("Owner3", new List<Candy> {myCandy3});

            foreach (var candy in MainOwner.CandyList)
            {
                Console.WriteLine($"{ MainOwner.OwnerId} has {candy.Name} " +
                    $"that has flavor of {candy.Flavor} that was Manufactured " +
                    $"at {candy.Manufacture} and received on {candy.RecievedDate}");
            }

          //  Console.ReadLine();

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

        private static bool TakeActions(CandyStorage db, ConsoleKeyInfo userInput)
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
                    EatCandy(db);
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

        private static void EatCandy(CandyStorage db)
        {
            var EatenCandies = new List<Candy>();
            var candies = new List<Candy>();
            candies.Add(new Candy("snickers", "Hersheys", FlavorType.Chocolate, DateTime.Now, 1));
            candies.Add(new Candy("watchmacallit", "Hersheys", FlavorType.Chocolate, DateTime.Now, 2));
            candies.Add(new Candy("skittles", "Wrigley", FlavorType.Sour, DateTime.Now, 3));
            candies.Add(new Candy("snickers", "Hersheys", FlavorType.Chocolate, DateTime.Now, 4));

            var listOfCandies = "";
            foreach (var candy in candies)
            {
                listOfCandies += $"{candy.Name}" + ",";
            }
            Console.WriteLine($"Please Select Which Candy You want to Eat :{listOfCandies.TrimEnd(',')}");
            var UserSelection = Console.ReadLine().ToLower();

            var FiltercandyByName = candies.Where(candy => candy.Name == UserSelection).ToList();
            FiltercandyByName.Sort((x,y)=>(x.CandyId.CompareTo(y.CandyId)));
            var OldCandy = FiltercandyByName.First();
            Console.WriteLine($"You can eat {OldCandy.Name} with id {OldCandy.CandyId}");
            
            candies.Remove(OldCandy);
            EatenCandies.Add(OldCandy);
            var listOfReamainingCandies = " ";
            foreach (var candy in candies)
            {
                listOfReamainingCandies += $"{candy.Name}" + ",";
            }
            Console.WriteLine($"newList of Candies after Eat: {listOfReamainingCandies}");
            var listOfEatenCandies = " ";
            foreach (var candy in EatenCandies)
            {
                listOfEatenCandies += $"{candy.Name}" + ",";
            }
            Console.WriteLine($"Eaten Candies: {listOfEatenCandies.TrimEnd(',')}");
            //foreach (var filterCandy in EatenCandies)
            //{
            //    Console.WriteLine($"{filterCandy.Name} with {filterCandy.CandyId}");
            //}
            // throw new NotImplementedException();
           
        }
    }
    }

