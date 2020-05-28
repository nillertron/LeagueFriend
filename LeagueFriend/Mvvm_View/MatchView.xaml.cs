using EFLibrary.Models;
using LeagueFriend.Mvvm_ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeagueFriend.Mvvm_View
{
    /// <summary>
    /// Interaction logic for MatchView.xaml
    /// </summary>
    public partial class MatchView : Window
    {
        private MatchViewModel ViewModel;
        public MatchView(List<Match> Liste, Player p)
        {
            InitializeComponent();
            DataContext = ViewModel = new MatchViewModel(Liste, p);
        }
    }
}
