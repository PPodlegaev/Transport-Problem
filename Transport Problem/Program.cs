using System;

class TransportProblem
{
    /// <summary>
    /// Транспортная задача: метод минимального элемента
    /// </summary>
    static void Main()
    {
        Console.Write("Введите количество поставщиков: ");
        int numSources = int.Parse(Console.ReadLine());

        Console.Write("Введите количество потребителей: ");
        int numDestinations = int.Parse(Console.ReadLine());

        int[,] supplies = new int[numSources, 1];
        int[,] demands = new int[1, numDestinations];
        int[,] costs = new int[numSources, numDestinations];
        int[,] allocations = new int[numSources, numDestinations];

        Console.WriteLine();

        // Ввод запасов источников
        for (int i = 0; i < numSources; i++)
        {
            Console.Write($"Введите запас поставщика {i + 1}: ");
            supplies[i, 0] = int.Parse(Console.ReadLine());
        }

        // Ввод потребностей потребителей
        for (int i = 0; i < numDestinations; i++)
        {
            Console.Write($"Введите потребность потребителя {i + 1}: ");
            demands[0, i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine();

        // Ввод стоимостей перевозки грузов между источниками и потребителями
        Console.WriteLine("Введите стоимость перевозки груза от поставщика к потребителю");
        for (int i = 0; i < numSources; i++)
        {
            for (int j = 0; j < numDestinations; j++)
            {
                Console.Write("[{0}, {1}]: ", i + 1, j + 1);
                costs[i, j] = int.Parse(Console.ReadLine());
            }
        }

        int source = 0;
        int dest = 0;
        while (source < numSources && dest < numDestinations)
        {
            int minCost = int.MaxValue;

            // Находим ячейку с минимальной стоимостью в текущей строке
            for (int j = 0; j < numDestinations; j++)
            {
                if (costs[source, j] < minCost && demands[0, j] > 0)
                {
                    minCost = costs[source, j];
                    dest = j;
                }
            }

            // Если не нашли ячейку с отрицательным коэффициентом, переходим к следующей строке
            if (minCost == int.MaxValue)
            {
                source++;
                continue;
            }

            // Иначе находим количество груза, которое можно перевезти
            int quantity = Math.Min(supplies[source, 0], demands[0, dest]);
            allocations[source, dest] = quantity;

            // Уменьшаем оставшиеся запасы и потребности
            supplies[source, 0] -= quantity;
            demands[0, dest] -= quantity;

            // Если запас текущего источника исчерпан, переходим к следующему источнику
            if (supplies[source, 0] == 0)
                source++;
        }

        Console.WriteLine();

        // Выводим результат
        Console.WriteLine("Оптимальный план:");
        for (int i = 0; i < numSources; i++)
        {
            for (int j = 0; j < numDestinations; j++)
            {
                Console.Write($"{allocations[i, j]}\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine();

        // Считаем общую стоимость
        int totalCost = 0;
        for (int i = 0; i < numSources; i++)
        {
            for (int j = 0; j < numDestinations; j++)
            {
                totalCost += allocations[i, j] * costs[i, j];
            }
        }

        Console.WriteLine($"Общая стоимость: {totalCost}");
    }
}