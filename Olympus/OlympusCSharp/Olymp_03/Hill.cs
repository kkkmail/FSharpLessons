namespace OlympusCSharp.Olymp_03
{
    public record Hill
    {
        public Pole StartPole { get; init; }
        public Pole EndPole { get; init; }

        public int Length => EndPole.PoleNumber - StartPole.PoleNumber + 1;
    }
}
