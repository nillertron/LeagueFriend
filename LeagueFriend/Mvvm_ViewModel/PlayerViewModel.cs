using Api.Processor;
using Autofac;
using EFLibrary.DataAcces;
using EFLibrary.Models;
using LeagueFriend.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueFriend.Mvvm_ViewModel
{
    public class PlayerViewModel : BaseViewModel, IPlayerViewModel
    {
        private IComponentContext Context;
        private ObservableCollection<Player> _PlayerListe = new ObservableCollection<Player>();
        public ObservableCollection<Player> PlayerListe { get => _PlayerListe; set { _PlayerListe = value; Notify("PlayerListe"); } }
        private string _SearchTb;
        public string SearchTb { get => _SearchTb; set { _SearchTb = value; Notify("SearchTb"); } }
        public ICommand SearchCommand { get { return _ClickCommand = new CommandHandler(async () => SearchCommandMethod(), () => true); } }
        private static CancellationTokenSource StoredToken;

        public PlayerViewModel(IComponentContext context)
        {
            Context = context;

            Task.Run(() =>
            {
                using (var db = (DbCon)Context.Resolve<IDbCon>())
                {
                    PlayerListe = new ObservableCollection<Player>(db.Player.Where(x => x.SaveSearch == true).ToList());
                }
            });


        }
        private async Task SearchCommandMethod()
        {
            var lolProcessor = Context.Resolve<ILolProcessor>();
            var player = await lolProcessor.FindAccountDetails(SearchTb);
            player.SaveSearch = true;
            using (var db = (DbCon)Context.Resolve<IDbCon>())
            {
                var dbPlayer = await db.FindAsync<Player>(player.Id);
                if (dbPlayer != null)
                {
                    dbPlayer.SaveSearch = true;
                    dbPlayer.SummonerLevel = player.SummonerLevel;
                    try
                    {
                        await db.SaveChangesAsync();

                        PlayerListe.Add(dbPlayer);
                    }
                    catch (Exception ee)
                    {
                        var msg = ee.Message;
                    }

                }
                else if (player != null)
                {
                    PlayerListe.Add(player);
                    db.Player.Add(player);
                    await db.SaveChangesAsync();
                }
            }


        }
        public async Task<List<Match>> GetMatchList(Player p)
        {
            if(StoredToken != null)
            {
                StoredToken.Cancel();
            }
            var proc = Context.Resolve<ILolProcessor>();
            var list = new List<Match>();
            if (p != null)
            {
                await Task.Run(async () =>
                {
                    var db = (DbCon)Context.Resolve<IDbCon>();

                    var matchList = await proc.GetMatchList(p);
                    matchList.ForEach(o => list.Add(new Match { ChampionId = o.Champion, GameId = o.GameId, Lane = o.Lane, PlatformId = o.PlatformId, Queue = o.Queue, Role = o.Role, Season = o.Season, TimeStamp = o.TimeStamp }));
                    var dbList = db.Match.ToList();
                    foreach (var game in dbList)
                    {
                        game.Participants = db.Participant.Where(x => x.MatchGameId == game.GameId).ToList();
                        game.Teams = db.Team.Where(o => o.Game == game).ToList();
                        foreach (var x in game.Participants)
                        {
                            x.Team = game.Teams.Where(o => o == x.Team).FirstOrDefault();

                        }
                        var listPosition = list.Where(o => o.GameId == game.GameId).FirstOrDefault();
                        if (listPosition != null)
                        {
                            listPosition.Participants = game.Participants;
                            listPosition.Teams = game.Teams;
                        }
                        else
                        {
                            list.Add(game);
                        }
                    }

                    var count = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        if (count >= list.Count)
                            break;
                        if (list[count].Participants.Count > 0)
                        {
                            i--;
                            count++;
                            continue;
                        }
                        try
                        {
                            list[count] = await proc.FillMatchDetails(list[count]);
                            await Task.Delay(50);
                        }
                        catch (Exception e)
                        {
                            //Hvis api limit er overskredet, break loopet og åben vinduet med cached games.
                            break;
                        }

                    }
               });
            }
            else
                throw new Exception("Player not found");
            StoredToken = new CancellationTokenSource();
            FillRemainingMatchesFromAPI(list, StoredToken.Token);

            return list;
        }
        private async Task FillRemainingMatchesFromAPI(List<Match> list, CancellationToken cts)
        {
            var proc = Context.Resolve<ILolProcessor>();
            var wait = true;
#pragma warning disable CS4014 
            Task.Run(async () =>
            {
            //Vent 2 minutter for ikke at overflow api'en

            Match dbMatch = null;
                for (int x = 0; x < list.Count; x++)
                {
                    if (wait)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(2));
                    }
                    using (var db = (DbCon)Context.Resolve<IDbCon>())
                    {
                        dbMatch = db.Match.Where(o => o.GameId == list[x].GameId && o.Participants.Count > 0).FirstOrDefault();

                        if (dbMatch != null)
                        {
                            wait = false;
                            continue;

                        }
                        for (int i = 0; i < 10; i++)
                        {
                            if(cts.IsCancellationRequested)
                            {
                                cts.ThrowIfCancellationRequested();
                            }
                            list[x] = await proc.FillMatchDetails(list[x]);
                            await Task.Delay(50);
                            if (x < list.Count)
                                x++;
                            else
                                break;


                        }
                        dbMatch = null;
                        wait = true;
                    }
                }
                StoredToken = null;
            });
        }
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

    }
}
