using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace LeagueFriend.Mvvm_ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IBaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string update)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(update));
        }
        protected ICommand _ClickCommand;
        private UserControl _CurrentPage;
        public UserControl CurrentPage { get => _CurrentPage; set { _CurrentPage = value; Notify("CurrentPage"); } }
    }
}
