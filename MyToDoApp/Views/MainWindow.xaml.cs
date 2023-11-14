using MaterialDesignThemes.Wpf;
using MyToDoApp.Extensions;
using Prism.Events;
using System.Windows;
using System.Windows.Input;

namespace MyToDoApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IEventAggregator aggregator)
        {
            InitializeComponent();
            //注册等待消息窗口
            aggregator.Resgiter(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;

                // 如果打开的话
                if (DialogHost.IsOpen)
                    // loading
                    DialogHost.DialogContent = new ProgressView();
            });
        }

        private bool isMaximized = false;
        private double restoreTop;
        private double restoreLeft;
        private double restoreWidth;
        private double restoreHeight;

        /// <summary>
        /// 窗口最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {


            if (isMaximized)
            {


                // 还原窗口的位置和大小
                this.Left = restoreLeft;
                this.Top = restoreTop;
                this.Width = restoreWidth;
                this.Height = restoreHeight;
                isMaximized = false;
                //this.WindowState = WindowState.Normal;
                return;
            }
            else
            {
               
                // 记录当前窗口的位置和大小，然后最大化窗口
                restoreLeft = this.Left;
                restoreTop = this.Top;
                restoreWidth = this.Width;
                restoreHeight = this.Height;
                // SystemParameters.WorkArea 获取windows工作区大小 以防覆盖windows任务栏
                this.Left = SystemParameters.WorkArea.Left;
                this.Top = SystemParameters.WorkArea.Top;
                this.Width = SystemParameters.WorkArea.Width;
                this.Height = SystemParameters.WorkArea.Height;
                isMaximized = true;
                //this.WindowState = WindowState.Maximized;
            }

        }

        /// <summary>
        /// 窗口最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 结束程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        //private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
        //    => ModifyTheme(DarkModeToggleButton.IsChecked == true);

        private static void ModifyTheme(bool isDarkTheme)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);

            paletteHelper.SetTheme(theme);
        }

        /// <summary>
        /// 当点击list时关闭侧边栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseDrawer(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TopMenuBar.IsLeftDrawerOpen = false;
        }


        /// <summary>
        /// 拖动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovingWindow(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        /// <summary>
        /// 当鼠标划入主题按钮时候改变鼠标样式为手指
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void themeButtonMouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// 当鼠标划出主题按钮时候改变鼠标样式为箭头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void themeButtonMouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;

        }
    }
}
