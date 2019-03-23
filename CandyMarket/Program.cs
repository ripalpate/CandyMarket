using System;
using System.Collections.Generic;

namespace CandyMarket
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = SetupNewApp();

            var candyCounter = new List<int> {1234, 1235, 1236};

            var myCandy = new Candy("candyYum", "sweetums", FlavorType.HardCandy, DateTime.Now , 1234);
            var myCandy2 = new Candy("sweets", "mars", FlavorType.Sour, DateTime.Now, 1235);
            var myCandy3 = new Candy("coolCandy", "pedigree", FlavorType.Stretchy, DateTime.Now, 1236);

            var MainOwner = new Owner("Owner1", new List<Candy> {myCandy});
            var Bob = new Owner("Owner2", new List<Candy> {myCandy2});
            var Daphne = new Owner("Owner3", new List<Candy> {myCandy3});

            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(db, userInput, MainOwner, myCandy, myCandy2, myCandy3, candyCounter);
            }


            foreach (var candy in MainOwner.CandyList)
            {
                Console.WriteLine($"{ MainOwner.OwnerId} has {candy.Name} " +
                    $"that has flavor of {candy.Flavor} that was Manufactured " +
                    $"at {candy.Manufacture} and received on {candy.RecievedDate}");
            }

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
                    .AddMenuOption("Did you want to add candyYum? Add it here.")
                    .AddMenuOption("Did you want to add sweets? Add it here.")
                    .AddMenuOption("Did you want to add coolCandy? Add it here.")
                    .AddMenuOption("Do you want to eat some candy? Take it here.")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(mainMenu.GetFullMenu());
            var userOption = Console.ReadKey();
            return userOption;
        }

        private static bool TakeActions(CandyStorage db, ConsoleKeyInfo userInput, Owner MainOwner, Candy candyYum, Candy sweets, Candy coolCandy, List<int> candyCounter)
        {
            Console.Write(Environment.NewLine);

            if (userInput.Key == ConsoleKey.Escape)
                return true;

            var selection = userInput.KeyChar.ToString();
            switch (selection)
            {
                case "1":
                    AddNewCandy(MainOwner, candyYum, candyCounter);
                    break;
                case "2":
                    AddNewCandy(MainOwner, sweets, candyCounter);
                    break;
                case "3":
                    AddNewCandy(MainOwner, coolCandy, candyCounter);
                    break;
                case "4":
                    EatCandy(db);
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

        private static void EatCandy(CandyStorage db)
        {
            throw new NotImplementedException();
        }
    }
    }

