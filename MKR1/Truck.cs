namespace DeliveryApp
{
    class Truck : Transport
    {
        public Truck(string name) : base(name, 1000) { }

        public override decimal CalculateCost(Order order)
        {
            return 200 + 15 * (decimal)order.Distance + 0.5m * (decimal)order.Weight;
        }
    }
}
