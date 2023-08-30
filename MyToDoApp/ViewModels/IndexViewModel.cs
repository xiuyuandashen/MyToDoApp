using MyToDoApp.Common.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoApp.ViewModels
{
    public class IndexViewModel : BindableBase
    {

        public IndexViewModel()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskBars();
        }

        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; } 
        }

        void CreateTaskBars()
        {
            TaskBars.Add(new TaskBar()
            {
                Icon = "ClockFast",
                Title = "汇总",
                Content = "9",
                Color = "#0096fc",
                Target = ""
            });
            TaskBars.Add(new TaskBar()
            {
                Icon = "ClockCheckOutline",
                Title = "已完成",
                Content = "9",
                Color = "#0fb13c",
                Target = ""
            });
            TaskBars.Add(new TaskBar()
            {
                Icon = "ChartLineVariant",
                Title = "完成比例",
                Content = "100%",
                Color = "#00b3db",
                Target = ""
            });
            TaskBars.Add(new TaskBar()
            {
                Icon = "PlaylistStar",
                Title = "备忘录",
                Content = "19",
                Color = "#ffa000",
                Target = ""
            });
        }

    }
}
