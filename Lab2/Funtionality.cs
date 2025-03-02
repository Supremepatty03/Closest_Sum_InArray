using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
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
    }
}
