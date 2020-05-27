using EFLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EFLibrary.DataAcces
{
    public interface IDbCon
    {
        DbSet<Match> Match { get; set; }
        DbSet<Player> Player { get; set; }
        DbSet<Champion> Champion { get; set; }
        DbSet<Team> Team { get; set; }
        DbSet<Participant> Participant { get; set; }
    }
}