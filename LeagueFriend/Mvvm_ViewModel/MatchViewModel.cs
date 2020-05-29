using EFLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueFriend.Mvvm_ViewModel
{
    class MatchViewModel : BaseViewModel, IMatchViewModel
    {
        private ObservableCollection<Match> _MatchList = new ObservableCollection<Match>();
        public ObservableCollection<Match> MatchList { get => _MatchList; set { _MatchList = value; Notify("MatchList"); } }

        private int _WinCount;
        public int WinCount { get => _WinCount; set { _WinCount = value; Notify("WinCount"); } }
        private int _LossCount;
        public int LossCount { get => _LossCount; set { _LossCount = value; Notify("LossCount"); } }
        private double _PercentCount;
        public double PercentCount { get => _PercentCount; set { _PercentCount = value; Notify("PercentCount"); } }
        private Player _Player;
 
        public MatchViewModel(List<Match> matchList, Player p)
        {
            matchList = matchList.Where(o => o.Participants.Count > 0).ToList();
            MatchList = new ObservableCollection<Match>(matchList);

            _Player = p;
            InitCalc(matchList);



        }

        private async Task InitCalc(List<Match> matchList)
        {


            matchList.ForEach(o =>
        {
            o.Participants.ForEach(x =>
            {
                if (x.PlayerId == _Player.Id)
                {
                    if (x.Team.Win == "Win")
                    {
                        WinCount++;
                        o.Win = true;
                    }
                    else
                    {
                        LossCount++;
                        o.Win = false;
                    }

                }
            });
        });
            var totalGames = WinCount + LossCount;
            PercentCount = ((double)WinCount / (double)totalGames) * (double)100;

        }
    }
}
