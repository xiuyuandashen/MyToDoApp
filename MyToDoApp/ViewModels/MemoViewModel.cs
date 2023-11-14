using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using MyToDoApp.Service;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyToDoApp.ViewModels
{
    public class MemoViewModel : NavigationViewModel
    {

        public MemoViewModel(IMemoService service, IContainerProvider containerProvider) : base(containerProvider)
        {
            this.service = service;
            MemoDtos = new ObservableCollection<MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand<MemoDto>(Delete);
            SelectCommand = new DelegateCommand<MemoDto>(Selected);
        }



        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

        private bool isRightDrawerOpen;
        private readonly IMemoService service;

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



        private MemoDto currentDto;

        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public MemoDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }

        public DelegateCommand AddCommand { get; private set; }

        public DelegateCommand<MemoDto> SelectCommand { get; private set; }


        public DelegateCommand<MemoDto> DeleteCommand { get; private set; }

        public DelegateCommand<string> ExecuteCommand { get; private set; }

        private void Add()
        {
            // 清空
            CurrentDto = new MemoDto();
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
                    ApiResponse<MemoDto> apiResponse = await service.UpdateAsync(currentDto);
                    if (apiResponse.Status)
                    {
                        var todo = MemoDtos.FirstOrDefault(f => f.Id == currentDto.Id);
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

                    ApiResponse<MemoDto> apiResponse = await service.AddAsync(currentDto);
                    if (apiResponse.Status)
                    {
                        MemoDtos.Add(apiResponse.Result);
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

            var apiResponse = await service.GetAllFilterAsync(new QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,
            });

            if (apiResponse.Status)
            {
                MemoDtos.Clear();
                foreach (var item in apiResponse.Result.Items)
                {
                    MemoDtos.Add(item);
                }
            }
            UpdateLoading(false);
        }

        private async void Selected(MemoDto obj)
        {
            try
            {
                UpdateLoading(true);

                ApiResponse<MemoDto> apiResponse = await service.GetFirstOfDefaultAsync(obj.Id);
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


        private async void Delete(MemoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var deleteResult = await service.DeleteAsync(obj.Id);
                if (deleteResult.Status)
                {
                    var model = MemoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (model != null)
                        MemoDtos.Remove(model);
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
