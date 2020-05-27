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
        private DbCon db;
        private IComponentContext Context;
        private ObservableCollection<Player> _PlayerListe = new ObservableCollection<Player>();
        public ObservableCollection<Player> PlayerListe { get => _PlayerListe; set { _PlayerListe = value; Notify("PlayerListe"); } }
        private string _SearchTb;
        public string SearchTb { get => _SearchTb; set { _SearchTb = value; Notify("SearchTb"); } }
        public ICommand SearchCommand { get { return _ClickCommand = new CommandHandler(async () => SearchCommandMethod(), () => true); } }
        //public ICommand PlayerDetailsCommand { get { return _ClickCommand = new CommandHandler(async () => PlayerDetails(), () => true); } }
        public PlayerViewModel(IDbCon dbCon, IComponentContext context)
        {
            db = (DbCon)dbCon;
            Context = context;
            Task.Run(() =>
            {
                PlayerListe = new ObservableCollection<Player>(db.Player.OrderBy(x => x.Id).ToList());
            });
        }
        private async Task SearchCommandMethod()
        {
            var lolProcessor = Context.Resolve<ILolProcessor>();
            var player = await lolProcessor.FindAccountDetails(SearchTb);
            if (player != null)
            {
                PlayerListe.Add(player);
                db.Player.Add(player);
                await db.SaveChangesAsync();
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
                    using (db = new DbCon())
                    {
                        var matchList = await proc.GetMatchList(p);
                        matchList.ForEach(o => list.Add(new Match { ChampionId = o.Champion, GameId = o.GameId, Lane = o.Lane, PlatformId = o.PlatformId, Queue = o.Queue, Role = o.Role, Season = o.Season, TimeStamp = o.TimeStamp }));
                        for (int i = 0; i < list.Count; i++)
                        {
                            await proc.FillMatchDetails(list[i]);
                            await Task.Delay(50);
                        }
                    }

                });
            }
            else
                return null;

            return list;
        }

    }
}
