namespace ShipFactoryApp.Models.Pieces
{
    public class Propulseur
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Propulseur(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}