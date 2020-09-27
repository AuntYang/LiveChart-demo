using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace LiveChart_demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
     // 数据点格式
    public class MeasureModel
    {
        public int Index { get; set; }
        public float Value { get; set; }
    }

    public partial class ConstantChangesChart : UserControl
    {
        private int _index;
        public ChartValues<MeasureModel> PhaseChartValues { get; set; }
        public ChartValues<MeasureModel> ModulusChartValues { get; set; }
        public float PhaseValue { get; set; }
        public float ModulusValue { get; set; }

        // 当数值被更改时，触发更新
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                Read();
            }
        }

        public ConstantChangesChart()
        {
            

            // 设置图表的XY和数值对应
            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.Index)
                .Y(model => model.Value);
            Charting.For<MeasureModel>(mapper);
            PhaseChartValues = new ChartValues<MeasureModel>();
            ModulusChartValues = new ChartValues<MeasureModel>();

            DataContext = this;
        }

        // 更新图表
        private void Read()
        {
            PhaseChartValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.PhaseValue
            });
            ModulusChartValues.Add(new MeasureModel
            {
                Index = this.Index,
                Value = this.ModulusValue
            });

            // 限定图表最多只有十五个元素
            if (PhaseChartValues.Count > 15)
            {
                PhaseChartValues.RemoveAt(0);
                ModulusChartValues.RemoveAt(0);
            }
        }
    }
    public partial class MainWindow : Window
    {
        private int index = 0;
        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(RecordData);
        }
        private void RecordData()
        {
            // 持续生成随机数，模拟数据处理过程
            while (true)
            {
                Thread.Sleep(500);
                var r = new Random();
                float phase = r.Next(1, 7);
                float modulus = r.Next(1, 10);
                // 更新图表数据
                constantChangesChart.PhaseValue = phase;
                constantChangesChart.ModulusValue = modulus;
                constantChangesChart.Index = index++;
            }
        }

    }

    
}


