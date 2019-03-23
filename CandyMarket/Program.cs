using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyMarket
{
    class Program
    {
        static void Main(string[] args)
        {
            var myCandy = new Candy("candy yum", "sweetums", FlavorType.hardCandy, DateTime.Now, 1234);
            var myCandy2 = new Candy("sweets", "mars", FlavorType.sour, DateTime.Now, 1235);
            var myCandy3 = new Candy("cool candy", "pedigree", FlavorType.stretchy, DateTime.Now, 1236);
            var myCandy4 = new Candy("candy yum", "sweetums", FlavorType.hardCandy, new DateTime(2019, 01, 15, 12, 00, 23, 430), 1237);
            var myCandy5 = new Candy("snickers", "hersheys", FlavorType.chocolate, DateTime.Now, 1238);
            var myCandy6 = new Candy("kisses", "pedigree", FlavorType.chocolate, new DateTime(2018, 09, 15, 12, 00, 23, 430), 1239);
            var EatenCandies = new List<Candy>();
            var MainOwner = new Owner("Owner1", new List<Candy> { myCandy, myCandy2, myCandy3, myCandy4, myCandy5, myCandy6 });
            var Bob = new Owner("Owner2", new List<Candy> { myCandy2 });
            var Daphne = new Owner("Owner3", new List<Candy> { myCandy3 });
            var db = SetupNewApp();
            var candyCounter = new List<int> {1234, 1235, 1236};


            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(db, userInput, MainOwner, myCandy, myCandy2, myCandy3, candyCounter, EatenCandies);
            }


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
                    .AddMenuOption("Did you want to add candyYum? Add it here.")
                    .AddMenuOption("Did you want to add sweets? Add it here.")
                    .AddMenuOption("Did you want to add coolCandy? Add it here.")
                    .AddMenuOption("Do you want to eat some candy? Take it here.")
                    .AddMenuOption("Do you want to eat random flavor candy? Take it here.")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(mainMenu.GetFullMenu());
            var userOption = Console.ReadKey();
            return userOption;
        }

        private static bool TakeActions(CandyStorage db, ConsoleKeyInfo userInput, Owner mainOwner, Candy candyYum, Candy sweets, Candy coolCandy, List<int> candyCounter, List<Candy> EatenCandies)

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
                //var EatenCandies = new List<Candy>();
                //var candies = new List<Candy>();
                //candies.Add(new Candy("snickers", "Hersheys", FlavorType.chocolate, new DateTime(2016, 08, 01, 18, 50, 23, 230), 1));
                //candies.Add(new Candy("watchmacallit", "Hersheys", FlavorType.chocolate, DateTime.Now, 2));
                //candies.Add(new Candy("skittles", "Wrigley", FlavorType.sour, DateTime.Now, 3));
                //candies.Add(new Candy("snickers", "Hersheys", FlavorType.chocolate, new DateTime(2017, 08, 01, 06, 20, 23, 230), 4));

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

        public static void EatRandomizeCandy(Owner mainOwner, List<Candy> EatenCandies) {
            try
            {
                //var EatenRandomizeCandies = new List<Candy>();
                //var candies = new List<Candy>();
                //candies.Add(new Candy("snickers", "Hersheys", FlavorType.chocolate, new DateTime(2018, 08, 01, 18, 50, 23, 230), 1));
                //candies.Add(new Candy("watchmacallit", "Hersheys", FlavorType.chocolate, DateTime.Now, 2));
                //candies.Add(new Candy("skittles", "Wrigley", FlavorType.sour, DateTime.Now, 3));
                //candies.Add(new Candy("kisses", "Hersheys", FlavorType.chocolate, new DateTime(2018, 09, 15, 12, 00, 23, 430), 4));
                //candies.Add(new Candy("snickers", "Hersheys", FlavorType.chocolate, new DateTime(2018, 10, 20, 11, 50, 20, 530), 5));
                //candies.Add(new Candy("sour punch", "American Licorice", FlavorType.sour, new DateTime(2019, 01, 05, 13, 25, 23, 330), 6));
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