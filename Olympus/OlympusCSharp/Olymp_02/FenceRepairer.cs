using System;
using System.Collections;
using System.Linq;

namespace OlympusCSharp.Olymp_02
{
    public class FenceRepairer
    {
        private int ReadInt() => int.Parse(Console.ReadLine()!);

        private bool ReadBool()
        {
            var x = int.Parse(Console.ReadLine()!);
            return x != 0;
        }

        private bool[] CreateFence(int k)
        {
            var x = new bool[k];

            for (var i = 0; i < k; i++)
            {
                x[i] = ReadBool();
            }

            // var y =
            //     Enumerable.Range(0, k - 1)
            //         .Select(e => ReadBool())
            //         .ToArray();

            return x;
        }

        int FindFirstBroken(bool[] f, int current)
        {
            if (current >= f.Length)
            {
                return f.Length;
            }

            for (var i = current; i <= f.Length - 1; i++)
            {
                if (f[i])
                {
                    return i;
                }
            }

            return f.Length;
        }

        private int Repair(int n, int current) => current + n;

        private int RepairAll(bool[] f, int n)
        {
            var i = 0;

            for (var current = 0; current <= f.Length - 1;)
            {
                current = FindFirstBroken(f, current);

                if (current < f.Length)
                {
                    current = Repair(n, current);
                    i++;
                }
            }

            return i;
        }

        public void RunFence()
        {
            var n = ReadInt();
            var k = ReadInt();
            var fence = CreateFence(k);
            var i = RepairAll(fence, n);
            Console.WriteLine(i);
        }
    }
}
