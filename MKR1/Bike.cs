namespace DeliveryApp
{
    class Bike : Transport
    {
        public Bike(string name) : base(name, 5) { }

        public override decimal CalculateCost(Order order)
        {
            return 20 + 5 * (decimal)order.Distance;
        }

        public override bool CanTake(Order order)
        {
            return base.CanTake(order) && order.Distance <= 10;
        }
    }
}
