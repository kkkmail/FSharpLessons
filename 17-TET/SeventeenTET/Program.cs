// See https://aka.ms/new-console-template for more information
using NAudio.Wave;
using SeventeenTET;

Console.WriteLine("Playingn 17 TET!");

using (var sineWaveProvider = new SineWaveProvider32())
{
    sineWaveProvider.SetWaveFormat(16000, 1);
    using (var waveOut = new WaveOutEvent())
    {
        waveOut.Init(sineWaveProvider);
        waveOut.Play();

        float baseFreq = 440.0f; // A4 in Hz
        float seventeenthRootOfTwo = (float)Math.Pow(2.0, 1.0 / 17.0);
        var melody = SeventeenTetMelodies.CsardasLikeMelody;

        foreach (var note in melody)
        {
            float freq = baseFreq * (float)Math.Pow(seventeenthRootOfTwo, note.Tone);
            sineWaveProvider.Frequency = freq;
            Thread.Sleep(note.Length); // Delay for each note based on tuple value
        }

        waveOut.Stop();
    }
}
