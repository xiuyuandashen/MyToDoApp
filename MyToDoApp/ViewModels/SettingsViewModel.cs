using MyToDoApp.Common.Models;
using MyToDoApp.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyToDoApp.ViewModels
{
    public class SettingsViewModel : BindableBase
    {

        public SettingsViewModel(IRegionManager regionMannage)
        {
            RegionMannage = regionMannage;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
        }

        public IRegionManager RegionMannage { get;set; }

        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        // 导航
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }


        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;
            RegionMannage.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.NameSpace);
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar()
            {
                Title = "个性化",
                Icon = "Palette",
                NameSpace = "SkinView"
            });
            MenuBars.Add(new MenuBar()
            {
                Title = "系统设置",
                Icon = "Cog",
                NameSpace = ""
            });
            MenuBars.Add(new MenuBar()
            {
                Title = "关于更多",
                Icon = "AlertCircle",
                NameSpace = "AbortView"
            });
            
        }
    }
}
