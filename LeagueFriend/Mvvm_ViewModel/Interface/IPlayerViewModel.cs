using EFLibrary.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueFriend.Mvvm_ViewModel
{
    public interface IPlayerViewModel
    {
        ObservableCollection<Player> PlayerListe { get; set; }
        ICommand SearchCommand { get; }
        string SearchTb { get; set; }

        Task<List<Match>> GetMatchList(Player p);
    }
}