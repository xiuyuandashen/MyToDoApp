using MyToDo.Shared.Dtos;
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
            CreateTestData();
        }

        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; }
        }

        private ObservableCollection<ToDoDto> todoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return todoDtos; }
            set { todoDtos = value; }
        }

        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; }
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


        void CreateTestData()
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            for (int i = 0; i < 10; i++)
            {
                ToDoDtos.Add(new ToDoDto() { Title = "待办" + i, Content = "正在处理中..." });
                memoDtos.Add(new MemoDto() { Title = "备忘" + i, Content = "我的世界" });
            }
        }
    }
}
