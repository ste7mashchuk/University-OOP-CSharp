namespace DeliveryApp
{
    class Car : Transport
    {
        public Car(string name) : base(name, 50) { }

        public override decimal CalculateCost(Order order)
        {
            return 50 + 8 * (decimal)order.Distance + 1 * (decimal)order.Weight;
        }
    }
}
