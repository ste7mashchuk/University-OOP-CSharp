using System;

namespace DeliveryApp
{
    class CardPayment : IPayment
    {
        public string Name => "картка";

        public void Pay(decimal amount)
        {
            Console.WriteLine($"  Оплачено карткою: {amount} грн");
        }
    }
}
