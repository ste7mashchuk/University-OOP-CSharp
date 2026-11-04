using System;

class Triangle
{
    private double a;
    private double b;
    private double c;

    public Triangle(double sideA, double sideB, double sideC)
    {
        a = sideA;
        b = sideB;
        c = sideC;
    }

    public bool IsPositive()
    {
        return a > 0 && b > 0 && c > 0;
    }

    public bool Exists()
    {
        return (a + b > c) && (a + c > b) && (b + c > a);
    }

    public double GetPerimeter()
    {
        return a + b + c;
    }

    public double GetArea()
    {
        double p = GetPerimeter() / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public void CheckType()
    {
        if (a == b && b == c)
        {
            Console.WriteLine("Рівносторонній трикутник");
        }
        else if (a == b || b == c || a == c)
        {
            Console.WriteLine("Рівнобедрений трикутник");
        }

        if (Math.Abs(a * a + b * b - c * c) < 0.0001 ||
            Math.Abs(a * a + c * c - b * b) < 0.0001 ||
            Math.Abs(b * b + c * c - a * a) < 0.0001)
        {
            Console.WriteLine("Прямокутний трикутник");
        }

        if (a != b && b != c && a != c &&
            Math.Abs(a * a + b * b - c * c) >= 0.0001 &&
            Math.Abs(a * a + c * c - b * b) >= 0.0001 &&
            Math.Abs(b * b + c * c - a * a) >= 0.0001)
        {
            Console.WriteLine("Довільний трикутник");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введіть сторону a: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введіть сторону b: ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введіть сторону c: ");
        double c = Convert.ToDouble(Console.ReadLine());

        Triangle t = new Triangle(a, b, c);

        if (!t.IsPositive())
        {
            Console.WriteLine("Помилка: сторони повинні бути додатними числами.");
            return;
        }

        if (!t.Exists())
        {
            Console.WriteLine("Трикутник з такими сторонами не існує.");
            return;
        }

        Console.WriteLine("Периметр: " + t.GetPerimeter());
        Console.WriteLine("Площа: " + t.GetArea());
        t.CheckType();
    }
}