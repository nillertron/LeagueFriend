using System.Collections.Generic;

namespace EFLibrary.Models
{
    public interface IMatch
    {
        Champion Champion { get; }
        long GameId { get; set; }
        string Lane { get; set; }
        List<Participant> Participants { get; set; }
        string PlatformId { get; set; }
        int Queue { get; set; }
        string Role { get; set; }
        int Season { get; set; }
        List<Team> Teams { get; set; }
        long TimeStamp { get; set; }
    }
}