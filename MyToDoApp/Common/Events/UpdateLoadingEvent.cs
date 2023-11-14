using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoApp.Common.Events
{
    /// <summary>
    /// 更新加载模型
    /// </summary>
    public class UpdateModel
    {
        public bool IsOpen { get; set; }
    }

    /// <summary>
    /// 更新加载滚动条事件
    /// </summary>
    public class UpdateLoadingEvent:PubSubEvent<UpdateModel>
    {
    }
}
