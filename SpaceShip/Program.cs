using ShipFactoryApp.Models;
using System;
using System.Collections.Generic;

namespace ShipFactoryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StockManager stockManager = new StockManager();

            stockManager.AddPieceToStock("Hull_HE1", 5);
            stockManager.AddPieceToStock("Engine_EE1", 5);
            stockManager.AddPieceToStock("Wings_WE1", 5);
            stockManager.AddPieceToStock("Thruster_TE1", 10);

            stockManager.AddPieceToStock("Hull_HS1", 5);
            stockManager.AddPieceToStock("Engine_ES1", 5);
            stockManager.AddPieceToStock("Wings_WS1", 5);
            stockManager.AddPieceToStock("Thruster_TS1", 10);

            stockManager.AddPieceToStock("Hull_HC1", 5);
            stockManager.AddPieceToStock("Engine_EC1", 5);
            stockManager.AddPieceToStock("Wings_WC1", 5);
            stockManager.AddPieceToStock("Thruster_TC1", 5);

            string input;
            do
            {
                Console.WriteLine("Entrez une commande (ou tapez 'STOCKS' pour afficher les stocks, 'NEEDED_STOCKS ARGS' pour afficher les pièces nécessaires, 'INSTRUCTIONS ARGS' pour afficher les instructions d'assemblage, 'VERIFY ARGS' pour vérifier une commande, 'PRODUCE ARGS' pour produire un vaisseau, ou 'QUIT' pour quitter) :");
                input = Console.ReadLine();

                if (input == "STOCKS")
                {
                    stockManager.DisplayStock();
                }
                else if (input.StartsWith("NEEDED_STOCKS "))
                {
                    string[] parts = input.Split(' ');
                    if (parts.Length >= 2)
                    {
                        Dictionary<string, int> vaisseaux = new Dictionary<string, int>();
                        for (int i = 1; i < parts.Length; i += 2)
                        {
                            string type = parts[i];
                            int quantity = int.Parse(parts[i + 1]);
                            if (vaisseaux.ContainsKey(type))
                            {
                                vaisseaux[type] += quantity;
                            }
                            else
                            {
                                vaisseaux[type] = quantity;
                            }
                        }
                        stockManager.NeededStocks(vaisseaux);
                    }
                    else
                    {
                        Console.WriteLine("Commande incorrecte. Utilisez: NEEDED_STOCKS [type quantité] ...");
                    }
                }
                else if (input.StartsWith("INSTRUCTIONS "))
                {
                    string[] parts = input.Split(' ');
                    if (parts.Length >= 2)
                    {
                        Dictionary<string, int> vaisseaux = new Dictionary<string, int>();
                        for (int i = 1; i < parts.Length; i += 2)
                        {
                            string type = parts[i];
                            int quantity = int.Parse(parts[i + 1]);
                            if (vaisseaux.ContainsKey(type))
                            {
                                vaisseaux[type] += quantity;
                            }
                            else
                            {
                                vaisseaux[type] = quantity;
                            }
                        }
                        stockManager.GenerateInstructions(vaisseaux);
                    }
                    else
                    {
                        Console.WriteLine("Commande incorrecte. Utilisez: INSTRUCTIONS [type quantité] ...");
                    }
                }
                else if (input.StartsWith("VERIFY "))
                {
                    stockManager.VerifyCommand(input);
                }
                else if (input.StartsWith("PRODUCE "))
                {
                    string[] parts = input.Split(' ');
                    if (parts.Length == 2)
                    {
                        stockManager.ProduceSpaceship(parts[1]);
                    }
                    else
                    {
                        Console.WriteLine("Commande incorrecte. Utilisez: PRODUCE [type]");
                    }
                }
                else if (input != "QUIT")
                {
                    Console.WriteLine("Commande non reconnue.");
                }

            } while (input != "QUIT");

            Console.WriteLine("Programme terminé.");
        }
    }
}
