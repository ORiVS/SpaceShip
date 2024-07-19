namespace ShipFactoryApp.Managers
{
    public class StockManager
    {
        // Étape 1: Ajouter un champ privé statique pour contenir l'instance unique.
        private static StockManager _instance;

        // Étape 2: Rendre le constructeur privé pour empêcher l'instanciation directe.
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

        // Étape 3: Fournir une méthode publique statique pour accéder à l'instance unique.
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

        private Dictionary<string, int> stock = new Dictionary<string, int>();

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
                    var vaisseau = CreateVaisseau(vaisseauType);
                    if (vaisseau != null)
                    {
                        foreach (var prop in vaisseau.GetType().GetProperties())
                        {
                            if (prop.PropertyType == typeof(Coque) || prop.PropertyType == typeof(Moteur) ||
                                prop.PropertyType == typeof(Ailes))
                            {
                                var piece = prop.GetValue(vaisseau) as dynamic;
                                if (!needed[vaisseauType].ContainsKey(piece.Name))
                                {
                                    needed[vaisseauType][piece.Name] = 0;
                                }
                                needed[vaisseauType][piece.Name] += piece.Quantity;

                                if (!total.ContainsKey(piece.Name))
                                {
                                    total[piece.Name] = 0;
                                }
                                total[piece.Name] += piece.Quantity;
                            }
                            else if (prop.PropertyType == typeof(List<Propulseur>))
                            {
                                var propulseurs = prop.GetValue(vaisseau) as List<Propulseur>;
                                foreach (var propulseur in propulseurs)
                                {
                                    if (!needed[vaisseauType].ContainsKey(propulseur.Name))
                                    {
                                        needed[vaisseauType][propulseur.Name] = 0;
                                    }
                                    needed[vaisseauType][propulseur.Name] += propulseur.Quantity;

                                    if (!total.ContainsKey(propulseur.Name))
                                    {
                                        total[propulseur.Name] = 0;
                                    }
                                    total[propulseur.Name] += propulseur.Quantity;
                                }
                            }
                        }
                    }
                }
            }

            foreach (var vaisseau in needed)
            {
                Console.WriteLine($"{vaisseau.Key}:");
                foreach (var piece in vaisseau.Value)
                {
                    Console.WriteLine($"{piece.Value} {piece.Key}");
                }
            }

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
                    var vaisseau = CreateVaisseau(vaisseauType);
                    if (vaisseau != null)
                    {
                        if (vaisseauType == "Speeder")
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
                            foreach (var prop in vaisseau.GetType().GetProperties())
                            {
                                if (prop.PropertyType == typeof(Coque) || prop.PropertyType == typeof(Moteur) ||
                                    prop.PropertyType == typeof(Ailes))
                                {
                                    var piece = prop.GetValue(vaisseau) as dynamic;
                                    Console.WriteLine($"GET_OUT_STOCK {piece.Quantity} {piece.Name}");
                                }
                                else if (prop.PropertyType == typeof(List<Propulseur>))
                                {
                                    var propulseurs = prop.GetValue(vaisseau) as List<Propulseur>;
                                    foreach (var propulseur in propulseurs)
                                    {
                                        Console.WriteLine($"GET_OUT_STOCK {propulseur.Quantity} {propulseur.Name}");
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine($"FINISHED {vaisseauType}");
                }
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
                if (CreateVaisseau(vaisseauType) == null)
                {
                    Console.WriteLine($"ERROR `{vaisseauType}` is not a recognized spaceship");
                    return;
                }

                for (int i = 0; i < quantity; i++)
                {
                    var vaisseau = CreateVaisseau(vaisseauType);
                    if (vaisseau != null)
                    {
                        foreach (var prop in vaisseau.GetType().GetProperties())
                        {
                            if (prop.PropertyType == typeof(Coque) || prop.PropertyType == typeof(Moteur) ||
                                prop.PropertyType == typeof(Ailes))
                            {
                                var piece = prop.GetValue(vaisseau) as dynamic;
                                if (!needed.ContainsKey(piece.Name))
                                {
                                    needed[piece.Name] = 0;
                                }
                                needed[piece.Name] += piece.Quantity;
                            }
                            else if (prop.PropertyType == typeof(List<Propulseur>))
                            {
                                var propulseurs = prop.GetValue(vaisseau) as List<Propulseur>;
                                foreach (var propulseur in propulseurs)
                                {
                                    if (!needed.ContainsKey(propulseur.Name))
                                    {
                                        needed[propulseur.Name] = 0;
                                    }
                                    needed[propulseur.Name] += propulseur.Quantity;
                                }
                            }
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

        private Vaisseau CreateVaisseau(string vaisseauType)
        {
            switch (vaisseauType)
            {
                case "Explorer":
                    return new Exploreur();
                case "Speeder":
                    return new Speeder();
                case "Cargo":
                    return new Cargo();
                default:
                    return null;
            }
        }
    }
}
