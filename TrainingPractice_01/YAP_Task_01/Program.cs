using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество золота: ");
        int gold = int.Parse(Console.ReadLine());

        Console.Write("Введите цену за один кристалл: ");
        int pricePerCrystal = int.Parse(Console.ReadLine());

        int crystalsToBuy = gold / pricePerCrystal;
        int remainingGold = gold % pricePerCrystal;

        Console.WriteLine($"Вы можете купить {crystalsToBuy} кристаллов. Останется золота: {remainingGold}, кристаллов: {crystalsToBuy}");
    }
}