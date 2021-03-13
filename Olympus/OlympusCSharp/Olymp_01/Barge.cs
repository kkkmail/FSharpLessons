using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympusCSharp.Olymp_01
{
    public record Barge
    {
        /// <summary>
        /// If true, then (X, Y, Z)
        /// If false, then ((X, Y), Z)
        /// </summary>
        public bool IsOneRow { get; init; }

        public Brick X { get; init; }
        public Brick Y { get; init; }
        public Brick Z { get; init; }

        private int GetOneRowArea()
        {
            var a = X.A + Y.A + Z.A;
            var b = Math.Max(Math.Max(X.B, Y.B), Z.B);
            return a * b;
        }

        private int GetTwoRowsArea()
        {
            var a = Math.Max(X.A + Y.A, Z.A);
            var b = Math.Max(X.B, Y.B) + Z.B;
            return a * b;
        }

        private int GetArea() => IsOneRow ? GetOneRowArea() : GetTwoRowsArea();
        public int Area => GetArea();

        public static Barge CreateBarge(
            Brick x,
            Brick y,
            Brick z,
            bool isOneRow,
            (bool flipX, bool flipY, bool flipZ) flips)
        {
            var x1 = x.TryFlip(flips.flipX);
            var y1 = y.TryFlip(flips.flipY);
            var z1 = z.TryFlip(flips.flipZ);

            return new Barge
            {
                IsOneRow = isOneRow,
                X = x1,
                Y = y1,
                Z = z1
            };
        }
    }
}
