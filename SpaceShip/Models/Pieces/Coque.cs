namespace ShipFactoryApp.Models.Pieces
{
    public class Coque
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Coque(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}

