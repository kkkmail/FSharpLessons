using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenTET
{
    public static class SeventeenTetMelodies
    {
        // Extended Hungarian Csárdás-like 17-TET Melody
        // Each tuple represents (Tone, Length in milliseconds)
        public static readonly (int Tone, int Length)[] CsardasLikeMelody =
            {
            // Lassú (slow) section
            (0, 800), (7, 800), (4, 600), (0, 200),
            (0, 800), (7, 800), (4, 600), (0, 200),
            (7, 800), (12, 800), (7, 600), (4, 200),
            (0, 800), (7, 800), (4, 600), (0, 200),

            (0, 800), (7, 800), (12, 600), (7, 200),
            (4, 800), (0, 800), (4, 600), (0, 200),
            (7, 800), (12, 800), (7, 600), (4, 200),
            (0, 800), (7, 800), (4, 600), (0, 200),

            // Transition
            (0, 400), (7, 400), (12, 400),

            // Friss (fast) section
            (0, 200), (4, 200), (7, 200), (9, 200),
            (12, 150), (16, 150), (12, 150), (9, 150),
            (7, 200), (4, 200), (0, 200), (16, 200),
            (12, 150), (9, 150), (7, 150), (4, 150),

            (0, 200), (4, 200), (7, 200), (9, 200),
            (12, 150), (16, 150), (12, 150), (9, 150),
            (7, 200), (4, 200), (0, 200), (16, 200),
            (12, 150), (9, 150), (7, 150), (4, 150),

            (0, 200), (4, 200), (7, 200), (9, 200),
            (12, 150), (16, 150), (12, 150), (9, 150),
            (7, 200), (4, 200), (0, 200), (16, 200),
            (12, 150), (9, 150), (7, 150), (4, 150)
        };
    }
}
