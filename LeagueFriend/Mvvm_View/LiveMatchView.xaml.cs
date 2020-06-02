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
namespace LeagueFriend.Mvvm_View
{
    /// <summary>
    /// Interaction logic for LiveMatchView.xaml
    /// </summary>
    public partial class LiveMatchView : UserControl
    {
        private ILiveMatchViewModel ViewModel;
        private IComponentContext Context;
        public LiveMatchView(IComponentContext context, ILiveMatchViewModel vm)
        {
            InitializeComponent();
            DataContext = ViewModel = vm;
            Context = context;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
