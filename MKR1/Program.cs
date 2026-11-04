using System;
using System.Text;

namespace DeliveryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            DeliveryService service = new DeliveryService();

            Console.WriteLine("=== 1. Створення транспорту і замовлень ===");
            Transport bike = new Bike("Велосипед");
            Transport car = new Car("Автомобіль");
            Transport truck = new Truck("Вантажівка");
            service.AddTransport(bike);
            service.AddTransport(car);
            service.AddTransport(truck);

            Order o1 = service.CreateOrder("Документи", 1, 5);
            Order o2 = service.CreateOrder("Піца", 2, 15);            
            Order o3 = service.CreateOrder("Холодильник", 80, 20);
            Order o4 = service.CreateOrder("Верстат", 2000, 30);
            Order o5 = service.CreateOrder("Книги", 4, 8);

            Console.WriteLine();
            Console.WriteLine("=== 2. Вартість доставки ===");
            service.ShowCosts(o1);
            service.ShowCosts(o2);
            service.ShowCosts(o3);
            service.ShowCosts(o4);

            Console.WriteLine();
            Console.WriteLine("=== 3. Призначення замовлень ===");
            service.Assign(o1, bike);
            service.Assign(o5, bike);
            service.Assign(o2, bike);
            service.Assign(o3, car);
            service.Assign(o3, truck);
            service.Assign(o4, truck);
            service.Assign(o5, car);

            Console.WriteLine();
            service.PrintActive();
            service.PrintHistory();

            Console.WriteLine();
            Console.WriteLine("=== 4. Доставка замовлень ===");
            service.Deliver(o1, new CardPayment());
            service.Deliver(o3, new CashPayment());
            service.Deliver(o2, new CardPayment());

            Console.WriteLine();
            Console.WriteLine("Велосипед і вантажівка тепер вільні, призначаємо замовлення №2 ще раз:");
            service.Assign(o2, bike);
            service.Assign(o2, car);
            service.Assign(o2, truck);

            Console.WriteLine();
            Console.WriteLine("=== 5. Активні замовлення та історія ===");
            service.PrintActive();
            service.PrintHistory();

            Console.WriteLine();
            Console.WriteLine("Доставляємо решту:");
            service.Deliver(o5, new CashPayment());
            service.Deliver(o2, new CardPayment());

            Console.WriteLine();
            service.PrintActive();
            service.PrintHistory();
        }
    }
}
