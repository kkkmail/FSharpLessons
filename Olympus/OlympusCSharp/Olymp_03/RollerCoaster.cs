using System;
using System.Linq;

namespace OlympusCSharp.Olymp_03
{
    public class RollerCoaster
    {
        private int ReadInt() => int.Parse(Console.ReadLine()!);
        private Hill ShortestHill(Hill h1, Hill? h2 = null) =>
            h2 == null ? h1 : (h1.Length <= h2.Length ? h1 : h2);

        private Hill? FindRollerCoaster(Pole[] poles)
        {
            Progress n = new NotStarted();
            Hill? m = null;

            foreach (var h in poles)
            {
                n = n.Check(h);
                if (n is not Found n1) continue;
                if (n1.IsBestCoaster) return n1.Hill;
                m = ShortestHill(n1.Hill, m);
            }

            return m;
        }

        public void RunRollerCoaster()
        {
            var n = ReadInt();

            var r =
                    new bool[n]
                        .Select((e, i) => new Pole {PoleNumber = i + 1, Height = ReadInt()})
                        .ToArray();

            var result = FindRollerCoaster(r);
            Console.WriteLine(result == null ? "0" : $"{result.StartPole.PoleNumber} {result.EndPole.PoleNumber}");
        }
    }
}
