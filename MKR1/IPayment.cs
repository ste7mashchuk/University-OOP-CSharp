namespace DeliveryApp
{
    interface IPayment
    {
        string Name { get; }
        void Pay(decimal amount);
    }
}
