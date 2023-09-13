using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenTET
{
    public class SineWaveProvider32 : WaveProvider32, IDisposable
    {
        int sample;

        public float Frequency { get; set; }
        public float Amplitude { get; set; }

        public SineWaveProvider32()
        {
            Frequency = 440.0f;
            Amplitude = 0.25f;
            SetWaveFormat(16000, 1); // 16kHz mono
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;

            for (int n = 0; n < sampleCount; n++)
            {
                buffer[n + offset] = (float)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                if (++sample >= sampleRate) sample = 0;
            }
            return sampleCount;
        }

        public void Dispose()
        {
        }
    }
}
