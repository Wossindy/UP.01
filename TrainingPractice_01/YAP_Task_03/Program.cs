using System;

class Program
{
    static void Main()
    {
        // Характеристики босса
        int hpBoss = 500;
        int urBossa = 100;

        // Характеристики игрока
        int hpPlayer = 500;
        bool razlomYESareNO = false;

        // Возможные заклинания
        int rashamon = 100;
        int hungazakura = 100;
        int vostanovlenie = 250;
        int mini = 50;
        int superUdar = 450;

        // Битва с боссом
        while (hpBoss > 0 && hpPlayer > 0)
        {
            // Выводим текущие характеристики
            Console.WriteLine($"У игрока {hpPlayer} HP и у босса {hpBoss} HP");

            // Игрок выбирает заклинание
            Console.WriteLine("Выберите заклинание:");
            Console.WriteLine("1. Рашамон");
            Console.WriteLine("2. Хуганзакура");
            Console.WriteLine("3. Межпространственный разлом");
            Console.WriteLine("4. Мини");
            Console.WriteLine("5. Удар С");

            int vibor = Convert.ToInt32(Console.ReadLine());

            // Применяем выбранное заклинание
            switch (vibor)
            {
                case 1:
                    if (!razlomYESareNO)
                    {
                        hpPlayer -= urBossa; // Игрок получает урон от босса
                        hpPlayer -= rashamon; // Игрок использует заклинание
                    }
                    else
                    {
                        Console.WriteLine("Вы не можете использовать Рашамон в разломе!");
                    }
                    break;
                case 2:
                    if (razlomYESareNO)
                    {
                        hpBoss -= hungazakura; // Игрок использует заклинание
                    }
                    else
                    {
                        Console.WriteLine("Хуганзакура может быть выполнен только после призыва теневого духа!");
                    }
                    break;
                case 3:
                    if (!razlomYESareNO)
                    {
                        hpPlayer += vostanovlenie; // Игрок восстанавливает HP
                        razlomYESareNO = true; // Игрок скрывается в разломе
                    }
                    else
                    {
                        Console.WriteLine("Вы уже находитесь в разломе!");
                    }
                    break;
                case 4:
                    if (razlomYESareNO)
                    {
                        hpBoss -= mini;
                    }
                    break;
                case 5:
                        if (hpPlayer < 100)
                    {
                        hpBoss -= superUdar;
                        hpPlayer += 50;
                    }
                        else
                    {
                        Console.WriteLine("Вы не можете использовать данную способность!");
                    }
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            // Босс атакует
            if (!razlomYESareNO)
            {
                hpPlayer -= urBossa;
            }
        }

        // Выводим результат битвы
        if (hpBoss <= 0)
        {
            Console.WriteLine("Вы победили босса! Поздравляем!");
        }
        else
        {
            Console.WriteLine("Босс победил вас. Попробуйте еще раз!");
        }
    }
}