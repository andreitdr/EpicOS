using System;

namespace CosmosKernel1.Items
{
    public class ProgressBar
    {
        public char LMargin { get; private set; }
        public char RMargin { get; private set; }
        public char Filler { get; private set; }
        public int MaxValue { get; private set; }

        public delegate void onProgressChanged(int percent);
        public onProgressChanged OnProgressChanged;



        public ProgressBar(char LMargin, char RMargin, char Filler, int MaxValue)
        {
            this.LMargin = LMargin;
            this.RMargin = RMargin;
            this.Filler = Filler;
            this.MaxValue = MaxValue;

            Percent = 0;
        }

        public int Percent { get; set; }

        public void WaitTime(int ticks, string text)
        {
            Console.WriteLine();
            Console.Write(text);
            Console.WriteLine();
            Console.Write(LMargin);

            for (int i = 0; i < ticks; ++i)
            {
                Cosmos.HAL.Global.PIT.Wait(1000);
                Console.Write(Filler);

            }

            Console.Write(RMargin);
            Cosmos.HAL.Global.PIT.Wait(1000);
            Console.WriteLine();
        }

        public void WaitTime(uint ms, int ticks, string text)
        {
            Console.WriteLine();
            Console.Write(text + "\n");
            Console.Write(LMargin);
            for (int i = 0; i < ticks; ++i)
            {
                Cosmos.HAL.Global.PIT.Wait(ms);
                Console.Write(Filler);

            }

            Console.Write(RMargin);
            Cosmos.HAL.Global.PIT.Wait(1000);
            Console.WriteLine();
        }

        public void WaitTimeWithRandomMS(int minMS, int maxMS, int ticks, string text)
        {
            Console.WriteLine();
            Console.Write(text + "\n");
            Console.Write(LMargin);
            for (int i = 0; i < ticks; ++i)
            {
                var rnd = new System.Random().Next(minMS, maxMS);
                Cosmos.HAL.Global.PIT.Wait((uint)rnd);
                Console.Write(Filler);

            }

            Console.Write(RMargin);
            Cosmos.HAL.Global.PIT.Wait(1000);
            Console.WriteLine();
        }
    }
}
