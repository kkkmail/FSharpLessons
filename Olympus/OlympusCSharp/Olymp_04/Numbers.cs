using System;

namespace OlympusCSharp.Olymp_04
{
    public class Numbers
    {
        private int ReadInt() => int.Parse(Console.ReadLine()!);
        private int A { get; }
        private int B { get; }

        public Numbers()
        {
            A = ReadInt();
            B = ReadInt();
        }

        private int CalculateAll() =>
            (B1 - A1 + 1) / 2 + CalculateA1() + CalculateB1();

        public void Run() => Console.WriteLine(CalculateAll());
        private int A1 => A % 10 == 0 ? A : ((A / 10) + 1) * 10;
        private int B1 => (B + 1) % 10 == 0 ? B : ((B / 10) - 1) * 10 + 9;

        private int SumOfDigits(int x)
        {
            var answer = 0;

            while (x != 0)
            {
                answer += x % 10;
                x /= 10;
            }

            return answer;
        }

        /// <summary>
        /// x is included, y is NOT included.
        /// </summary>
        private int Calculate(int x, int y) =>
            (y - x) / 2 + ((y - x) % 2 == 0 ? 0 : SumOfDigits(x) % 2);

        private int CalculateA1() => Calculate(A, A1);
        private int CalculateB1() => Calculate(B1 + 1, B + 1);
    }
}
