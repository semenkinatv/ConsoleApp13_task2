using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CountWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            // path = @"C:\Semenkina\Text1.txt";

            while (true)
            {
                Console.WriteLine("\r\nВведите путь к файлу с текстом:");
                path = Console.ReadLine();
                if (!(File.Exists(path)))
                {
                    Console.WriteLine("Данный файл не существует!");
                }
                else
                {
                    break;
                }
            }

            // читаем весь файл в строку текста
            string text = File.ReadAllText(path);

            //зачистка знаков пунктуации
            var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

            // Сохраняем символы-разделители в массив
            char[] delimiters = new char[] { ' ', '\r', '\n' };

            // разбиваем нашу строку текста, используя ранее перечисленные символы-разделители
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            // выводим количество
            Console.WriteLine($"\r\nКоличество слов в тексте романа 'Обломов': {words.Length}");
            Console.WriteLine();

            // Создаем Словарь, где слово - ключ, количество его использований в тексте - значение 
            var word_count = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (word_count.ContainsKey(word))
                    word_count[word]++;
                else
                    word_count.Add(word, 1);
            }

            //сортировка по значению
            word_count = word_count.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            Console.WriteLine("10 слов, которые чаще всего встречаются в тексте:");
            Console.WriteLine();

            var cnt = word_count.Count - 1;
            for (int i = 0; i < 10; i++)
            {
                var value = word_count.ElementAt(cnt - i).Value;
                var key = word_count.ElementAt(cnt - i).Key;

                Console.WriteLine(key + "    " + value);
            }
        }
    }
}
