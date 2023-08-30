using MyToDoApp.Common.Models;
using MyToDoApp.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace MyToDoApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        // 导航
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        // 上一步
        public DelegateCommand GetBackCommand { get; set; }
        // 下一步
        public DelegateCommand GetForWardCommand { get; set; }
        
        public IRegionManager RegionManager { get; }

        // 导航日志
        private IRegionNavigationJournal journal;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            GetBackCommand = new DelegateCommand(Back);
            GetForWardCommand = new DelegateCommand(Forward);
            CreateMenuBar();
            RegionManager = regionManager;
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;
            RegionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                // 如果导航成功
                if ((bool)back.Result)
                {
                    // 给导航日志赋值
                    journal = back.Context.NavigationService.Journal;
                }
            });
        }

        /// <summary>
        /// 返回上一步
        /// </summary>
        private void Back()
        {
            if (journal == null) return;
            // 允许返回上一步
            if (journal.CanGoBack)
            {
                journal.GoBack();
            }
        }

        /// <summary>
        /// 返回下一步
        /// </summary>
        private void Forward()
        {
            if (journal == null) return;
            // 允许返回下一步
            if (journal.CanGoForward)
            {
                journal.GoForward();
            }
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar()
            {
                Title = "主页",
                Icon = "Home",
                NameSpace = "IndexView"
            });
            MenuBars.Add(new MenuBar()
            {
                Title = "代办事项",
                Icon = "NotebookPlusOutline",
                NameSpace = "TodoView"
            });
            MenuBars.Add(new MenuBar()
            {
                Title = "备忘录",
                Icon = "NotebookPlus",
                NameSpace = "MemoView"
            });
            MenuBars.Add(new MenuBar()
            {
                Title = "设置",
                Icon = "Cog",
                NameSpace = "SettingsView"
            });
        }
    }
}
