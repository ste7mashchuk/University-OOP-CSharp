namespace DeliveryApp
{
    class Order
    {
        public int Number { get; }
        public string Description { get; }
        public double Weight { get; }
        public double Distance { get; }

        public Order(int number, string description, double weight, double distance)
        {
            Number = number;
            Description = description;
            Weight = weight;
            Distance = distance;
        }

        public override string ToString()
        {
            return $"Замовлення №{Number}: {Description}, {Weight} кг, {Distance} км";
        }
    }
}
