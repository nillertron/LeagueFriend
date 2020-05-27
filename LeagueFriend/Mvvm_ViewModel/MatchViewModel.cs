using EFLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LeagueFriend.Mvvm_ViewModel
{
    class MatchViewModel:BaseViewModel
    {
        private ObservableCollection<Match> _MatchList;
        public ObservableCollection<Match> MatchList { get => _MatchList; set { _MatchList = value; Notify("MatchList"); } }
        public MatchViewModel(List<Match> matchList)
        {
            MatchList = new ObservableCollection<Match>(matchList);
        }
    }
}
