using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static int playerHealth = 10; // Устанавливаем здоровье игрока на 10
    static bool showPath = false; // Флаг для отображения пути до выхода

    static void Main()
    {
        char[,]? map = ReadMapFromFile("map.txt");
        if (map != null)
        {
            int playerX = 1;
            int playerY = 1;
            int exitX = -1;
            int exitY = -1;

            FindExit(map, ref exitX, ref exitY);

            // Добавление врагов
            AddEnemies(map);

            DrawMap(map, playerX, playerY);

            while (true)
            {
                MoveEnemies(map); // Движение врагов

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    // Обработка управления игроком
                    case ConsoleKey.UpArrow:
                        if (playerY > 0 && map[playerY - 1, playerX] != '#')
                            playerY--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (playerY < map.GetLength(0) - 1 && map[playerY + 1, playerX] != '#')
                            playerY++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (playerX > 0 && map[playerY, playerX - 1] != '#')
                            playerX--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (playerX < map.GetLength(1) - 1 && map[playerY, playerX + 1] != '#')
                            playerX++;
                        break;
                    case ConsoleKey.Escape:
                        return; // Выход из программы по нажатию Esc
                    case ConsoleKey.Spacebar:
                        showPath = !showPath; // Переключаем флаг отображения пути
                        DrawMap(map, playerX, playerY); // Обновляем карту после изменения флага
                        break;
                }

                // Проверяем, коснулся ли враг игрока
                if (map[playerY, playerX] == 'X')
                {
                    playerHealth -= 1; // Уменьшаем здоровье игрока
                    Console.SetCursorPosition(map.GetLength(1) + 2, playerY);
                    Console.Write(" "); // Удаляем символ врага, чтобы он не находился в той же ячейке, что и игрок
                }

                Console.Clear();
                DrawMap(map, playerX, playerY);

                if (playerX == exitX && playerY == exitY)
                {
                    Console.WriteLine("Вы прошли лабиринт!");
                    return;
                }

                // Проверяем, закончилось ли здоровье игрока
                if (playerHealth <= 0)
                {
                    Console.WriteLine("Вы проиграли. Здоровье закончилось.");
                    return;
                }
            }
        }
        else
        {
            Console.WriteLine("Ошибка загрузки карты. Убедитесь, что файл 'map.txt' существует и доступен.");
        }
    }

    static char[,]? ReadMapFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            int height = lines.Length;
            int width = lines[0].Length;
            char[,] map = new char[height, width];

            for (int y = 0; y < height; y++)
            {
                if (lines[y].Length != width)
                {
                    Console.WriteLine("Ошибка загрузки карты. Строки должны иметь одинаковую длину.");
                    return null;
                }

                for (int x = 0; x < width; x++)
                {
                    map[y, x] = lines[y][x];
                }
            }

            return map; // Возвращаем карту, если файл существует и прочитан успешно
        }
        else
        {
            Console.WriteLine("Ошибка загрузки карты. Убедитесь, что файл 'map.txt' существует и доступен.");
            return null; // Возвращаем null, если файл не найден
        }
    }

    static void DrawMap(char[,] map, int playerX, int playerY)
    {
        int height = map.GetLength(0);
        int width = map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (showPath && map[y, x] == '.')
                {
                    bool isPath = PathExists(map, playerX, playerY, x, y);
                    if (isPath)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }

                if (x == playerX && y == playerY)
                {
                    Console.Write('■'); // Символ игрока
                }
                else
                {
                    Console.Write(map[y, x]);
                }

                Console.ResetColor();
            }
            Console.WriteLine();
        }

        // Рисуем HealthBar
        DrawHealthBar(map.GetLength(1) + 2, 1);
    }

    static void DrawHealthBar(int x, int y)
    {
        int healthWidth = playerHealth; // Используем текущее значение здоровья для ширины HealthBar
        Console.SetCursorPosition(x, y);
        Console.Write("[");
        Console.Write(new string('#', healthWidth));
        Console.Write(new string('_', 10 - healthWidth)); // Исправлено на 10 - healthWidth
        Console.Write("]");
    }

    static void FindExit(char[,] map, ref int exitX, ref int exitY)
    {
        int height = map.GetLength(0);
        int width = map.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[y, x] == 'E')
                {
                    exitX = x;
                    exitY = y;
                    return;
                }
            }
        }
    }

    static void AddEnemies(char[,] map)
    {
        Random random = new Random();

        // Добавление врагов на случайные пустые клетки карты
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == '.')
                {
                    if (random.Next(100) < 20) // Вероятность добавления врага = 20%
                    {
                        map[y, x] = 'X'; // Символ врага
                    }
                }
            }
        }
    }

    static void MoveEnemies(char[,] map)
    {
        Random random = new Random();

        // Движение врагов
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 'X')
                {
                    int direction = random.Next(4); // Выбор случайного направления

                    int newX = x;
                    int newY = y;

                    // Перемещение врага на одну клетку в выбранном направлении
                    switch (direction)
                    {
                        case 0: // Вверх
                            newY = Math.Max(0, y - 1);
                            break;
                        case 1: // Вниз
                            newY = Math.Min(map.GetLength(0) - 1, y + 1);
                            break;
                        case 2: // Влево
                            newX = Math.Max(0, x - 1);
                            break;
                        case 3: // Вправо
                            newX = Math.Min(map.GetLength(1) - 1, x + 1);
                            break;
                    }

                    if (map[newY, newX] == '.')
                    {
                        map[y, x] = '.';
                        map[newY, newX] = 'X';
                    }
                }
            }
        }
    }

    static bool PathExists(char[,] map, int startX, int startY, int targetX, int targetY)
    {
        // Просто проверяем, является ли целевая клетка пустой
        return map[targetY, targetX] == '.';
    }
}
