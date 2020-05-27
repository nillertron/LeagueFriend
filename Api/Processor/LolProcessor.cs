using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Api.Model;
using EFLibrary.Models;
using EFLibrary.DataAcces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Api.Processor
{
    public class LolProcessor : ILolProcessor
    {
        string Key = "";
        private DbCon db;
        public LolProcessor(IDbCon db)
        {
            this.db = (DbCon)db;
        }

        public async Task<Player> FindAccountDetails(string accName)
        {
            
            var client = new RestClient("https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/" + accName.Trim());
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept-Charset", "application/x-www-form-urlencoded; charset=UTF-8");
            request.AddHeader("X-Riot-Token", Key);
            var response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<Player>(response.Content);
        }
        public async Task<Player> FindAccountDetailsById(string AccountId)
        {
            var player = new Player();
            using (var dbCon = new DbCon())
            {

                player = dbCon.Player.Where(x => x.AccountId == AccountId).FirstOrDefault();
                if (player == null)
                {
                    var client = new RestClient("https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-account/" + AccountId);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Accept-Charset", "application/x-www-form-urlencoded; charset=UTF-8");
                    request.AddHeader("X-Riot-Token", Key);
                    var response = await client.ExecuteAsync(request);
                    player = JsonConvert.DeserializeObject<Player>(response.Content);
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        var rnd = new Random();
                        player = new Player { Id = Guid.NewGuid().ToString(), AccountId = AccountId };
                    }
                    dbCon.Player.Add(player);
                    await dbCon.SaveChangesAsync();
                }
            }


            return player;
        }

        public async Task<List<ChampionResponse>> GetAllChampions()
        {
            var client = new RestClient("http://ddragon.leagueoflegends.com/cdn/10.10.3216176/data/en_US/champion.json");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept-Charset", "application/x-www-form-urlencoded; charset=UTF-8");
            request.AddHeader("X-Riot-Token", Key);
            var response = await client.ExecuteAsync(request);
            var champDic = JsonConvert.DeserializeObject<Response>(response.Content);
            var champList = new List<ChampionResponse>();
            foreach (ChampionResponse c in champDic.Data.Values)
            {
                champList.Add(c);
            }
            return champList;
        }

        public async Task<List<MatchResponse>> GetMatchList(Player p)
        {
            var client = new RestClient("https://euw1.api.riotgames.com/lol/match/v4/matchlists/by-account/" + p.AccountId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept-Charset", "application/x-www-form-urlencoded; charset=UTF-8");
            request.AddHeader("X-Riot-Token", Key);
            var response = await client.ExecuteAsync(request);
            var matchList = JsonConvert.DeserializeObject<Response>(response.Content);
            return matchList.Matches;
        }
        public async Task FindChampionFromId(Participant p, int id)
        {
            using (var dbCon = new DbCon())
            {
                p.Champion = dbCon.Champion.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }

        }
        public async Task FillMatchDetails(Match game)
        {
            using(var db = new DbCon())
            {
                var storedGame = db.Match.Where(x => x.GameId == game.GameId).FirstOrDefault();
                if (storedGame == null)
                {
                    var client = new RestClient("https://euw1.api.riotgames.com/lol/match/v4/matches/" + game.GameId);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Accept-Charset", "application/x-www-form-urlencoded; charset=UTF-8");
                    request.AddHeader("X-Riot-Token", Key);
                    var response = await client.ExecuteAsync(request);
                    var Match = JsonConvert.DeserializeObject<Response>(response.Content);
                    var Teams = new List<Team>();
                    Match.Teams.ForEach(x => Teams.Add(new Team { TeamId = x.TeamId, Win = x.Win }));
                    db.AddRange(Teams);
                    Match.participantIdentities.ForEach(async o =>
                    {
                        using (var dbCon = new DbCon())
                        {
                            if (o.player.Id != null)
                            { 
                                var player = await FindAccountDetailsById(o.player.AccountId);

                            
                                var participant = Match.Participants.Where(x => x.ParticipantId == o.ParticipantId).FirstOrDefault();

                                var pt = new Participant { PlayerId = player.Id, ChampionId = participant.ChampionId, Team = Teams.Where(x => x.TeamId == participant.TeamId).FirstOrDefault(), Match = game };
                                game.Participants.Add(pt);
                            }

                        }

                    });

                        db.Add(game);
                        await db.SaveChangesAsync();

                }
                else
                {
                    game = storedGame;
                }

            }
            

        }
    }
}
