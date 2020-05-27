namespace EFLibrary.Models
{
    public interface IParticipant
    {
        Champion Champion { get; set; }
        Match Match { get; set; }
        int ParticipantId { get; set; }
        Player Player { get; set; }
        Team Team { get; set; }
    }
}