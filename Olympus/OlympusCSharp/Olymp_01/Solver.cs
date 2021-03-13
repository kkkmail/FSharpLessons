using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympusCSharp.Olymp_01
{
    public class Solver
    {
        private static (bool, bool, bool)[] Flips { get; } =
            new[]
            {
                (false, false, false),
                (false, false, true),
                (false, true, false),
                (false, true, true),
                (true, false, false),
                (true, false, true),
                (true, true, false),
                (true, true, true),
            };

        public static List<Barge> CreateAll(Brick x, Brick y, Brick z)
        {
            var b1 =
                Flips
                .Select(e => Barge.CreateBarge(x, y, z, true, e));

            var b2 =
                Flips
                .Select(e => Barge.CreateBarge(x, y, z, false, e));

            var b3 =
                Flips
                .Select(e => Barge.CreateBarge(z, y, x, false, e));

            var b4 =
                Flips
                .Select(e => Barge.CreateBarge(x, z, y, false, e));

            var b = b1
                .Concat(b2)
                .Concat(b3)
                .Concat(b4)
                .ToList();

            return b;
        }

        public static int FindBest(Brick x, Brick y, Brick z)
        {
            var a =
                CreateAll(x, y, z)
                .Select(e => e.Area)
                .Min();

            return a;
        }

        public static void RunBarge()
        {
            var b1 = Brick.CreateBrick();
            var b2 = Brick.CreateBrick();
            var b3 = Brick.CreateBrick();
            var a = FindBest(b1, b2, b3);
            Console.WriteLine(a);
        }
    }
}
