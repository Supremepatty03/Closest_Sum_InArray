using System;
using System.Drawing;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace Lab2
{
    enum InputChoice
    {
        FROM_KEYBOARD = 1,
        FROM_FILE = 2
    }
    internal class Program
    {
        static void Main()
        {
            AdditionalInfo.Greetings();
            int[]? Array = default;
            int target = default;
            while (true)
            {
                AdditionalInfo.ChooseInputMethod();
                int UserInputChoice = Functionality.ApplyingInputChoice();
                if (UserInputChoice == (int)InputChoice.FROM_KEYBOARD)
                {
                    Array = UserArrayHandler.Manual_Input();
                    target = InputHandler.GetInput<int>(" Введите целевое число: ");
                }
                else if (UserInputChoice == (int)InputChoice.FROM_FILE)
                {
                    Console.WriteLine("Для отмены введите «RETURN»");
                    Console.Write(" Введите путь к файлу: ");
                    string filepath = Console.ReadLine();
                    var result = FileReader.ReadArrayAndTargetFromFile(ref filepath);

                    if (result.HasValue) { (Array, target) = result.Value; }
                    else { continue; }

                    Console.WriteLine("Массив чисел: " + string.Join(", ", Array));
                    Console.WriteLine("Целевое число: " + target);
                }

                Tuple <int , List<int>> pair = ArrayCheker.FindClosestSubset(Array, target);
                Console.WriteLine($"Сумма: {pair.Item1}");
                Console.WriteLine("Подмассив: " + string.Join(", ", pair.Item2));

                string[] stringArray = Array.Select(num => num.ToString()).ToArray();
                FileWriter.SavingToFile(stringArray, target, pair);

                if (!Functionality.Looping()) { break; }
            }
            AdditionalInfo.Farewell();
        }
    }

}
