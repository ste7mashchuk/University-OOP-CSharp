namespace DeliveryApp
{
    class Delivery
    {
        public Order Order { get; }
        public Transport Transport { get; }
        public decimal Cost { get; }
        public string PaymentName { get; set; } = "-";

        public Delivery(Order order, Transport transport, decimal cost)
        {
            Order = order;
            Transport = transport;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"{Order} | {Transport.Name} | {Cost} грн | оплата: {PaymentName}";
        }
    }
}
