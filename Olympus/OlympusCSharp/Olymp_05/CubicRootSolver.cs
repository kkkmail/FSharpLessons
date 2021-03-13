using System;
using System.Collections.Generic;
using System.Linq;

namespace OlympusCSharp.Olymp_05
{
    public class CubicRootSolver
    {
        public long A { get; }
        public long B { get; }
        public long C { get; }
        public long D { get; }

        private CubicRootSolver(long a, long b, long c, long d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public static CubicRootSolver Create()
        {
            var s = Console.ReadLine();

            var x =
                s.Split(" ")
                    .Select(e => long.Parse(e))
                    .ToArray();

            return new CubicRootSolver(x[0], x[1], x[2], x[3]);
        }

        /// <summary>
        /// x >=0 && y >= 0
        /// </summary>
        private long GetGCD(long x, long y)
        {
            var (a, b) = x >= y ? (x, y) : (y, x);

            switch (b)
            {
                case 0:
                    return a;
                case 1:
                    return b;
                default:
                {
                    var r = a % b;
                    return r == 0 ? b : GetGCD(b, r);
                }
            }
        }

        public void Run()
        {
            var solutions = SolveAll();

            if (solutions == null)
            {
                Console.WriteLine("-1");
            }
            else
            {
                Console.WriteLine($"{solutions.Length}");

                if (solutions.Length > 0)
                {
                    var x = string.Join(" ", solutions);
                    Console.WriteLine(x);
                }
            }
        }

        /// <summary>
        /// x >= 0
        /// </summary>
        private long[] GetDivisors(long x)
        {
            if (x == 0) return new long[] { 0 };
            var s = (long) Math.Sqrt(x) + 1;

            var result = new List<long>
            {
                1,
                x,
            };

            for (var i = 2; i <= s; i++)
            {
                if (x % i != 0) continue;
                result.Add(i);
                result.Add(x / i);
            }

            return result.ToArray();
        }

        /// <summary>
        /// A == B == C == D == 0
        /// </summary>
        private long[] Solve0000() => null!;

        private long[] NoSolutions { get; } = Array.Empty<long>();

        /// <summary>
        /// A == B == C == 0, D != 0
        /// </summary>
        private long[] Solve000() => NoSolutions;

        /// <summary>
        /// A == B == 0, C != 0
        /// </summary>
        private long[] Solve00() =>
            Math.Abs(D) % Math.Abs(C) == 0
                ? new[] { D / C, }
                : NoSolutions;

        /// <summary>
        /// A == 0, B != 0
        /// </summary>
        private long[] Solve0() => Solve2(B, C, D);

        private long[] Solve2(long a, long b, long c)
        {
            var discr = b * b - 4 * a * c;

            if (discr < 0) return NoSolutions;
            var root = (long) Math.Round(Math.Sqrt(discr), 0);
            if (root * root != discr) return NoSolutions;

            return new[]
                {
                    (-b + root) / (2 * a),
                    (-b - root) / (2 * a),
                }
                .Where(VerifyRoot)
                .Distinct()
                .OrderBy(e => e)
                .ToArray();
        }

        /// <summary>
        /// A != 0
        /// </summary>
        private long[] Solve()
        {
            if (D == 0)
            {
                var roots2 =
                    Solve2(A, B, C)
                        .Concat(new long[] { 0 })
                        .Where(VerifyRoot)
                        .Distinct()
                        .OrderBy(e => e)
                        .ToArray();

                return roots2;
            }

            var gcd = GetGCD(Math.Abs(A), Math.Abs(D));
            var d1 = D / gcd;
            var divisors = GetDivisors(Math.Abs(d1));

            var negativeDivisors =
                divisors
                    .Select(e => -e)
                    .ToArray();

            var roots =
                divisors
                    .Concat(negativeDivisors)
                    .Where(VerifyRoot)
                    .Distinct()
                    .OrderBy(e => e)
                    .ToArray();

            return roots;
        }

        private long[] SolveAll() =>
            A != 0
                ? Solve()
                : B != 0
                    ? Solve0()
                    : C != 0
                        ? Solve00()
                        : D != 0
                            ? Solve000()
                            : Solve0000();

        private bool VerifyRoot(long x)
        {
            var y = (decimal) x;

            var result =
                A == 0
                    ? B * y * y + C * y + D
                    : y * y * y + B * y * y / A + C * y / A + D / ((decimal) A);

            return (double)Math.Abs(result) < 1.0e-10;
        }
    }
}
