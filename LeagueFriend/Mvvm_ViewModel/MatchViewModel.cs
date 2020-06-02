using EFLibrary.Models;
using LeagueFriend.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueFriend.Mvvm_ViewModel
{
    class MatchViewModel : BaseViewModel, IMatchViewModel
    {
        private ObservableCollection<Match> CachedMatchList = new ObservableCollection<Match>();

        private ObservableCollection<Match> _MatchList = new ObservableCollection<Match>();
        public ObservableCollection<Match> MatchList { get => _MatchList; set { _MatchList = value; Notify("MatchList"); } }

        private int _WinCount;
        public int WinCount { get => _WinCount; set { _WinCount = value; Notify("WinCount"); } }
        private int _LossCount;
        public int LossCount { get => _LossCount; set { _LossCount = value; Notify("LossCount"); } }
        private double _PercentCount;
        public double PercentCount { get => _PercentCount; set { _PercentCount = value; Notify("PercentCount"); } }

        private string _AvgKd;
        public string AvgKd { get => _AvgKd; set { _AvgKd = value; Notify("AvgKd"); } }
        private int _CurrentPage = 1;
        public int CurrentPage { get => _CurrentPage; set { _CurrentPage = value; Notify("CurrentPage"); } }
        private int MaxPage;

        private Player _Player;

        public ICommand NextPageCommand { get { return new CommandHandler(async () => await NextPage(), () => true); } }
        public ICommand PreviousPageCommand { get { return new CommandHandler(async () => await PreviousPage(), () => true); } }
        private Champion _FavChampion;
        public Champion FavChampion { get => _FavChampion; set { _FavChampion = value; Notify("FavChampion"); } }
        private Dictionary<Champion,int> FavoriteChampionDic = new Dictionary<Champion,int>();
        private string _FavoriteLane;
        public string FavoriteLane { get => _FavoriteLane; set { _FavoriteLane = value; Notify("FavoriteLane"); } }
        private Dictionary<string, int> FavoriteLaneDic = new Dictionary<string, int>();

        public MatchViewModel(List<Match> matchList, Player p)
        {
            matchList = matchList.Where(o => o.Participants.Count > 0).ToList();
            CachedMatchList = new ObservableCollection<Match>(matchList);
            for (int i = 0; i < 10; i++)
                MatchList.Add(CachedMatchList[i]);
            _Player = p;
            MaxPage = CachedMatchList.Count / 10;
            InitCalc(matchList);
            FindFavoriteChamp();
            FindFavoriteLane();

        }
        private async Task FindFavoriteChamp()
        {
            var tempMax = 0;
            foreach(var key in FavoriteChampionDic)
            {
                if(key.Value > tempMax)
                {
                    FavChampion = key.Key;
                    tempMax = key.Value;
                }
            }
        }
        private async Task FindFavoriteLane()
        {
            var tempMax = 0;
            foreach (var key in FavoriteLaneDic)
            {
                if (key.Value > tempMax)
                {
                    FavoriteLane = key.Key;
                    tempMax = key.Value;
                }
            }
        }
        private async Task NextPage()
        {
            if(CurrentPage < MaxPage)
            {
                CurrentPage++;
                var endPoint = CurrentPage * 10;
                var startPoint = endPoint - 10;
                await Load10ListElements(startPoint, endPoint);
            }
        }
        private async Task PreviousPage()
        {
            if(CurrentPage >1)
            {
                CurrentPage--;
                var endPoint = CurrentPage * 10;
                var startPoint = endPoint - 10;
                await Load10ListElements(startPoint, endPoint);
            }
        }
        private async Task Load10ListElements(int startPoint, int endPoint)
        {
            MatchList.Clear();
            for (int i = startPoint; i < endPoint; i++)
                MatchList.Add(CachedMatchList[i]);
        }
        private async Task InitCalc(List<Match> matchList)
        {

            var kills = 0;
            var deaths = 0;
            var assist = 0;
            var count = 0;
            
            matchList.ForEach(o =>
             {
                 o.Participants.ForEach(x =>
                 {
                     if (x.PlayerId == _Player.Id)
                     {
                         kills += x.Stats.Kills;
                         deaths += x.Stats.Deaths;
                         assist += x.Stats.Assists;
                         count++;
                         o.Assist = x.Stats.Assists;
                         o.Kills = x.Stats.Kills;
                         o.Deaths = x.Stats.Deaths;
                         o.ItemImage0 = x.Stats.Item0Image;
                         o.ItemImage1 = x.Stats.Item1Image;
                         o.ItemImage2 = x.Stats.Item2Image;
                         o.ItemImage3 = x.Stats.Item3Image;
                         o.ItemImage4 = x.Stats.Item4Image;
                         o.ItemImage5 = x.Stats.Item5Image;
                         o.ItemImage6 = x.Stats.Item6Image;
                         o.GoldEarned = x.Stats.GoldEarned;
                         o.TotalMinionsKilled = x.Stats.TotalMinionsKilled;
                         var champ = FavoriteChampionDic.Where(s => s.Key.Id == o.Champion.Id).FirstOrDefault();
                         if (champ.Key == null)
                             FavoriteChampionDic.Add(o.Champion, 1);
                         else
                         {
                             var occourences = champ.Value;
                             occourences++;
                             FavoriteChampionDic.Remove(champ.Key);
                             FavoriteChampionDic.Add(champ.Key, occourences);
                         }

                         var lane = FavoriteLaneDic.Where(i => i.Key == o.Lane).FirstOrDefault();
                         if(lane.Key == null || lane.Key == string.Empty)
                         {
                             FavoriteLaneDic.Add(o.Lane, 1);
                         }
                         else
                         {
                             var occurences = lane.Value;
                             occurences++;
                             FavoriteLaneDic.Remove(lane.Key);
                             FavoriteLaneDic.Add(lane.Key, occurences);
                         }
                            
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
            PercentCount = Math.Round(PercentCount, 2);
            AvgKd = $"{kills / count} / {deaths / count} / {assist / count} ";
        }
    }
}
