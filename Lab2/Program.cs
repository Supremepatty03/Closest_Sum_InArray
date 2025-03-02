using System;

namespace Lab2
{
    internal class Program
    {
        static void Main()
        {
            AdditionalInfo.Greetings();
            while (true)
            {
                
                if (!Functionality.Looping()) { break; }
            }
            AdditionalInfo.Farewell();
        }
    }

}
