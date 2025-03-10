using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public static class UserArrayHandler
    {
        static int GetValidSize ()
        {
            var ArraySize = InputHandler.GetInput<int>("Введите количество элементов в массиве: ");
            while (ArraySize < 2)
            {
                Console.WriteLine(" Количество элементов должно быть больше 1");
                ArraySize = InputHandler.GetInput<int>(" - ");
            }
            return ArraySize;
        }

        public static int[] Manual_Input()
        {
            var ArraySize = GetValidSize();
            int[] arr = new int[ArraySize];

            for (int i = 0; i < ArraySize; i++)
            {
                arr[i] = InputHandler.GetInput<int>($" Значение {i+1} элемента: " );
            }
            return arr;
        }
    }
    public static class Functionality
    {
        enum Literals
        {
            CONTINUE = 1,
            EXIT = 2
        };
        public static bool Looping()
        {
            int response;
            while (true)
            {
                AdditionalInfo.LoopMenu();
                response = InputHandler.GetInput<int>(" - ");
                if (response == (int)Literals.CONTINUE) { return true; }
                else if (response == (int)Literals.EXIT) { return false; }
                Console.WriteLine(" Ошибка ввода! ");
            }

        }
        enum InputChoice
        {
            FROM_KEYBOARD = 1,
            FROM_FILE = 2
        }
        public static int ApplyingInputChoice()
        {
            int choice;
            while (true)
            {
                choice = InputHandler.GetInput<int>(" - ");
                switch (choice)
                {
                    case (int)InputChoice.FROM_KEYBOARD:
                        return choice;
                    case (int)InputChoice.FROM_FILE:
                        return choice;
                    default:
                        Console.WriteLine("Введите «1», «2»");
                        break;
                }
            }
        }
    }
}
