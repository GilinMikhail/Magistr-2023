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

    // Функция для поиска слова в отсортированном списке и возвращения индекса и количества шагов поиска
    static Tuple<int, int> GetIndexWord(List<string> allWords, string word)
    {
        int l = 0, r = allWords.Count - 1;
        int steps = 0;

        // Бинарный поиск
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

        return Tuple.Create(-1, steps); // Возвращаем -1, если слово не найдено
    }

    static void Main()
    {
        Console.Clear(); // Очищаем консоль (для Linux используйте Console.Clear())

        // Читаем слова из файлов
        List<string> allWords = GetListWords("./words_utf.txt");
        List<string> findWords = GetListWords("./find_words.txt");

        // Ищем каждое слово из списка findWords в списке allWords
        foreach (string word in findWords)
        {
            var result = GetIndexWord(allWords, word);
            int index = result.Item1; // Получаем индекс из Tuple
            int steps = result.Item2; // Получаем количество шагов из Tuple

            if (index != -1)
            {
                Console.WriteLine($"Word '{word}' found at index {index} in {steps} steps.");
            }
            else
            {
                Console.WriteLine($"Word '{word}' not found.");
            }
        }

        // Дополнительный код, если необходим
    }
}
