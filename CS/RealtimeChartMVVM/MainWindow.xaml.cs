using DevExpress.Xpf.Charts;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace RealtimeChartMvvm {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void chart_BoundDataChanged(object sender, RoutedEventArgs e) {
            // Adjust the visual range.
            AxisX2D axisX = ((XYDiagram2D)chart.Diagram).ActualAxisX;
            DateTime maxRangeValue = (DateTime)axisX.ActualWholeRange.ActualMaxValue;
            axisX.ActualVisualRange.SetMinMaxValues(maxRangeValue.AddSeconds(-10), maxRangeValue);
        }
    }
    public class ChartViewModel {
        const int MaxPointCount = 3000;
        readonly DispatcherTimer timer = new DispatcherTimer();
        public ObservableCollection<DataPoint> DataPoints { get; } = new ObservableCollection<DataPoint>();
        public ChartViewModel() {
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();
        }
        private void Timer_Tick(object sender, System.EventArgs e) {
            DataPoints.Add(new DataPoint(DateTime.Now, GenerateValue(DataPoints.Count)));
            if (DataPoints.Count == MaxPointCount) { timer.Stop(); }
        }
        private double GenerateValue(double x) {
            return Math.Sin(x) * 3 + x / 2 + 5;
        }
    }
    public class DataPoint {
        public DateTime Argument { get; set; }
        public double Value { get; set; }
        public DataPoint(DateTime argument, double value) {
            this.Argument = argument;
            this.Value = value;
        }
    }
}
