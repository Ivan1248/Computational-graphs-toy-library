Imports System.Text
Imports OxyPlot
Imports OxyPlot.Axes
Imports OxyPlot.Series
Imports ComputationalGraphs, LinAlg

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RuleCountComboBox.SelectedIndex = 3
        BatchSizeComboBox.SelectedIndex = 0
        CreateSystem()
        RefreshPlot()
        For i = -4 To 4
            For j = -4 To 4
                Dim x = Vector.Of(i, j)
                DataX.Add(x)
                DataY.Add(Vector.Of(F(x(0), x(1))))
            Next
        Next
        RuleCountComboBox_SelectedIndexChanged(Nothing, Nothing)
        ' OutputPlot.Model.Series.Add(New FunctionSeries(Function(x) f(x) + 1, -4, 4, 0.1, "f(x)"))
    End Sub

    Sub RefreshPlot()
        For Each plotView In {ARulesPlot, BRulesPlot, ConclusionsPlot, FsPlot}
            plotView.Model = New PlotModel()
            plotView.Model.Axes.Add(New LinearAxis)
            plotView.Model.Axes(0).Minimum = 0
            plotView.Model.Axes(0).Maximum = 1.8
        Next
        For i = 0 To RuleCount - 1
            Dim ii = i
            ARulesPlot.Model.Series.Add(New FunctionSeries(Function(x) ARules(ii, x), -20, 20, 80, "A" & ii))
            BRulesPlot.Model.Series.Add(New FunctionSeries(Function(x) BRules(ii, x), -20, 20, 80, "B" & ii))
        Next
        Dim concfs As New List(Of Func(Of Vector, Double))
        For i = 0 To RuleCount - 1
            Dim ii = i
            concfs.Add(Function(x) Conclusions(ii, x))
        Next
        ConclusionsPlot.Model = Plot3D(concfs.ToArray(), sampleCount:=12)
        OutputPlot.Model = Plot3D(New Func(Of Vector, Double)() {Function(x As Vector) anfis.Predict(x)(0), AddressOf F},
            colors:=New Double(,) {{1.8, 3.0, 1.0}, {1, 2, 4}})
    End Sub

    Function F(x As Double, y As Double) As Double
        Return (Math.Pow(x - 1, 2) + Math.Pow(y + 2, 2) - 5 * x * y + 3) * Math.Pow(Math.Cos(x / 5), 2)
    End Function

    Function F(x As Vector) As Double
        Return F(x(0), x(1))
    End Function

    Private DataX As New List(Of Vector), DataY As New List(Of Vector)

    Private anfis As GraphicalLearningMachine
    Private inputNode As InputNode
    Private antecedentPartNodes As Node()
    Private antecedentsNode As Node
    Private nAntecedentsNode As Node
    Private consequentsNode As Node
    Private conclusionsNode As Node
    Private outputNode As Node
    Function ARules(i As Integer, x As Double) As Double
        inputNode.Set(Vector.Of(x, 0))
        Return antecedentPartNodes(0).Output(i)
    End Function
    Function BRules(i As Integer, x As Double) As Double
        inputNode.Set(Vector.Of(0, x))
        Return antecedentPartNodes(1).Output(i)
    End Function

    Private CompletedEpochCount As Integer = 0

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        anfis.ReinitializeParameters()
        CompletedEpochCount = 0
        RefreshPlot()
    End Sub

    Private Property Training As Boolean
        Get
            Return _training
        End Get
        Set
            _training = Value
            TrainButton.Text = If(Value, "Stop", "Train")
        End Set
    End Property
    Private _training As Boolean = False

    Private Sub TrainButton_Click(sender As Object, e As EventArgs) Handles TrainButton.Click
        Training = Not Training
        If Not Training Then Return
        CreateSystem()
        Dim t As Long = DateTime.Now.Ticks

        'Dim errorData As New List(Of Double)

        While Training
            anfis.TrainEpoch(DataX, DataY)
            CompletedEpochCount += 1

            ' errorData.Add(anfis.GetError(DataX, DataY))
            ' If CompletedEpochCount = 10000 Then
            ' Exit While
            ' End If

            Dim t1 As Long = DateTime.Now.Ticks
            If t1 - t < 2000000 Then
                Continue While
            End If
            t = t1
            RefreshPlot()
            ErrorLabel.Text = $"E = {anfis.GetError(DataX, DataY):0.0000e+00}"
            EpochCountLabel.Text = $"Epochs: {CompletedEpochCount}"
            For Each plotView In {ARulesPlot, BRulesPlot, ConclusionsPlot, FsPlot}
                plotView.InvalidatePlot(True)
            Next
            Application.DoEvents()
        End While
        'Clipboard.SetText(String.Join(vbNewLine, (From x In errorData Select $"{x}")))
        Training = False
    End Sub

    Function Conclusions(i As Integer, x As Vector) As Double
        inputNode.Set(x)
        Return conclusionsNode.Output(i)
    End Function

    Private Sub PredictionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PredictionToolStripMenuItem.Click
        Clipboard.SetText(Plot3DAsCSV(Function(x) anfis.Predict(x)(0), "h", -4, 4, -4, 4))
    End Sub

    Private Sub OriginalFunctionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OriginalFunctionToolStripMenuItem.Click
        Clipboard.SetText(Plot3DAsCSV(AddressOf F, "f", -4, 4, -4, 4))
    End Sub

    Private Sub ErrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ErrorToolStripMenuItem.Click
        Clipboard.SetText(Plot3DAsCSV(Function(x) anfis.Predict(x)(0) - F(x), "e", -4, 4, -4, 4))
    End Sub

    Private Sub ConclusionToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim i = Integer.Parse(sender.ToString())
        Clipboard.SetText(Plot3DAsCSV(Function(x)
                                          anfis.Predict(x)
                                          Return conclusionsNode.Output(i)
                                      End Function, $"""f{i}""", -4, 4, -4, 4))
    End Sub

    Private Sub AntecedentsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim i = Integer.Parse(sender.ToString())
        Clipboard.SetText(Plot3DAsCSV(Function(x)
                                          anfis.Predict(x)
                                          Return nAntecedentsNode.Output(i)
                                      End Function, $"""a{i}""", -4, 4, -4, 4))
    End Sub

    Private Sub RuleCountComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RuleCountComboBox.SelectedIndexChanged
        For Each item As ToolStripMenuItem In ConclusionsToolStripMenuItem.DropDownItems
            RemoveHandler item.Click, AddressOf ConclusionToolStripMenuItem_Click
            RemoveHandler item.Click, AddressOf AntecedentsToolStripMenuItem_Click
        Next
        ConclusionsToolStripMenuItem.DropDownItems.Clear()
        AntecedentsToolStripMenuItem.DropDownItems.Clear()
        For i = 0 To RuleCount - 1
            Dim item = New ToolStripMenuItem(i.ToString())
            ConclusionsToolStripMenuItem.DropDownItems.Add(item)
            AddHandler item.Click, AddressOf ConclusionToolStripMenuItem_Click

            item = New ToolStripMenuItem(i.ToString())
            AntecedentsToolStripMenuItem.DropDownItems.Add(item)
            AddHandler item.Click, AddressOf AntecedentsToolStripMenuItem_Click
        Next
    End Sub

    Private Function Plot3DAsCSV(f As Func(Of Vector, Double), fname As String, xmin As Double, xmax As Double, ymin As Double, ymax As Double) As String
        Dim sb As New StringBuilder()
        sb.AppendLine($"x, y, {fname}")
        For x As Double = xmin To xmax Step (xmax - xmin) / 20
            For y As Double = ymin To ymax Step (xmax - xmin) / 20
                sb.AppendLine($"{x}, {y}, {f(Vector.Of(x, y))}")
            Next
        Next
        Return sb.ToString()
    End Function

