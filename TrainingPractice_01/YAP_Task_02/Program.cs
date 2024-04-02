using System;

class Program
{
    static bool IsSameColor(string x1, int y1, string x2, int y2)
    {
        bool x1White = (x1[0] - 'a' + y1) % 2 == 0;
        bool x2White = (x2[0] - 'a' + y2) % 2 == 0;

        return x1White == x2White;
    }

    static void Board()
    {
        for (int i = 8; i >= 1; i--)
        {
            Console.Write($"{i} ");
            for (char j = 'a'; j <= 'h'; j++)
            {
                Console.Write((j - 'a' + i) % 2 == 0 ? "W" : "B");
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }

    static void Main()
    {
        Board();

        Console.Write("Введите координаты первого поля (например, a1): ");
        string coordinate1 = Console.ReadLine();
        string x1 = coordinate1.Substring(0, 1);
        int y1 = int.Parse(coordinate1.Substring(1, 1));

        Console.Write("Введите координаты второго поля: ");
        string coordinate2 = Console.ReadLine();
        string x2 = coordinate2.Substring(0, 1);
        int y2 = int.Parse(coordinate2.Substring(1, 1));

        if (IsSameColor(x1, y1, x2, y2))
        {
            Console.WriteLine("Поля одного цвета.");
        }
        else
        {
            Console.WriteLine("Поля разного цвета.");
        }
    }
}