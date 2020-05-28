using Api.Model;
using EFLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Processor
{
    public interface ILolProcessor
    {
        Task<Match> FillMatchDetails(Match game);
        Task<Player> FindAccountDetails(string accName);
        Task<Player> FindAccountDetailsById(string AccountId);
        Task<List<ChampionResponse>> GetAllChampions();
        Task<List<MatchResponse>> GetMatchList(Player p);
    }
}