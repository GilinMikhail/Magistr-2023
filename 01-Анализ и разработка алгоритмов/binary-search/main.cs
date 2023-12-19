using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Открываем файл для чтения
        string filePath = "./abiturs.csv";
        List<string> lines = File.ReadAllLines(filePath).Skip(1).ToList();

        // Создаем список для хранения записей
        List<Tuple<int, string, int>> records = new List<Tuple<int, string, int>>();

        // Обрабатываем каждую строку
        foreach (string line in lines)
        {
            // Разделяем строку по запятым
            string[] parts = line.Split(',');

            // Создаем кортеж (запись) из элементов строки
            Tuple<int, string, int> record = new Tuple<int, string, int>(
                int.Parse(parts[0]), parts[1], int.Parse(parts[2]));

            // Добавляем запись в список
            records.Add(record);
        }

        // Выводим список записей
        Console.WriteLine("Original Records:");
        foreach (var record in records)
        {
            Console.WriteLine($"{record.Item1}, {record.Item2}, {record.Item3}");
        }

        // Сортируем записи по третьему элементу в порядке убывания
        records.Sort((rec1, rec2) => rec2.Item3.CompareTo(rec1.Item3));

        // Выводим отсортированный список записей
        Console.WriteLine("\nSorted Records:");
        foreach (var record in records)
        {
            Console.WriteLine($"{record.Item1}, {record.Item2}, {record.Item3}");
        }
    }
}
