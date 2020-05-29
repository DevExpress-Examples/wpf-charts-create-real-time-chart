Imports DevExpress.Xpf.Charts
Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Threading

Namespace RealtimeChartMvvm
	Partial Public Class MainWindow
		Inherits Window

		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub chart_BoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
			' Adjust the visual range.
			Dim axisX As AxisX2D = CType(chart.Diagram, XYDiagram2D).ActualAxisX
			Dim maxRangeValue As DateTime = CDate(axisX.ActualWholeRange.ActualMaxValue)
			axisX.ActualVisualRange.SetMinMaxValues(maxRangeValue.AddSeconds(-10), maxRangeValue)
		End Sub
	End Class
	Public Class ChartViewModel
		Private Const MaxPointCount As Integer = 3000
		Private ReadOnly timer As New DispatcherTimer()
		Public ReadOnly Property DataPoints() As New ObservableCollection(Of DataPoint)()
		Public Sub New()
			AddHandler timer.Tick, AddressOf Timer_Tick
			timer.Interval = TimeSpan.FromMilliseconds(100)
			timer.Start()
		End Sub
		Private Sub Timer_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
			DataPoints.Add(New DataPoint(DateTime.Now, GenerateValue(DataPoints.Count)))
			If DataPoints.Count = MaxPointCount Then
				timer.Stop()
			End If
		End Sub
		Private Function GenerateValue(ByVal x As Double) As Double
			Return Math.Sin(x) * 3 + x / 2 + 5
		End Function
	End Class
	Public Class DataPoint
		Public Property Argument() As DateTime
		Public Property Value() As Double
'INSTANT VB NOTE: The variable argument was renamed since Visual Basic does not handle local variables named the same as class members well:
'INSTANT VB NOTE: The variable value was renamed since Visual Basic does not handle local variables named the same as class members well:
		Public Sub New(ByVal argument_Conflict As DateTime, ByVal value_Conflict As Double)
			Me.Argument = argument_Conflict
			Me.Value = value_Conflict
		End Sub
	End Class
End Namespace
