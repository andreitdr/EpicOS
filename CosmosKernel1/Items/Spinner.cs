using System;

namespace CosmosKernel1.Items
{
    public class Spinner
    {
        private int counter;
        public Spinner() { counter = 0; }

        public void StartTurn(int turns)
        {
            while (counter <= turns * 4)
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Cosmos.HAL.Global.PIT.Wait(1000);
            }

        }

        public void StartTurn(int turns, string text)
        {
            Console.Write(text + "   ");
            while (counter <= turns * 4)
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Cosmos.HAL.Global.PIT.Wait(1000);
            }

        }
        public void StartTurn(int turns, uint delayMS)
        {
            while (counter <= turns * 4)
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Cosmos.HAL.Global.PIT.Wait(delayMS);
            }

        }
        public void StartTurn(int turns, uint delayMS, string text)
        {
            Console.Write(text + "   ");
            while (counter <= turns * 4)
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Cosmos.HAL.Global.PIT.Wait(delayMS);
            }

        }
    }
}
