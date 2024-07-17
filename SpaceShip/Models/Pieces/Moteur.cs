namespace ShipFactoryApp.Models.Pieces
{
    public class Moteur
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Moteur(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
