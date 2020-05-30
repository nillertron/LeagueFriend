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
using Autofac;

namespace Api.Processor
{
    public class LolProcessor : ILolProcessor
    {
        string Key = "";
        private IComponentContext Context;
        public LolProcessor(IComponentContext context)
        {
            Context = context;
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
            var player = (Player)Context.Resolve<IPlayer>();
            using (var dbCon = (DbCon)Context.Resolve<IDbCon>())
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
            if (player == null)
                return null;

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
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var matchList = JsonConvert.DeserializeObject<Response>(response.Content);
                return matchList.Matches;

            }
            else
                throw new Exception("Api limit exceeded");
        }
        public async Task FindChampionFromId(Participant p, int id)
        {
            using (var dbCon = (DbCon)Context.Resolve<IDbCon>())
            {
                p.Champion = dbCon.Champion.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }

        }
        public async Task<Match> FillMatchDetails(Match game)
        {
            var db = (DbCon)Context.Resolve<IDbCon>();

            var storedGame = db.Match.Where(x => x.GameId == game.GameId).FirstOrDefault();
            if (storedGame == null)
            {
                try
                {
                    var client = new RestClient("https://euw1.api.riotgames.com/lol/match/v4/matches/" + game.GameId);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Accept-Charset", "application/x-www-form-urlencoded; charset=UTF-8");
                    request.AddHeader("X-Riot-Token", Key);
                    var response = await client.ExecuteAsync(request);
                    if (response.ResponseStatus == ResponseStatus.Error)
                        throw new Exception("Bad request");
                    var Match = JsonConvert.DeserializeObject<Response>(response.Content);
                    var Teams = new List<Team>();
                    Match.Teams.ForEach(x => Teams.Add(new Team { TeamId = x.TeamId, Win = x.Win }));
                    db.AddRange(Teams);
                    var participantList = new List<Participant>();
                    await Task.Run(async () =>
                    {
                        foreach(var o in Match.participantIdentities)
                        {
                            if (o.player.AccountId != null)
                            {
                                await Task.Delay(50);
                                Player player = null;

                                player = await FindAccountDetailsById(o.player.AccountId);
                                if (player != null)
                                {
                                    var participant = Match.Participants.Where(x => x.ParticipantId == o.ParticipantId).FirstOrDefault();

                                    db.Add(participant.Stats);

                                    var pt = new Participant { TimeLine = new TimeLine {Lane = participant.TimeLine.Lane }, Stats=participant.Stats,  PlayerId = player.Id, ChampionId = participant.ChampionId, Team = Teams.Where(x => x.TeamId == participant.TeamId).FirstOrDefault(), MatchGameId = game.GameId };

                                    if (participant.TimeLine.CreepsPerMinDeltas != null)
                                    {
                                        foreach (var d in participant.TimeLine.CreepsPerMinDeltas)
                                        {
                                            pt.TimeLine.CsPrMin.Add(new Delta { Period = d.Key, Value = d.Value });
                                        }
                                    }
                                    if (participant.TimeLine.CsDiffPerMinDeltas != null)
                                    {
                                        foreach (var d in participant.TimeLine.CsDiffPerMinDeltas)
                                        {
                                            pt.TimeLine.CsDiffPerMin.Add(new Delta { Period = d.Key, Value = d.Value });
                                        }
                                    }
                                    if (participant.TimeLine.XpDiffPerMinDeltas != null)
                                    {
                                        foreach (var d in participant.TimeLine.XpPerMinDeltas)
                                        {
                                            pt.TimeLine.XpPrMin.Add(new Delta { Period = d.Key, Value = d.Value });
                                        }
                                    }
                                    if (participant.TimeLine.XpDiffPerMinDeltas != null)
                                    {
                                        foreach (var d in participant.TimeLine.XpDiffPerMinDeltas)
                                        {
                                            pt.TimeLine.XpDiffPerMin.Add(new Delta { Period = d.Key, Value = d.Value });
                                        }
                                    }



                                    participantList.Add(pt);
                                }
                            }
                        }


                    });
                    Teams.ForEach(o => o.Game = game);
                    //db.Participant.AddRange(participantList);
                    //await db.SaveChangesAsync();
                    game.Participants = participantList;
                    db.Add(game);
                    await db.SaveChangesAsync();
                }
                catch (Exception ee)
                {
                    var msg = ee;
                }


            }
            else
            {
                game = storedGame;
                game.Participants = db.Participant.Where(x => x.MatchGameId == game.GameId).ToList();
               
                game.Teams = db.Team.Where(o => o.Game == game).ToList();
                foreach(var x in game.Participants)
                {
                    x.Team = game.Teams.Where(o => o == x.Team).FirstOrDefault();
                    x.TimeLine = db.TimeLine.Where(o => x.TimeLine == o).FirstOrDefault();
                    x.Stats = db.Stats.Where(o => x.Stats == o).FirstOrDefault();


                }
            }
            return game;
        }
    }
}
