namespace DeliveryApp
{
    abstract class Transport
    {
        public string Name { get; }
        public double MaxWeight { get; }
        public bool IsBusy { get; set; }

        protected Transport(string name, double maxWeight)
        {
            Name = name;
            MaxWeight = maxWeight;
            IsBusy = false;
        }

        public abstract decimal CalculateCost(Order order);

        public virtual bool CanTake(Order order)
        {
            return order.Weight <= MaxWeight;
        }

        public override string ToString()
        {
            return $"{Name} (макс. вага {MaxWeight} кг, {(IsBusy ? "зайнятий" : "вільний")})";
        }
    }
}
