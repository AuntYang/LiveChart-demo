using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace LiveChart_demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }//折线图
        public List<string> Labels { get; set; }//横坐标
        private double _trend;
        private double[] temp = { 1, 3, 2, 4, -3, 5, 2, 1 };
        public MainWindow()
        {
            InitializeComponent();
            LineSeries mylineseries = new LineSeries();//实例化一条折线图
            mylineseries.Title = "Temp";//设置折线的标题
            mylineseries.LineSmoothness = 0;//折线图直线形式
            mylineseries.PointGeometry = null;//折线图的无点样式
            Labels = new List<string> { "1", "3", "2", "4", "-3", "5", "2", "1" };//添加横坐标,初始横坐标   
            mylineseries.Values = new ChartValues<double>(temp);//添加纵坐标数据，初始纵坐标
            SeriesCollection = new SeriesCollection { };
            SeriesCollection.Add(mylineseries);
            //_trend = 8;
            linestart();
            DataContext = this;
        }
        //连续折线图的方法
        public void linestart()
        {
            Task.Run(() =>
            {
                var r = new Random();//随机函数
                while (true)
                {
                    Thread.Sleep(1000);//刷新间隔时间
                    _trend = r.Next(-10, 10);//随机数产生y值
                    //通过Dispatcher在工作线程中更新窗体的UI元素
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        //更新横坐标时间
                        Labels.Add(DateTime.Now.ToString());//添加横坐标时间数据值
                        Labels.RemoveAt(0);
                        //更新纵坐标数据
                        SeriesCollection[0].Values.Add(_trend);//添加y值，_trend为数据
                        SeriesCollection[0].Values.RemoveAt(0);
                    });
                }
            });
        }
       
     

     }

    
}


