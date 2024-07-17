namespace ShipFactoryApp.Models.Pieces
{
    public class Ailes
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Ailes(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
