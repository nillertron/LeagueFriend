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
        private static List<CancellationTokenSource> TokenList = new List<CancellationTokenSource>();

        //Statisk da det er muligt at åbne en ny instans af denne side imens den gamle tråd køre

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
            //Tjek aktive tråde og cancel
            if (TokenList.Count > 0)
            {
                for (int i = 0; i < TokenList.Count; i++)
                {
                    TokenList[i].Cancel();
                    TokenList.RemoveAt(i);
                }
            }

            var proc = Context.Resolve<ILolProcessor>();
            var list = new List<Match>();

            if (p != null)
            {
                await Task.Run(async () =>
                {
                    await GetDbMatchesFromPlayer(p, list);

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


            var storedToken = new CancellationTokenSource();
            TokenList.Add(storedToken);
            FillRemainingMatchesFromAPI(list, storedToken.Token);

            return list;
        }
        private async Task GetDbMatchesFromPlayer(Player p, List<Match> list)
        {
            var proc = Context.Resolve<ILolProcessor>();

            var db = (DbCon)Context.Resolve<IDbCon>();
            
            try
            {
                var matchList = await proc.GetMatchList(p);
                matchList.ForEach(o => list.Add(new Match { ChampionId = o.Champion, GameId = o.GameId, Lane = o.Lane, PlatformId = o.PlatformId, Queue = o.Queue, Role = o.Role, Season = o.Season, TimeStamp = o.TimeStamp }));

            }
            catch (Exception ee) { }
            var dbList = db.Match.Where(o => o.Participants.Any(x => x.PlayerId == p.Id)).Include(o => o.Participants).Include("Participants.Stats").Include(x => x.Teams).Include("Participants.TimeLine").ToList();
            foreach (var game in dbList)
            {

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
                        for (int i = 0; i < 120; i++)
                        {
                            await Task.Delay(TimeSpan.FromSeconds(10));
                            if (cts.IsCancellationRequested)
                            {
                                cts.ThrowIfCancellationRequested();
                            }
                        }
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
                            if (cts.IsCancellationRequested)
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

            });
        }
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

    }
}
