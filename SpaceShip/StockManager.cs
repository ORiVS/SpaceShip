using System;
using System.Collections.Generic;
using ShipFactoryApp.Factories;
using ShipFactoryApp.Models.Pieces;
using ShipFactoryApp.Models.Vaisseaux;

namespace ShipFactoryApp.Managers
{
    public class StockManager
    {
        private static StockManager _instance;
        private Dictionary<string, int> stock = new Dictionary<string, int>();

        private StockManager()
        {
            // Initialize with some stock
            AddToStock("Hull_HE1", 5);
            AddToStock("Engine_EE1", 5);
            AddToStock("Wings_WE1", 5);
            AddToStock("Thruster_TE1", 10);
            AddToStock("Hull_HS1", 5);
            AddToStock("Engine_ES1", 5);
            AddToStock("Wings_WS1", 5);
            AddToStock("Thruster_TS1", 10);
            AddToStock("Hull_HC1", 5);
            AddToStock("Engine_EC1", 5);
            AddToStock("Wings_WC1", 5);
            AddToStock("Thruster_TC1", 5);
        }

        public static StockManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StockManager();
                }
                return _instance;
            }
        }

        public void AddToStock(string piece, int quantity)
        {
            if (stock.ContainsKey(piece))
            {
                stock[piece] += quantity;
            }
            else
            {
                stock[piece] = quantity;
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

        public void NeededStocks(Dictionary<string, int> order)
        {
            Dictionary<string, Dictionary<string, int>> needed = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, int> total = new Dictionary<string, int>();

            foreach (var item in order)
            {
                string vaisseauType = item.Key;
                int quantity = item.Value;

                if (!needed.ContainsKey(vaisseauType))
                {
                    needed[vaisseauType] = new Dictionary<string, int>();
                }

                for (int i = 0; i < quantity; i++)
                {
                    var vaisseau = VaisseauFactory.CreateVaisseau(vaisseauType);
                    if (vaisseau != null)
                    {
                        AddNeededPiece(needed[vaisseauType], total, vaisseau.Coque);
                        AddNeededPiece(needed[vaisseauType], total, vaisseau.Moteur);
                        AddNeededPiece(needed[vaisseauType], total, vaisseau.Ailes);
                        foreach (var propulseur in vaisseau.Propulseurs)
                        {
                            AddNeededPiece(needed[vaisseauType], total, propulseur);
                        }
                    }
                }
            }

            DisplayNeededPieces(needed);
            DisplayTotalNeededPieces(total);
        }

        private void AddNeededPiece(Dictionary<string, int> needed, Dictionary<string, int> total, dynamic piece)
        {
            if (piece != null)
            {
                if (!needed.ContainsKey(piece.Name))
                {
                    needed[piece.Name] = 0;
                }
                needed[piece.Name] += piece.Quantity;

                if (!total.ContainsKey(piece.Name))
                {
                    total[piece.Name] = 0;
                }
                total[piece.Name] += piece.Quantity;
            }
        }

        private void DisplayNeededPieces(Dictionary<string, Dictionary<string, int>> needed)
        {
            foreach (var vaisseau in needed)
            {
                Console.WriteLine($"{vaisseau.Key}:");
                foreach (var piece in vaisseau.Value)
                {
                    Console.WriteLine($"{piece.Value} {piece.Key}");
                }
            }
        }

        private void DisplayTotalNeededPieces(Dictionary<string, int> total)
        {
            Console.WriteLine("Total:");
            foreach (var piece in total)
            {
                Console.WriteLine($"{piece.Value} {piece.Key}");
            }
        }

        public void Instructions(Dictionary<string, int> order)
        {
            foreach (var item in order)
            {
                string vaisseauType = item.Key;
                int quantity = item.Value;

                for (int i = 0; i < quantity; i++)
                {
                    Console.WriteLine($"PRODUCING {vaisseauType}");
                    var vaisseau = VaisseauFactory.CreateVaisseau(vaisseauType);
                    if (vaisseau != null)
                    {
                        DisplayAssemblyInstructions(vaisseau);
                    }
                    Console.WriteLine($"FINISHED {vaisseauType}");
                }
            }
        }

        private void DisplayAssemblyInstructions(IVaisseau vaisseau)
        {
            if (vaisseau.GetVaisseauType() == "Speeder")
            {
                Console.WriteLine("GET_OUT_STOCK 1 Hull_HS1");
                Console.WriteLine("GET_OUT_STOCK 1 Engine_ES1");
                Console.WriteLine("GET_OUT_STOCK 1 Wings_WS1");
                Console.WriteLine("GET_OUT_STOCK 2 Thruster_TS1");
                Console.WriteLine("ASSEMBLE TMP1 Hull_HS1 Engine_ES1");
                Console.WriteLine("ASSEMBLE TMP1 Wings_WS1");
                Console.WriteLine("ASSEMBLE TMP3 [TMP1,Wings_WS1] Thruster_TS1");
                Console.WriteLine("ASSEMBLE TMP3 Thruster_TS1");
            }
            else
            {
                DisplayGetOutStockInstructions(vaisseau.Coque);
                DisplayGetOutStockInstructions(vaisseau.Moteur);
                DisplayGetOutStockInstructions(vaisseau.Ailes);
                foreach (var propulseur in vaisseau.Propulseurs)
                {
                    DisplayGetOutStockInstructions(propulseur);
                }
            }
        }

        private void DisplayGetOutStockInstructions(dynamic piece)
        {
            if (piece != null)
            {
                Console.WriteLine($"GET_OUT_STOCK {piece.Quantity} {piece.Name}");
            }
        }

        public void Verify(Dictionary<string, int> order)
        {
            Dictionary<string, int> needed = new Dictionary<string, int>();

            foreach (var item in order)
            {
                string vaisseauType = item.Key;
                int quantity = item.Value;

                // Check if the vaisseauType is valid
                if (VaisseauFactory.CreateVaisseau(vaisseauType) == null)
                {
                    Console.WriteLine($"ERROR `{vaisseauType}` is not a recognized spaceship");
                    return;
                }

                for (int i = 0; i < quantity; i++)
                {
                    var vaisseau = VaisseauFactory.CreateVaisseau(vaisseauType);
                    if (vaisseau != null)
                    {
                        AddNeededPiece(needed, needed, vaisseau.Coque);
                        AddNeededPiece(needed, needed, vaisseau.Moteur);
                        AddNeededPiece(needed, needed, vaisseau.Ailes);
                        foreach (var propulseur in vaisseau.Propulseurs)
                        {
                            AddNeededPiece(needed, needed, propulseur);
                        }
                    }
                }
            }

            foreach (var piece in needed)
            {
                if (!stock.ContainsKey(piece.Key) || stock[piece.Key] < piece.Value)
                {
                    Console.WriteLine($"UNAVAILABLE");
                    return;
                }
            }
            Console.WriteLine("AVAILABLE");
        }
    }
}
