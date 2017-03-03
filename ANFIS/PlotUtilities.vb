Imports LinAlg
Imports OxyPlot
Imports OxyPlot.Axes
Imports OxyPlot.Series

Module PlotUtilities
    Function RotationMatrix(angle As Double) As Matrix
        Dim sin = Math.Sin(angle), cos = Math.Cos(angle)
        Return Matrix.Of(New Double(,) {{cos, -sin}, {sin, cos}})
    End Function

    Function Plot3D(fs As Func(Of Vector, Double)(), Optional ymin As Integer = -60, Optional ymax As Integer = 120,
                    Optional sampleCount As Integer = 24, Optional colors As Double(,) = Nothing) As PlotModel
        Dim model = New PlotModel()
        With model
            .Axes.Add(New LinearAxis)
            .Axes(0).Minimum = ymin
            .Axes(0).Maximum = ymax
            .DefaultColors = New List(Of OxyColor)()
            Dim c = If(colors, New Double(,) {{1.8, 3.0, 1.0}, {2.5, 1.5, 1}, {3, 1, 1}, {1, 2, 4}, {1, 2, 2}, {1.5, 2, 1}, {1, 4, 1}, {2, 1, 2}})
            For i = -20 To 20
                Dim t = 0.2 + 0.8 * (i + 20) / 40
                For j = 0 To fs.Length - 1
                    .DefaultColors.Add(OxyColor.FromArgb(255, CByte(255 - 255 * t / c(j, 0)), CByte(255 - 255 * t / c(j, 1)),
                                                         CByte(255 - 255 * t / c(j, 2))))
                Next
            Next
            Dim rot = RotationMatrix(DateTime.UtcNow.Ticks * 0.00000001)
            For i = 20 To -20 Step -1
                Dim x0 = i / 5.0
                For j = 0 To fs.Length - 1
                    Dim jj = j
                    .Series.Add(New FunctionSeries(Function(x) fs(jj)(rot.MatMul(Vector.Of(x0, x))), -5, 5, sampleCount) _
                                   With {.StrokeThickness = 6})
                Next
            Next
        End With
        Return model
    End Function
End Module
