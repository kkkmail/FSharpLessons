using System;

namespace OlympusCSharp.Olymp_03
{
    public abstract record Progress
    {
        public virtual bool IsFound => false;
        public virtual bool IsBestCoaster => false;
        public abstract Progress Check(Pole x);
    }

    public record NotStarted : Progress
    {
        public override Progress Check(Pole x) => new Started {Start = x};
    }

    public record Started : Progress
    {
        public Pole Start { get; init; }

        public override Progress Check(Pole x) =>
            Start.Height < x.Height
                ? new InProgress {Start = Start, Current = x}
                : new Started {Start = x};
    }

    public record InProgress : Progress
    {
        public Pole Start { get; init; }
        public Pole Current { get; init; }

        public override Progress Check(Pole x)
        {
            if (Current.Height > x.Height)
            {
                return new Found {Hill = new Hill {StartPole = Start, EndPole = x}};
            }

            if (Current.Height == x.Height)
            {
                return this with {Current = x};
            }

            return new InProgress {Start = Current, Current = x};
        }
    }

    public record Found : Progress
    {
        public Hill Hill { get; init; }
        public override bool IsFound => true;
        public override bool IsBestCoaster => Hill.Length == 3;
        public override Progress Check(Pole x) => this;
    }
}
