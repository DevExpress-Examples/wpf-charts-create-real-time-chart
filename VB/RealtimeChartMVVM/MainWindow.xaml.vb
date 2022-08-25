Imports DevExpress.Xpf.Charts
Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Threading

Namespace RealtimeChartMvvm

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub chart_BoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Adjust the visual range.
            Dim axisX As AxisX2D = CType(Me.chart.Diagram, XYDiagram2D).ActualAxisX
            Dim maxRangeValue As Date = CDate(axisX.ActualWholeRange.ActualMaxValue)
            axisX.ActualVisualRange.SetMinMaxValues(maxRangeValue.AddSeconds(-10), maxRangeValue)
        End Sub
    End Class

    Public Class ChartViewModel

        Const MaxPointCount As Integer = 3000

        Private ReadOnly timer As DispatcherTimer = New DispatcherTimer()

        Public ReadOnly Property DataPoints As ObservableCollection(Of DataPoint) = New ObservableCollection(Of DataPoint)()

        Public Sub New()
            AddHandler timer.Tick, AddressOf Timer_Tick
            timer.Interval = TimeSpan.FromMilliseconds(100)
            timer.Start()
        End Sub

        Private Sub Timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            DataPoints.Add(New DataPoint(Date.Now, GenerateValue(DataPoints.Count)))
            If DataPoints.Count = MaxPointCount Then
                timer.Stop()
            End If
        End Sub

        Private Function GenerateValue(ByVal x As Double) As Double
            Return Math.Sin(x) * 3 + x / 2 + 5
        End Function
    End Class

    Public Class DataPoint

        Public Property Argument As Date

        Public Property Value As Double

        Public Sub New(ByVal argument As Date, ByVal value As Double)
            Me.Argument = argument
            Me.Value = value
        End Sub
    End Class
End Namespace
