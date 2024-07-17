using System;
using System.Collections.Generic;

namespace ShipFactoryApp.Models
{
	public class StockManager
	{
		private Dictionary<string, int> stock = new Dictionary<string, int>();

		public void AddPieceToStock(string pieceName, int quantity)
		{
			if (stock.ContainsKey(pieceName))
			{
				stock[pieceName] += quantity;
			}
			else
			{
				stock[pieceName] = quantity;
			}
		}

		public void DisplayStock()
		{
			Console.WriteLine("Inventaire des pièces disponibles :");
			foreach (var item in stock)
			{
				Console.WriteLine($"{item.Key}: {item.Value}");
			}
		}

		public void NeededStocks(Dictionary<string, int> vaisseaux)
		{
			Dictionary<string, int> totalNeeded = new Dictionary<string, int>();

			foreach (var vaisseau in vaisseaux)
			{
				Console.WriteLine($"{vaisseau.Value} {vaisseau.Key} :");

				switch (vaisseau.Key.ToUpper())
				{
					case "EXPLORER":
						PrintNeededPieces("Hull_HE1", 1 * vaisseau.Value, totalNeeded);
						PrintNeededPieces("Engine_EE1", 1 * vaisseau.Value, totalNeeded);
						PrintNeededPieces("Wings_WE1", 1 * vaisseau.Value, totalNeeded);
						PrintNeededPieces("Thruster_TE1", 2 * vaisseau.Value, totalNeeded);
						break;
					case "SPEEDER":
						PrintNeededPieces("Hull_HS1", 1 * vaisseau.Value, totalNeeded);
						PrintNeededPieces("Engine_ES1", 1 * vaisseau.Value, totalNeeded);
						PrintNeededPieces("Wings_WS1", 1 * vaisseau.Value, totalNeeded);
						PrintNeededPieces("Thruster_TS1", 2 * vaisseau.Value, totalNeeded);
						break;
					case "CARGO":
						PrintNeededPieces("Hull_HC1", 1 * vaisseau.Value, totalNeeded);
						PrintNeededPieces("Engine_EC1", 1 * vaisseau.Value, totalNeeded);
						PrintNeededPieces("Wings_WC1", 1 * vaisseau.Value, totalNeeded);
						PrintNeededPieces("Thruster_TC1", 1 * vaisseau.Value, totalNeeded);
						break;
					default:
						Console.WriteLine("Type de vaisseau inconnu.");
						break;
				}
			}

			Console.WriteLine("\nTotal :");
			foreach (var item in totalNeeded)
			{
				Console.WriteLine($"{item.Value} {item.Key}");
			}
		}

		private void PrintNeededPieces(string pieceName, int quantity, Dictionary<string, int> totalNeeded)
		{
			Console.WriteLine($"{quantity} {pieceName}");

			if (totalNeeded.ContainsKey(pieceName))
			{
				totalNeeded[pieceName] += quantity;
			}
			else
			{
				totalNeeded[pieceName] = quantity;
			}
		}

		public void Instructions()
		{
			Console.WriteLine("Instructions disponibles :");
			Console.WriteLine("STOCKS - Afficher les stocks");
			Console.WriteLine("NEEDED_STOCKS ARGS - Afficher les pièces nécessaires pour chaque type de vaisseau");
			Console.WriteLine("VERIFY [type] - Vérifier si un vaisseau du type spécifié peut être produit");
			Console.WriteLine("PRODUCE [type] - Produire un vaisseau du type spécifié si le stock est suffisant");
			Console.WriteLine("INSTRUCTIONS ARGS - Générer les instructions d'assemblage pour une commande");
			Console.WriteLine("QUIT - Quitter le programme");
		}

		public void Verify(string type)
		{
			bool canProduce = false;

			switch (type)
			{
				case "EXPLORER":
					canProduce = stock.ContainsKey("Hull_HE1") && stock["Hull_HE1"] >= 1 &&
								 stock.ContainsKey("Engine_EE1") && stock["Engine_EE1"] >= 1 &&
								 stock.ContainsKey("Wings_WE1") && stock["Wings_WE1"] >= 1 &&
								 stock.ContainsKey("Thruster_TE1") && stock["Thruster_TE1"] >= 2;
					break;
				case "SPEEDER":
					canProduce = stock.ContainsKey("Hull_HS1") && stock["Hull_HS1"] >= 1 &&
								 stock.ContainsKey("Engine_ES1") && stock["Engine_ES1"] >= 1 &&
								 stock.ContainsKey("Wings_WS1") && stock["Wings_WS1"] >= 1 &&
								 stock.ContainsKey("Thruster_TS1") && stock["Thruster_TS1"] >= 2;
					break;
				case "CARGO":
					canProduce = stock.ContainsKey("Hull_HC1") && stock["Hull_HC1"] >= 1 &&
								 stock.ContainsKey("Engine_EC1") && stock["Engine_EC1"] >= 1 &&
								 stock.ContainsKey("Wings_WC1") && stock["Wings_WC1"] >= 1 &&
								 stock.ContainsKey("Thruster_TC1") && stock["Thruster_TC1"] >= 1;
					break;
				default:
					Console.WriteLine("Type de vaisseau inconnu.");
					return;
			}

			if (canProduce)
			{
				Console.WriteLine("AVAILABLE");
			}
			else
			{
				Console.WriteLine("UNAVAILABLE");
			}
		}

