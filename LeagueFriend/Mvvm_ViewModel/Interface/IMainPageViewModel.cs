using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueFriend.Mvvm_ViewModel
{
    public interface IMainPageViewModel
    {
        ICommand ChampionPageCommand { get; }
        ICommand PlayerPageCommand { get; }

        Task ChampionCheck();
    }
}