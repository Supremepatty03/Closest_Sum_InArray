using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2 {

    public static class FileWriter
    {
        enum FileChoise
        {
            REWRITE = 1,
            ADD = 2,
            CANCEL = 3
        }
        public static bool ContainsInvalidPathChars(string filePath)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

            string fileName = Path.GetFileName(filePath); // только имя файла без пути

            return filePath.Any(ch => invalidPathChars.Contains(ch)) || fileName.Any(ch => invalidFileNameChars.Contains(ch));
        }
        public static void EnsureFileNDirectoryExists(ref string filePath)
        {
            while (ContainsInvalidPathChars(filePath))
            {
                Console.WriteLine("Ошибка: путь к файлу содержит недопустимые символы.");
                Console.Write("Введите корректный путь к файлу: ");
                filePath = Console.ReadLine();
            }
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close(); // Создает файл и сразу закрывает поток, чтобы избежать блокировки
            }
        }
        public static int WhatToDoWithData(string filePath)
        {

            AdditionalInfo.FileMenu(filePath);

            int choice = InputHandler.GetInput<int>("Введите ваш выбор: ");

            return choice;
        }

        public static void ApplyingChoice(string filePath, int choice, string[] data)
        {
            if (choice == (int)FileChoise.REWRITE)
            {
                File.WriteAllLines(filePath, data);
            }
            else if (choice == (int)FileChoise.ADD)
            {
                File.AppendAllLines(filePath, data);
            }
            else if (choice == (int)FileChoise.CANCEL)
            {
                return;
            }

        }
        enum SaveOptions
        {
            ONLY_INPUT = 1,
            RESULT = 2,
            NO_SAVE = 3
        }
        public static void SavingToFile(string[] data, int target, Tuple<int, List<int>> result)
        {
            AdditionalInfo.SaveToFile();
            bool flag = true;
            while (flag)
            {
                int choice = InputHandler.GetInput<int>(" - ");
                string filepath;

                switch (choice)
                {
                    case (int)SaveOptions.ONLY_INPUT:
                        Console.Write(" Введите путь сохранения файла: ");
                        filepath = Console.ReadLine();
                        WriteArrayAndTargetToFile(ref filepath, data, target);
                        flag = false;

                        break;
                    case (int)SaveOptions.RESULT:
                        Console.Write(" Введите путь сохранения файла: ");
                        filepath = Console.ReadLine();
                        WriteResultToFile(ref filepath, result, data);
                        flag = false;
                        break;
                    case (int)SaveOptions.NO_SAVE:
                        Console.WriteLine("Данные не сохранены.");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }
        private static void PrepareFilePath(ref string filePath)
        {
            bool flag = true;
            while (flag)
            {
                try
                {
                    EnsureFileNDirectoryExists(ref filePath);
                    flag = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.Write(" Введите путь еще раз: ");
                    filePath = Console.ReadLine();
                }
            }
        }
        public static void WriteArrayAndTargetToFile(ref string filePath, string[] numbers, int target)
        {
            PrepareFilePath(ref filePath);

            // Создаём строковый массив, где первая строка — массив чисел (как строка), вторая строка — целевое число
            string[] data = new string[numbers.Length + 1];
            data[0] = string.Join(", ", numbers); // Первая строка - массив строк чисел
            data[1] = target.ToString();          // Вторая строка - целевое число

            // Проверяем, существует ли файл, и если есть данные, то спрашиваем, что делать
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                int choice = WhatToDoWithData(filePath);
                ApplyingChoice(filePath, choice, data);
                return;
            }

            // Записываем данные в файл
            File.WriteAllLines(filePath, data);
            Console.WriteLine("Данные успешно сохранены");
        }

        public static void WriteResultToFile(ref string filePath, Tuple<int, List<int>> result, string[] data)
        {
            PrepareFilePath(ref filePath);

            // Преобразуем Tuple<int, List<int>> в строку
            string resultString = $"Сумма: {result.Item1}, Подмассив: [{string.Join(", ", result.Item2)}]";

            // Создаем строку для массива чисел, объединяя их через запятую
            string dataString = string.Join(", ", data); // Соединяем числа в одну строку

            // Формируем итоговую строку с данными и результатом
            string[] combinedData = new string[] { dataString, resultString };

            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                int choice = WhatToDoWithData(filePath);
                ApplyingChoice(filePath, choice, combinedData);
                return;
            }

            // Записываем объединенные данные в файл
            File.WriteAllLines(filePath, combinedData);
            Console.WriteLine("Данные успешно сохранены");
        }
    }
}
