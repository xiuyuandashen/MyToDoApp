using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoApp.Common.Models
{
    /// <summary>
    /// 待办事项
    /// </summary>
    public class ToDoDto : BaseDto
    {
        private string title;
        private string content;
        private int status;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        public string Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); }
        }

        public int Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged(); }
        }
    }
}
