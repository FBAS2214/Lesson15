namespace Lesson15_2
{
    [Serializable]
    public class Car
    {
        public int Year { get; set; }
        public string? Model { get; set; }
        public string? Make { get; set; }

        public override string ToString() => $"{Year} - {Model} - {Make}";
    }
}