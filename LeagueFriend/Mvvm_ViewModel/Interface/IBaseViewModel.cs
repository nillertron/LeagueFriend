using System.ComponentModel;
using System.Windows.Controls;

namespace LeagueFriend.Mvvm_ViewModel
{
    public interface IBaseViewModel
    {
        UserControl CurrentPage { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void Notify(string update);
    }
}