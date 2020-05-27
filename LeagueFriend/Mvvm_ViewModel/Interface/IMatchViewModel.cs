using EFLibrary.Models;
using System.Collections.ObjectModel;

namespace LeagueFriend.Mvvm_ViewModel
{
    interface IMatchViewModel
    {
        ObservableCollection<Match> MatchList { get; set; }
    }
}