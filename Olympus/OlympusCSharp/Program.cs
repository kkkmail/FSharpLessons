using System;
using System.Diagnostics;
using OlympusCSharp.Olymp_01;
using OlympusCSharp.Olymp_02;
using OlympusCSharp.Olymp_03;
using OlympusCSharp.Olymp_04;
using OlympusCSharp.Olymp_05;

namespace OlympusCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Solver.RunBarge();
            // new FenceRepairer().RunFence();
            // new RollerCoaster().RunRollerCoaster();

            // Console.WriteLine("Starting");
            // var sw = new Stopwatch();
            // sw.Start();
            //
            // int calculate(int i, int cnt)
            // {
            //     return cnt + (i % 2);
            // }
            //
            // var count = 0;
            //
            // for (var i = 0; i < 1_000_000_000; i++)
            // {
            //     count = calculate(i, count);
            // }
            //
            // Console.WriteLine($"Elapsed: {sw.Elapsed.TotalSeconds}.");
            // Console.WriteLine($"Count = {count}.");

            // new Numbers().Run();

            CubicRootSolver.Create().Run();
        }
    }
}
