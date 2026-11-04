using System;

namespace DeliveryApp
{
    class CashPayment : IPayment
    {
        public string Name => "готівка";

        public void Pay(decimal amount)
        {
            Console.WriteLine($"  Оплачено готівкою: {amount} грн");
        }
    }
}
