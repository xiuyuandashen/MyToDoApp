using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDoApp.Common.Models;
using MyToDoApp.Service;
using Prism.Commands;
using Prism.Ioc;
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
    public class TodoViewModel : NavigationViewModel
    {

        public TodoViewModel(IToDoService service, IContainerProvider containerProvider) : base(containerProvider)
        {
            this.service = service;
            ToDoDtos = new ObservableCollection<ToDoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
            SelectCommand = new DelegateCommand<ToDoDto>(Selected);
        }



        private ObservableCollection<ToDoDto> toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }

        private bool isRightDrawerOpen;
        private readonly IToDoService service;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }


        private int selectIndex;

        public int SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; }
        }


        private string search;

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }



        private ToDoDto currentDto;

        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public ToDoDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }

        public DelegateCommand AddCommand { get; private set; }

        public DelegateCommand<ToDoDto> SelectCommand { get; private set; }


        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        private void Add()
        {
            // 清空
            CurrentDto = new ToDoDto();
            IsRightDrawerOpen = true;
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break;
                case "查询": GetDataAsync(); break;
                case "保存": Save(); break;
            }
        }

        private async void Save()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CurrentDto.Title) ||
                string.IsNullOrWhiteSpace(CurrentDto.Content))
                    return;

                UpdateLoading(true);
                if (currentDto.Id > 0)
                {
                    ApiResponse<ToDoDto> apiResponse = await service.UpdateAsync(currentDto);
                    if (apiResponse.Status)
                    {
                        var todo = ToDoDtos.FirstOrDefault(f => f.Id == currentDto.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentDto.Title;
                            todo.Content = CurrentDto.Content;
                            todo.Status = CurrentDto.Status;
                        }
                    }
                    IsRightDrawerOpen = false;
                }
                else
                {

                    ApiResponse<ToDoDto> apiResponse = await service.AddAsync(currentDto);
                    if (apiResponse.Status)
                    {
                        ToDoDtos.Add(apiResponse.Result);
                        isRightDrawerOpen = false;
                    }
                }

            }
            catch (Exception ex)
            {


            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private async void GetDataAsync()
        {

            UpdateLoading(true);
            int? status = SelectIndex == 0 ? null : (selectIndex == 1 ? 0 : 1);

            var apiResponse = await service.GetAllFilterAsync(new ToDoParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,
                Status = status
            });

            if (apiResponse.Status)
            {
                ToDoDtos.Clear();
                foreach (var item in apiResponse.Result.Items)
                {
                    ToDoDtos.Add(item);
                }
            }
            UpdateLoading(false);
        }

        private async void Selected(ToDoDto obj)
        {
            try
            {
                UpdateLoading(true);

                ApiResponse<ToDoDto> apiResponse = await service.GetFirstOfDefaultAsync(obj.Id);
                if (apiResponse.Status)
                {
                    CurrentDto = apiResponse.Result;
                    IsRightDrawerOpen = true;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {

                UpdateLoading(false);
            }
        }


        private async void Delete(ToDoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var deleteResult = await service.DeleteAsync(obj.Id);
                if (deleteResult.Status)
                {
                    var model = ToDoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (model != null)
                        ToDoDtos.Remove(model);
                }
            }
            finally
            {

                UpdateLoading(false);
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }

    }
}
