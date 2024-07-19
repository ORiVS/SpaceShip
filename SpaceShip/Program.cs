using System;
using System.Collections.Generic;
using ShipFactoryApp.Models.Pieces;
using ShipFactoryApp.Models.Vaisseaux;
using ShipFactoryApp.Managers;

class Program
{
    static void Main(string[] args)
    {
        StockManager stockManager = StockManager.Instance;

        Console.WriteLine("Entrez une commande (ou tapez 'STOCKS' pour afficher les stocks, 'NEEDED_STOCKS ARGS' pour afficher les pièces nécessaires, 'INSTRUCTIONS ARGS' pour afficher les instructions d'assemblage, ou 'QUIT' pour quitter) :");

        string input;
        while ((input = Console.ReadLine()) != "QUIT")
        {
            string[] parts = input.Split(' ');
            string command = parts[0];
            string[] commandArgs = parts.Skip(1).ToArray();

            switch (command.ToUpper())
            {
                case "STOCKS":
                    stockManager.DisplayStock();
                    break;
                case "NEEDED_STOCKS":
                    HandleNeededStocksCommand(commandArgs);
                    break;
                case "INSTRUCTIONS":
                    GenerateInstructions(commandArgs);
                    break;
                case "VERIFY":
                    VerifyOrder(commandArgs);
                    break;
                default:
                    Console.WriteLine("Commande non reconnue.");
                    break;
            }

            Console.WriteLine("Entrez une commande (ou tapez 'STOCKS' pour afficher les stocks, 'NEEDED_STOCKS ARGS' pour afficher les pièces nécessaires, 'INSTRUCTIONS ARGS' pour afficher les instructions d'assemblage, ou 'QUIT' pour quitter) :");
        }
    }

    static void HandleNeededStocksCommand(string[] args)
    {
        Dictionary<string, int> neededPieces = ParseOrder(args);
        StockManager stockManager = StockManager.Instance;
        stockManager.NeededStocks(neededPieces);
    }

    static void GenerateInstructions(string[] args)
    {
        Dictionary<string, int> order = ParseOrder(args);
        StockManager stockManager = StockManager.Instance;
        stockManager.Instructions(order);
    }

    static void VerifyOrder(string[] args)
    {
        Dictionary<string, int> order = ParseOrder(args);
        StockManager stockManager = StockManager.Instance;
        stockManager.Verify(order);
    }

    static Dictionary<string, int> ParseOrder(string[] args)
    {
        var order = new Dictionary<string, int>();
        for (int i = 0; i < args.Length; i += 2)
        {
            string vaisseauType = args[i];
            int quantity = int.Parse(args[i + 1]);
            order[vaisseauType] = quantity;
        }
        return order;
    }
}
