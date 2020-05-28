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
        //public ICommand PlayerDetailsCommand { get { return _ClickCommand = new CommandHandler(async () => PlayerDetails(), () => true); } }
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
            var proc = Context.Resolve<ILolProcessor>();
            var list = new List<Match>();
            if (p != null)
            {
                await Task.Run(async () =>
                {
                    var db = (DbCon)Context.Resolve<IDbCon>();

                    var matchList = await proc.GetMatchList(p);
                    matchList.ForEach(o => list.Add(new Match { ChampionId = o.Champion, GameId = o.GameId, Lane = o.Lane, PlatformId = o.PlatformId, Queue = o.Queue, Role = o.Role, Season = o.Season, TimeStamp = o.TimeStamp }));
                    for (int i = 0; i < 8; i++)
                    {
                        list[i] = await proc.FillMatchDetails(list[i]);
                        await Task.Delay(50);
                    }


                });
            }
            else
                throw new Exception("Player not found");

            Task.Run(async () =>
            {
                //Vent 2 minutter for ikke at overflow api'en
                await Task.Delay(TimeSpan.FromMinutes(2));
                Match dbMatch = null;
                for (int x = 0; x < list.Count; x++)
                {
                    using (var db = (DbCon)Context.Resolve<IDbCon>())
                    {
                        dbMatch = db.Match.Where(o => o.GameId == list[x].GameId).FirstOrDefault();
                    }
                    if (dbMatch == null)
                        continue;
                    for (int i = 0; i < 8; i++)
                    {
                        list[x] = await proc.FillMatchDetails(list[i]);
                        await Task.Delay(50);
                    }
                    dbMatch = null;
                }

            });
            return list;
        }
    }
}
