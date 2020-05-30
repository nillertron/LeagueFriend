namespace EFLibrary.Models
{
    public interface IStats
    {
        int Assists { get; set; }
        int ChampLevel { get; set; }
        int Deaths { get; set; }
        int GoldEarned { get; set; }
        int Id { get; set; }
        int Item0 { get; set; }
        int Item1 { get; set; }
        int Item2 { get; set; }
        int Item3 { get; set; }
        int Item4 { get; set; }
        int Item5 { get; set; }
        int Item6 { get; set; }
        int Kills { get; set; }
        int LargestMultiKill { get; set; }
        long TotalDamageDealt { get; set; }
        int TotalMinionsKilled { get; set; }
        int TotalPlayerScore { get; set; }
        long VisionScore { get; set; }
    }
}