using Autofac;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using Api.Processor;

namespace LeagueFriend.Mvvm_View
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        private PlayerViewModel ViewModel;
        private IComponentContext Context;
        public PlayerView(PlayerViewModel vm, IComponentContext context)
        {
            InitializeComponent();
            DataContext = ViewModel = vm;
            Context = context;
            
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Test");
        }

        private async void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = sender as ListViewItem;
            var player = obj.Content as Player;
            var matchList = new List<Match>();
            try
            {
                matchList = await ViewModel.GetMatchList(player);

            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            new MatchView(matchList).Show();


        }
    }
}
