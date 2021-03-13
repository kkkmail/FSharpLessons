using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympusCSharp.Olymp_01
{
    public record Brick
    {
        public int A { get; init; }
        public int B { get; init; }

        public Brick Flip() => new Brick { A = B, B = A };
        public Brick TryFlip(bool b) => b ? Flip() : this;
        public static int GetInt32() => int.Parse(Console.ReadLine());

        public static Brick CreateBrick() =>
            new Brick { A = GetInt32(), B = GetInt32() };
    }
}
