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

            var myCandy = new Candy("candyYum", "sweetums", FlavorType.hardCandy, DateTime.Now, 1234);
            var myCandy2 = new Candy("sweets", "mars", FlavorType.sour, DateTime.Now, 1235);
            var myCandy3 = new Candy("coolCandy", "pedigree", FlavorType.stretchy, DateTime.Now, 1236);

            var MainOwner = new Owner("Owner1", new List<Candy> { myCandy });
            var Bob = new Owner("Owner2", new List<Candy> { myCandy2 });
            var Daphne = new Owner("Owner3", new List<Candy> { myCandy3 });

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
                    .AddMenuOption("Do you want to eat random flavor candy? Take it here.")
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
                case "3":
                    EatRandomizeCandy(db);
                    break;
                default: return true;
            }
            Console.ReadLine();
            return false;
        }

        internal static void AddNewCandy(CandyStorage db)
        {


            //var savedCandy = db.SaveNewCandy(newCandy);
            // Console.WriteLine($"Now you own the candy {savedCandy.Name}");
        }

        private static void EatCandy(CandyStorage db)
        {
            try
            {
                var EatenCandies = new List<Candy>();
                var candies = new List<Candy>();
                candies.Add(new Candy("snickers", "Hersheys", FlavorType.chocolate, DateTime.Now, 1));
                candies.Add(new Candy("watchmacallit", "Hersheys", FlavorType.chocolate, DateTime.Now, 2));
                candies.Add(new Candy("skittles", "Wrigley", FlavorType.sour, DateTime.Now, 3));
                candies.Add(new Candy("snickers", "Hersheys", FlavorType.chocolate, DateTime.Now, 4));

                var listOfCandies = "";
                foreach (var candy in candies)
                {
                    listOfCandies += $"{candy.Name}" + ",";
                }
                Console.WriteLine($"Please Select Which Candy You want to Eat :{listOfCandies.TrimEnd(',')}");
                var UserSelection = Console.ReadLine().ToLower();

                var FiltercandyByName = candies.Where(candy => candy.Name == UserSelection).ToList();
                FiltercandyByName.Sort((x, y) => (x.CandyId.CompareTo(y.CandyId)));
                var OldCandy = FiltercandyByName.First();
                Console.WriteLine($"You can eat {OldCandy.Name} with id {OldCandy.CandyId}");

                candies.Remove(OldCandy);
                EatenCandies.Add(OldCandy);
                var listOfReamainingCandies = " ";
                var listOfEatenCandies = " ";
                foreach (var candy in EatenCandies)
                {
                    listOfEatenCandies += $"{candy.Name}" + ",";
                }
                Console.WriteLine($"Candy that I can eat: {listOfEatenCandies.TrimEnd(',')} ");
                foreach (var candy in candies)
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

        public static void EatRandomizeCandy(CandyStorage db) {
            try
            {
                var EatenRandomizeCandies = new List<Candy>();
                var candies = new List<Candy>();
                candies.Add(new Candy("snickers", "Hersheys", FlavorType.chocolate, DateTime.Now, 1));
                candies.Add(new Candy("watchmacallit", "Hersheys", FlavorType.chocolate, DateTime.Now, 2));
                candies.Add(new Candy("skittles", "Wrigley", FlavorType.sour, DateTime.Now, 3));
                candies.Add(new Candy("kisses", "Hersheys", FlavorType.chocolate, DateTime.Now, 4));
                candies.Add(new Candy("snickers", "Hersheys", FlavorType.chocolate, DateTime.Now, 5));
                candies.Add(new Candy("sour punch", "American Licorice", FlavorType.sour, DateTime.Now, 6));
                var listOfCandies = "";
                foreach (var candy in candies)
                {
                    listOfCandies += $"{candy.Flavor}" + ",";
                }
                Console.WriteLine($"Please select flavor of Candy that you want to eat :{listOfCandies.TrimEnd(',')}");
                var UserFlavorSelection = Console.ReadLine().ToLower();

                var FilterCandyByFlavor = candies.Where(candy => candy.Flavor.ToString() == UserFlavorSelection).Select(candy => candy).ToList();
                Random random = new Random();
                int randFlavor = random.Next(FilterCandyByFlavor.Count);
                var randSelectedCandy = FilterCandyByFlavor[randFlavor].Name;
                var checkSameCandies = FilterCandyByFlavor.Where(candy => candy.Name == randSelectedCandy).First();
                Console.WriteLine($"Here is the random candy {checkSameCandies.Name} with {checkSameCandies.CandyId} id");
                candies.Remove(checkSameCandies);
                EatenRandomizeCandies.Add(checkSameCandies);
                var listOfReamainingCandies = " ";
                var listOfEatenCandies = " ";
                foreach (var candy in EatenRandomizeCandies)
                {
                    listOfEatenCandies += $"{candy.Name}" + ",";
                }
                Console.WriteLine($"Random flavor candy that I can eat: {listOfEatenCandies.TrimEnd(',')}");

                foreach (var candy in candies)
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