#Region "Neuronska mreža"
    Private ReadOnly Property RuleCount As Integer
        Get
            Return CInt(RuleCountComboBox.Text)
        End Get
    End Property
    Private ReadOnly Property BatchSize As Integer
        Get
            Return CInt(BatchSizeComboBox.Text)
        End Get
    End Property

    Private LearningRate As Double

    Private Sub LearningRateTextbox_TextChanged(sender As Object, e As EventArgs) Handles LearningRateTextbox.TextChanged
        Dim lr As Double
        If Double.TryParse(LearningRateTextbox.Text, lr) Then LearningRate = lr
    End Sub

    Private inputDimension As Integer = 2

    Private Sub CreateSystem()
        inputNode = Nodes.Input(inputDimension)
        antecedentPartNodes = New Node(inputDimension) {}
        For i = 0 To inputDimension - 1
            Dim inp = Enumerable.Repeat(inputNode.Slice(i, i + 1), RuleCount).ToArray()
            antecedentPartNodes(i) = Nodes.Concat(inp).Add().Mul().Logistic()
        Next
        antecedentsNode = (antecedentPartNodes(0) * antecedentPartNodes(1)).BackpropAmplifier(1)  ' treba promijentiti za inputDimension <> 2
        nAntecedentsNode = antecedentsNode / antecedentsNode.ReduceSum()
        consequentsNode = inputNode.WeightedSums(RuleCount).Add()
        conclusionsNode = nAntecedentsNode * consequentsNode
        outputNode = conclusionsNode.ReduceSum()
        Dim labelNode = Nodes.Input(1)
        Dim trainer = New Trainer(
            inputNode:=inputNode, labelNode:=labelNode, lossNode:=outputNode.SquaredLoss(labelNode),
            optimizer:=New GradientDescentOptimizer(learningRate:=LearningRate),
            batchSize:=BatchSize)
        anfis = New GraphicalLearningMachine(trainer)
    End Sub
#End Region

End Class

