using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryApp
{
    class DeliveryService
    {
        private List<Transport> transports = new List<Transport>();
        private List<Order> orders = new List<Order>();
        private List<Delivery> active = new List<Delivery>();
        private List<Delivery> history = new List<Delivery>();
        private int nextOrderNumber = 1;

        //створення транспорту і замовлень
        public void AddTransport(Transport transport)
        {
            transports.Add(transport);
            Console.WriteLine($"Додано транспорт: {transport}");
        }

        public Order CreateOrder(string description, double weight, double distance)
        {
            Order order = new Order(nextOrderNumber++, description, weight, distance);
            orders.Add(order);
            Console.WriteLine($"Створено: {order}");
            return order;
        }

        //вартість доставки кожним транспортом, який може взяти
        public void ShowCosts(Order order)
        {
            Console.WriteLine($"Вартість доставки для {order}:");
            bool any = false;
            foreach (Transport t in transports)
            {
                if (t.CanTake(order))
                {
                    Console.WriteLine($"  {t.Name}: {t.CalculateCost(order)} грн");
                    any = true;
                }
            }
            if (!any)
            {
                Console.WriteLine("  Жоден транспорт не може взяти це замовлення.");
            }
        }

        //призначення замовлення на транспорт
        public bool Assign(Order order, Transport transport)
        {
            if (active.Any(d => d.Order == order) || history.Any(d => d.Order == order))
            {
                Console.WriteLine($"Не вдалося: замовлення №{order.Number} вже призначене або доставлене.");
                return false;
            }
            if (transport.IsBusy)
            {
                Console.WriteLine($"Не вдалося: {transport.Name} зайнятий.");
                return false;
            }
            if (!transport.CanTake(order))
            {
                Console.WriteLine($"Не вдалося: {transport.Name} не може взяти замовлення №{order.Number}.");
                return false;
            }

            transport.IsBusy = true;
            Delivery delivery = new Delivery(order, transport, transport.CalculateCost(order));
            active.Add(delivery);
            Console.WriteLine($"Замовлення №{order.Number} призначено на {transport.Name}.");
            return true;
        }

        //доставка з оплатою
        public bool Deliver(Order order, IPayment payment)
        {
            Delivery delivery = active.FirstOrDefault(d => d.Order == order);
            if (delivery == null)
            {
                Console.WriteLine($"Не вдалося: замовлення №{order.Number} немає серед активних.");
                return false;
            }

            Console.WriteLine($"Доставка замовлення №{order.Number} транспортом {delivery.Transport.Name}...");
            payment.Pay(delivery.Cost);
            delivery.PaymentName = payment.Name;

            delivery.Transport.IsBusy = false;
            active.Remove(delivery);
            history.Add(delivery);
            Console.WriteLine("  Доставлено.");
            return true;
        }

        //вивід активних замовлень та історії
        public void PrintActive()
        {
            Console.WriteLine("--- Активні замовлення ---");
            if (active.Count == 0)
            {
                Console.WriteLine("  (порожньо)");
            }
            foreach (Delivery d in active)
            {
                Console.WriteLine("  " + d);
            }
        }

        public void PrintHistory()
        {
            Console.WriteLine("--- Історія доставок ---");
            if (history.Count == 0)
            {
                Console.WriteLine("  (порожньо)");
            }
            foreach (Delivery d in history)
            {
                Console.WriteLine("  " + d);
            }
        }
    }
}
