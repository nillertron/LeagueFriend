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
        Task FindChampionFromId(Participant p, int id);
        Task<List<ChampionResponse>> GetAllChampions();
        Task GetLiveMatchDetails(string accId);
        Task<List<MatchResponse>> GetMatchList(Player p);
    }
}