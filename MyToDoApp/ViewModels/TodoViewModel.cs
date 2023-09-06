﻿using MyToDoApp.Common.Models;
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
    public class TodoViewModel :BindableBase
    {

        public TodoViewModel()
        {
            ToDoDtos =new ObservableCollection<ToDoDto> ();
            CreateToDoList();
            AddCommand = new DelegateCommand(Add);
        }

        

        private ObservableCollection<ToDoDto> toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
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
                ToDoDtos.Add(new ToDoDto() {
                    Title=$"标题{i}" ,
                    Content = "测试数据..."
                });
            }
        }
    }
}
