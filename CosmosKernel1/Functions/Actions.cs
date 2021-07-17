using System;

namespace CosmosKernel1.Functions
{
    public static class Actions
    {
        public static void CreateProgressBar(int ticksSeconds, char borderL = '[', char borderR = ']', char BarChar = '=')
        {
            Console.Write(borderL);
            for (int i = 0; i < ticksSeconds; ++i)
            {
                Cosmos.HAL.Global.PIT.Wait(1000);
                Console.Write(BarChar);
            }
            Console.Write(borderR);
        }

        public static void WriteTextCharByChar(string text, uint delayMS)
        {
            int len = text.Length;
            char[] a = text.ToCharArray();
            for (int i = 0; i < len; ++i)
            {
                Console.Write(a[i]);
                Cosmos.HAL.Global.PIT.Wait(delayMS);
            }
        }
    }
}
