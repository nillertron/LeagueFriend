using Api.Processor;
using Autofac;
using EFLibrary.DataAcces;
using EFLibrary.Models;
using LeagueFriend.Command;
using LeagueFriend.Mvvm_View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace LeagueFriend.Mvvm_ViewModel
{
    public class MainPageViewModel : BaseViewModel, IMainPageViewModel
    {

        private IContainer Context;

        public ICommand PlayerPageCommand { get { return _ClickCommand = new CommandHandler(() => PlayerPage(), () => true); } }
        public ICommand ChampionPageCommand { get { return _ClickCommand = new CommandHandler(() => ChampionPage(), () => true); } }

        public MainPageViewModel()
        {
            Context = DependencyInjection.ContainerConfig.Configure();
            ChampionCheck();

        }
        public async Task ChampionCheck()
        {
            using (var scope = Context.BeginLifetimeScope())
            {
                var lolProc = scope.Resolve<ILolProcessor>();
                var db = (DbCon)scope.Resolve<IDbCon>();
                try
                {

#pragma warning disable CS4014
                    await Task.Run(async () =>
                    {
                        var apiList = await lolProc.GetAllChampions();
                        var dbList = db.Champion.AsNoTracking().OrderBy(o => o.Id).ToList();

                        var tempList = new List<Champion>();
                        apiList.ForEach(o =>
                        {
                            tempList.Add(new Champion { Id = Convert.ToInt32(o.Key), Name = o.Id });
                        });
                        tempList.ForEach(async x =>
                        {
                            if (!dbList.Any(y => y.Id == x.Id))
                                db.Champion.Add(x);
                        });
                        await db.SaveChangesAsync();

                    });
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

#pragma warning restore CS4014
            }


        }
        private void PlayerPage()
        {
            using (Context.BeginLifetimeScope())
            {
                CurrentPage = Context.Resolve<PlayerView>();

            }
        }
        private void ChampionPage()
        {
            using (Context.BeginLifetimeScope())
            {
                CurrentPage = Context.Resolve<ChampionView>();
            }
        }



    }
}
