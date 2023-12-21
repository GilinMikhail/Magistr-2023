using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    // Функция для чтения слов из файла и возвращения списка слов
    static List<string> GetListWords(string filename)
    {
        List<string> result = new List<string>();
        // Используем блок using для автоматического закрытия файла после использования
        using (StreamReader reader = new StreamReader(filename, System.Text.Encoding.UTF8))
        {
            string line;
            // Читаем строки из файла и добавляем их в список
            while ((line = reader.ReadLine()) != null)
            {
                result.Add(line.Trim());
            }
        }
        return result;
    }

    // Функция для линейного поиска слова в списке и возвращения индекса и количества шагов поиска
    static Tuple<int, int> LinearSearch(List<string> allWords, string word)
    {
        int steps = 0;
        for (int i = 0; i < allWords.Count; i++)
        {
            steps++;
            if (word == allWords[i])
            {
                return Tuple.Create(i, steps);
            }
        }
        return Tuple.Create(-1, steps);
    }

    // Функция для бинарного поиска слова в отсортированном списке и возвращения индекса и количества шагов поиска
    static Tuple<int, int> BinarySearch(List<string> allWords, string word)
    {
        int l = 0, r = allWords.Count - 1;
        int steps = 0;

        while (l <= r)
        {
            int mid = (l + r) / 2;
            steps++;

            if (word == allWords[mid])
            {
                return Tuple.Create(mid, steps);
            }
            if (word.CompareTo(allWords[mid]) > 0)
            {
                l = mid + 1;
            }
            if (word.CompareTo(allWords[mid]) < 0)
            {
                r = mid - 1;
            }
        }

        return Tuple.Create(-1, steps);
    }

    // Функция для интерполяционного поиска слова в отсортированном списке и возвращения индекса и количества шагов поиска
    static Tuple<int, int> InterpolationSearch(List<string> allWords, string word)
    {
        int steps = 0;
        int low = 0, high = allWords.Count - 1;

        while (low <= high && word.CompareTo(allWords[low]) >= 0 && word.CompareTo(allWords[high]) <= 0)
        {
            steps++;
            if (low == high)
            {
                if (word == allWords[low])
                {
                    return Tuple.Create(low, steps);
                }
                return Tuple.Create(-1, steps);
            }

            int pos = low + (int)(((double)(high - low) / (allWords[high].CompareTo(allWords[low]) == 0 ? 1 : allWords[high].CompareTo(allWords[low]))) * (word.CompareTo(allWords[low])));

            if (word == allWords[pos])
            {
                return Tuple.Create(pos, steps);
            }

            if (word.CompareTo(allWords[pos]) > 0)
            {
                low = pos + 1;
            }
            else
            {
                high = pos - 1;
            }
        }

        return Tuple.Create(-1, steps);
    }

    static void Main()
    {
        Console.Clear();

        // Читаем слова из файлов
        List<string> allWords = GetListWords("./words_utf.txt");
        List<string> findWords = GetListWords("./find_words.txt");

        foreach (string word in findWords)
        {
            // Линейный поиск
            var linearResult = LinearSearch(allWords, word);

            // Бинарный поиск
            var binaryResult = BinarySearch(allWords, word);

            // Интерполяционный поиск
            var interpolationResult = InterpolationSearch(allWords, word);

            // Вывод результатов для каждого метода
            Console.WriteLine($"Слово '{word}':");
            Console.WriteLine($"Линейный поиск - Индекс: {linearResult.Item1}, Шаги: {linearResult.Item2}");
            Console.WriteLine($"Бинарный поиск - Индекс: {binaryResult.Item1}, Шаги: {binaryResult.Item2}");
            Console.WriteLine($"Интерполяционный поиск - Индекс: {interpolationResult.Item1}, Шаги: {interpolationResult.Item2}");
            Console.WriteLine();
        }
    }
}