		public void ProduceSpaceship(string type)
		{
			if (VerifyProduction(type))
			{
				switch (type)
				{
					case "EXPLORER":
						stock["Hull_HE1"] -= 1;
						stock["Engine_EE1"] -= 1;
						stock["Wings_WE1"] -= 1;
						stock["Thruster_TE1"] -= 2;
						break;
					case "SPEEDER":
						stock["Hull_HS1"] -= 1;
						stock["Engine_ES1"] -= 1;
						stock["Wings_WS1"] -= 1;
						stock["Thruster_TS1"] -= 2;
						break;
					case "CARGO":
						stock["Hull_HC1"] -= 1;
						stock["Engine_EC1"] -= 1;
						stock["Wings_WC1"] -= 1;
						stock["Thruster_TC1"] -= 1;
						break;
					default:
						Console.WriteLine("Type de vaisseau inconnu.");
						return;
				}

				Console.WriteLine($"Un vaisseau de type {type} a été produit.");
			}
			else
			{
				Console.WriteLine($"Un vaisseau de type {type} ne peut pas être produit en raison de pièces manquantes.");
			}
		}

		private bool VerifyProduction(string type)
		{
			switch (type)
			{
				case "EXPLORER":
					return stock.ContainsKey("Hull_HE1") && stock["Hull_HE1"] >= 1 &&
						   stock.ContainsKey("Engine_EE1") && stock["Engine_EE1"] >= 1 &&
						   stock.ContainsKey("Wings_WE1") && stock["Wings_WE1"] >= 1 &&
						   stock.ContainsKey("Thruster_TE1") && stock["Thruster_TE1"] >= 2;
				case "SPEEDER":
					return stock.ContainsKey("Hull_HS1") && stock["Hull_HS1"] >= 1 &&
						   stock.ContainsKey("Engine_ES1") && stock["Engine_ES1"] >= 1 &&
						   stock.ContainsKey("Wings_WS1") && stock["Wings_WS1"] >= 1 &&
						   stock.ContainsKey("Thruster_TS1") && stock["Thruster_TS1"] >= 2;
				case "CARGO":
					return stock.ContainsKey("Hull_HC1") && stock["Hull_HC1"] >= 1 &&
						   stock.ContainsKey("Engine_EC1") && stock["Engine_EC1"] >= 1 &&
						   stock.ContainsKey("Wings_WC1") && stock["Wings_WC1"] >= 1 &&
						   stock.ContainsKey("Thruster_TC1") && stock["Thruster_TC1"] >= 1;
				default:
					return false;
			}
		}

		public void GenerateInstructions(Dictionary<string, int> vaisseaux)
		{
			foreach (var vaisseau in vaisseaux)
			{
				for (int i = 0; i < vaisseau.Value; i++)
				{
					Console.WriteLine($"PRODUCING {vaisseau.Key}");

					switch (vaisseau.Key.ToUpper())
					{
						case "EXPLORER":
							Console.WriteLine("GET_OUT_STOCK 1 Hull_HE1");
							Console.WriteLine("GET_OUT_STOCK 1 Engine_EE1");
							Console.WriteLine("GET_OUT_STOCK 1 Wings_WE1");
							Console.WriteLine("GET_OUT_STOCK 2 Thruster_TE1");
							break;
						case "SPEEDER":
							Console.WriteLine("GET_OUT_STOCK 1 Hull_HS1");
							Console.WriteLine("GET_OUT_STOCK 1 Engine_ES1");
							Console.WriteLine("GET_OUT_STOCK 1 Wings_WS1");
							Console.WriteLine("GET_OUT_STOCK 2 Thruster_TS1");
							break;
						case "CARGO":
							Console.WriteLine("GET_OUT_STOCK 1 Hull_HC1");
							Console.WriteLine("GET_OUT_STOCK 1 Engine_EC1");
							Console.WriteLine("GET_OUT_STOCK 1 Wings_WC1");
							Console.WriteLine("GET_OUT_STOCK 1 Thruster_TC1");
							break;
						default:
							Console.WriteLine("Type de vaisseau inconnu.");
							break;
					}

					Console.WriteLine($"FINISHED {vaisseau.Key}");
				}
			}
		}

		public void VerifyCommand(string command)
		{
			string[] parts = command.Split(' ');

			if (parts.Length != 2)
			{
				Console.WriteLine("ERROR Commande incorrecte : nombre incorrect d'arguments.");
				return;
			}

			string type = parts[1].ToUpper();

			switch (type)
			{
				case "EXPLORER":
				case "SPEEDER":
				case "CARGO":
					Verify(type);
					break;
				default:
					Console.WriteLine("ERROR Type de vaisseau inconnu.");
					break;
			}
		}
	}
}
