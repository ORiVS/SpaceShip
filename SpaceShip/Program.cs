using System;
using System.Collections.Generic;
using System.IO;
using ShipFactoryApp.Managers;
using ShipFactoryApp.Utilities;

class Program
{
    static void Main(string[] args)
    {
        StockManager stockManager = StockManager.Instance;

        Console.WriteLine("Entrez une commande (ou tapez 'STOCKS' pour afficher les stocks, 'NEEDED_STOCKS ARGS' pour afficher les pièces nécessaires, 'INSTRUCTIONS ARGS' pour afficher les instructions d'assemblage, 'VERIFY ARGS' pour vérifier les stocks, 'LOAD FILENAME' pour charger un fichier, ou 'QUIT' pour quitter) :");

        string input;
        while ((input = Console.ReadLine()) != "QUIT")
        {
            string[] parts = input.Split(' ', 2);
            string command = parts[0];
            string commandArgs = parts.Length > 1 ? parts[1] : null;

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
                case "LOAD":
                    LoadFile(commandArgs);
                    break;
                default:
                    Console.WriteLine("Commande non reconnue.");
                    break;
            }

            Console.WriteLine("Entrez une commande (ou tapez 'STOCKS' pour afficher les stocks, 'NEEDED_STOCKS ARGS' pour afficher les pièces nécessaires, 'INSTRUCTIONS ARGS' pour afficher les instructions d'assemblage, 'VERIFY ARGS' pour vérifier les stocks, 'LOAD FILENAME' pour charger un fichier, ou 'QUIT' pour quitter) :");
        }
    }

    static void HandleNeededStocksCommand(string args)
    {
        Dictionary<string, int> neededPieces = ParseOrder(args);
        StockManager stockManager = StockManager.Instance;
        stockManager.NeededStocks(neededPieces);
    }

    static void GenerateInstructions(string args)
    {
        Dictionary<string, int> order = ParseOrder(args);
        StockManager stockManager = StockManager.Instance;
        stockManager.Instructions(order);
    }

    static void VerifyOrder(string args)
    {
        Dictionary<string, int> order = ParseOrder(args);
        StockManager stockManager = StockManager.Instance;
        stockManager.Verify(order);
    }

    static void LoadFile(string filename)
    {
        string extension = Path.GetExtension(filename).ToLower();

        try
        {
            switch (extension)
            {
                case ".txt":
                    FileLoader.LoadFromTextFile(filename);
                    break;
                case ".json":
                    JsonLoader.LoadFromJsonFile(filename);
                    break;
                case ".xml":
                    XmlLoader.LoadFromXmlFile(filename);
                    break;
                default:
                    Console.WriteLine("Format de fichier non supporté.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du chargement du fichier : {ex.Message}");
        }
    }

    static Dictionary<string, int> ParseOrder(string args)
    {
        var order = new Dictionary<string, int>();
        string[] items = args.Split(' ');

        for (int i = 0; i < items.Length; i += 2)
        {
            if (i + 1 < items.Length)
            {
                string vaisseauType = items[i];
                int quantity = int.Parse(items[i + 1]);
                order[vaisseauType] = quantity;
            }
        }

        return order;
    }
}
