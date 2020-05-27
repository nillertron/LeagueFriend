namespace EFLibrary.Models
{
    public interface ITeam
    {
        int Id { get; set; }
        int TeamId { get; set; }
        string Win { get; set; }
    }
}