using MyToDoApp.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyToDoApp.ViewModels
{
    public class MemoViewModel : BindableBase
    {

        public MemoViewModel()
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            CreateToDoList();
            AddCommand = new DelegateCommand(Add);
        }

        private ObservableCollection<MemoDto> meMoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return meMoDtos; }
            set { meMoDtos = value; RaisePropertyChanged(); }
        }

        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }



        public DelegateCommand AddCommand { get; private set; }

        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        void CreateToDoList()
        {
            for (int i = 0; i < 20; i++)
            {
                MemoDtos.Add(new MemoDto()
                {
                    Title = $"标题{i}",
                    Content = "测试数据..."
                });
            }
        }
    }
}
