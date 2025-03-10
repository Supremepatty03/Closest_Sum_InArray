using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public static class FileReader
    {
        public static (int[] numbers, int target)? ReadArrayAndTargetFromFile(ref string filePath)
        {
            if (filePath == "RETURN") { return default; }

            filePath = ValidateFilePath(filePath);
            if (filePath == null) return default;

            string[] lines = ReadFileLines(filePath);
            if (lines == null || lines.Length < 2)
            {
                Console.WriteLine("Ошибка: Ожидался хотя бы один массив чисел и целевое число.");
                return default;
            }

            int[] numbers = ParseNumbers(lines[0]); // Разбираем массив чисел
            if (numbers == null) return default;

            int target;
            if (!int.TryParse(lines[1], out target)) // Читаем целевое число
            {
                Console.WriteLine("Ошибка: Вторая строка должна быть целым числом.");
                return default;
            }

            return (numbers, target);
        }

        private static string ValidateFilePath(string filePath)
        {
            while (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!");
                Console.Write("Введите путь к файлу: ");
                filePath = Console.ReadLine();
                if (filePath == "RETURN") return null;
            }
            return filePath;
        }

        private static string[] ReadFileLines(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length == 0)
                    throw new InvalidDataException("Файл пуст. Ожидался хотя бы один элемент.");
                return lines;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return default;
            }
        }

        private static int[] ParseNumbers(string line)
        {
            try
            {
                return line.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(num => int.Parse(num))
                           .ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке массива чисел: {ex.Message}");
                return default;
            }
        }
    }
}
