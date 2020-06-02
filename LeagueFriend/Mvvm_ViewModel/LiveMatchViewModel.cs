using EFLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Autofac;
using EFLibrary.DataAcces;
using System.Linq;
using LeagueFriend.Command;
using Api.Processor;
using System.Threading.Tasks;

namespace LeagueFriend.Mvvm_ViewModel
{
   public class LiveMatchViewModel : BaseViewModel, ILiveMatchViewModel
    {
        public ICommand SearchCommand { get { return new CommandHandler(() => Search(), () => true); } }
        private ObservableCollection<Player> _PlayerListe;
        public ObservableCollection<Player> PlayerListe { get => _PlayerListe; set { _PlayerListe = value; Notify("PlayerListe"); } }
        private IComponentContext Context;
        private string _SearchTb;
        public string SearchTb { get => _SearchTb; set { _SearchTb = value; Notify("SearchTb"); } }
        
        public LiveMatchViewModel(IComponentContext context)
        {
            Context = context;
            var db = (DbCon)context.Resolve<IDbCon>();
            var list = db.Player.Where(x => x.SaveSearch == true).ToList();
            PlayerListe = new ObservableCollection<Player>(list);
        
       }

        public async Task Search()
        {
            var lolProc = Context.Resolve<ILolProcessor>();
            var player = await lolProc.FindAccountDetails(SearchTb);
            if (player != null)
            {
                var db = (DbCon)Context.Resolve<IDbCon>();
                db.Add(player);
                await db.SaveChangesAsync();
            }
        }

    }
}
