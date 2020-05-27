namespace EFLibrary.Models
{
    public interface IPlayer
    {
        string AccountId { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        int ProfileIconId { get; set; }
        string PuuId { get; set; }
        int SummonerLevel { get; set; }
    }
}