Imports System.IO
Imports ComputationalGraphs
Imports LinAlg

Public Class Form1
    Private ann As GraphicalLearningMachine

    Private ReadOnly rawData As New List(Of List(Of List(Of Vector)))

    Private ReadOnly Property ClassCount As Integer
        Get
            Return rawData.Count
        End Get
    End Property

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

#Region "Dodavanje, uklanjanje i odabir klase (popis, dodavanje, uklanjanje)"

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim item As New ListViewItem() With {.Text = ClassCount.ToString()}
        item.SubItems.Add(0.ToString())
        ListViewClasses.Items.Add(item)
        rawData.Add(New List(Of List(Of Vector))())
        item.Checked = True
    End Sub

    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        If ClassCount = 0 Then Return
        Dim i = ListViewClasses.Items.IndexOf(ListViewClasses.CheckedItems(0))
        rawData.RemoveAt(i)
        ListViewClasses.Items.RemoveAt(i)
        If ClassCount = 0 Then Return
        ListViewClasses.Items(ClassCount - 1).Checked = True
    End Sub

    Private Sub ListViewClasses_ItemChecked(sender As Object, e As ItemCheckedEventArgs) _
        Handles ListViewClasses.ItemChecked
        RemoveHandler ListViewClasses.ItemChecked, AddressOf ListViewClasses_ItemChecked
        If ListViewClasses.CheckedItems.Count <> 1 Then
            For Each item As ListViewItem In ListViewClasses.CheckedItems
                item.Checked = False
            Next
            e.Item.Checked = True
        End If
        AddHandler ListViewClasses.ItemChecked, AddressOf ListViewClasses_ItemChecked
    End Sub

#End Region

#Region "Snimanje gesti"

    Private Function PointGestureToVectorGesture(gesture As List(Of Point)) As List(Of Vector)
        Return gesture.Select(Function(p) Vector.Of(New Double() {p.X, p.Y})).ToList()
    End Function

    Private Sub GesturePanel1_GestureDrawn_Collection(sender As Object, e As GestureEventArgs) _
        Handles GesturePanel1.GestureDrawn
        If ClassCount = 0 Then Return
        Dim item = ListViewClasses.CheckedItems(0)
        Dim i = ListViewClasses.Items.IndexOf(item)
        rawData(i).Add(PointGestureToVectorGesture(e.gesture))
        item.SubItems(1).Text = rawData(i).Count.ToString()
    End Sub

#End Region

#Region "Prepoznavanje gesti"

    Private Sub GesturePanel1_GestureDrawn_Recognition(sender As Object, e As GestureEventArgs)
        If ann Is Nothing Then Return
        Dim gest = GestureToVector(PointGestureToVectorGesture(e.gesture))
        Dim h = ann.Predict(gest)
        Dim c = 0
        For i = 1 To h.Dimension - 1
            If h(i) > h(c) Then c = i
        Next
        Label1.Text = h.DivBy(h.Sum()).ToString() & vbNewLine & "(" & c.ToString() & ")"
        Using g = TabPageRecognition.CreateGraphics()
            g.Clear(TabPageRecognition.BackColor)
            Dim w = TabPageRecognition.Width
            Dim points = From p In Normalize(rawData(c)(0))
                         Select New PointF(CSng(w / 4 + (p(0) + 1) * w / 4), CSng(w / 4 + (p(1) + 1) * w / 4))
            g.DrawCurve(GesturePanel1.Pen, points.ToArray())
        End Using
    End Sub

#End Region

#Region "Učitavanje i spremanje gesti"

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Using file = SaveFileDialog1.OpenFile()
                Dim sw = New StreamWriter(file)
                For i = 0 To rawData.Count - 1
                    sw.WriteLine("c")
                    For Each gest As List(Of Vector) In rawData(i)
                        sw.WriteLine("g")
                        For Each p In gest
                            sw.Write("v ")
                            sw.Write(CType(p(0) + 0.5, Integer).ToString() & " ")
                            sw.WriteLine(CType(p(1) + 0.5, Integer).ToString())
                        Next
                    Next
                Next
                sw.Flush()
            End Using
        End If
    End Sub

    Private Sub ButtonLoad_Click(sender As Object, e As EventArgs) Handles ButtonLoad.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Using file = OpenFileDialog1.OpenFile()
                Dim sr = New StreamReader(file)
                rawData.Clear()
                Dim cl As List(Of List(Of Vector)) = Nothing
                Dim g As List(Of Vector) = Nothing
                While True
                    Dim line = sr.ReadLine()
                    If line Is Nothing Then Exit While
                    Select Case line(0)
                        Case "c"c
                            cl = New List(Of List(Of Vector))()
                            rawData.Add(cl)
                        Case "g"c
                            g = New List(Of Vector)()
                            cl.Add(g)
                        Case "v"c
                            Dim p = line.Substring(2).Split(" "c)
                            g.Add(Vector.Of({Double.Parse(p(0)), Double.Parse(p(1))}))
                    End Select
                End While
            End Using
            ListViewClasses.Items.Clear()
            For i = 0 To rawData.Count - 1
                ListViewClasses.Items.Add(i.ToString())
                ListViewClasses.Items(i).SubItems.Add(rawData(i).Count.ToString())
            Next
            ListViewClasses.Items(rawData.Count - 1).Selected = True
        End If
    End Sub

    Private Sub ButtonUndoLastInClass_Click(sender As Object, e As EventArgs) Handles ButtonUndoLastInClass.Click
        Dim i = ListViewClasses.CheckedIndices(0)
        If rawData(i).Count > 0 Then
            rawData(i).RemoveAt(rawData(i).Count - 1)
            ListViewClasses.Items(i).SubItems(1).Text = rawData(i).Count.ToString()
        End If
    End Sub

#End Region

#Region "Predobrada gesti i učenje"

    Private Const SampleCount = 20
    Private X As List(Of Vector), Y As List(Of Vector)

    Private Function Normalize(gesture As List(Of Vector)) As List(Of Vector)
        Dim xmin = gesture(0)(0),
            xmax = gesture(0)(0),
            ymin = gesture(0)(1),
            ymax = gesture(0)(1)
        For Each p As Vector In gesture
            If p(0) < xmin Then : xmin = p(0)
            ElseIf p(0) > xmax Then : xmax = p(0)
            End If
            If p(1) < ymin Then : ymin = p(1)
            ElseIf p(1) > ymax Then : ymax = p(1)
            End If
        Next
        Dim scale = Math.Max(xmax - xmin, ymax - ymin) / 2
        Dim c = Vector.Of((xmax + xmin) / 2, (ymax + ymin) / 2)
        Dim nGesture As New List(Of Vector)(gesture.Count)
        For Each p As Vector In gesture
            nGesture.Add((p - c).DivBy(scale))
        Next
        Return nGesture
    End Function

    Private Function GestureToVector(gesture As List(Of Vector)) As Vector
        gesture = Normalize(gesture)
        Dim distances(gesture.Count - 1) As Double
        Dim length = 0.0
        Dim prev = gesture(0)
        For i = 1 To gesture.Count - 1
            distances(i - 1) = length
            length += gesture(i).Distance(prev)
            prev = gesture(i)
        Next
        distances(gesture.Count - 1) = length

        Dim result = Vector.Zeros(SampleCount * 2)
        result(0) = gesture(0)(0)
        result(1) = gesture(0)(1)
        Dim j = 0
        For i = 1 To SampleCount - 1
            Dim snext = i * length / (SampleCount - 1)
            While j < distances.Length - 1 AndAlso distances(j) < snext : j += 1 : End While
            Dim t = (snext - distances(j - 1)) / (distances(j) - distances(j - 1))
            If Double.IsInfinity(t) Then t = 1
            Dim interpolated = gesture(j - 1) * (1 - t) + gesture(j) * t
            result(i * 2) = interpolated(0)
            result(i * 2 + 1) = interpolated(1)
        Next
        Return result
    End Function

    Private Training As Boolean = False

    Private Sub ButtonTrain_Click(sender As Object, e As EventArgs) Handles ButtonTrain.Click
        If Training Then
            Training = False
            ButtonTrain.Text = "Nauči mrežu"
            Return
        Else
            ButtonTrain.Text = "Zaustavi učenje"
        End If
        X = New List(Of Vector)()
        Y = New List(Of Vector)()
        For i = 0 To ClassCount - 1
            Dim c As List(Of List(Of Vector)) = rawData(i)
            For Each gesture As List(Of Vector) In c
                X.Add(GestureToVector(gesture))
                Dim label = Vector.Zeros(ClassCount)
                label(i) = 1
                Y.Add(label)
            Next
        Next
        CreateAppropriateNeuralNetwork()

        Training = True
        ToolStripStatusLabelError.Text = "E = " & ann.GetError(X, Y).ToString("0.0000e+00")
        For i = 0 To 1000
            If Not Training Then Exit For
            ann.TrainEpoch(X, Y)
            'If i Mod 20 <> 0 Then Continue For
            Application.DoEvents()
            'Dim H = ann.Predict(X)
            Dim err = ann.GetError(X, Y)
            ToolStripStatusLabelError.Text = "E = " & err.ToString("0.0000e+00")
            If err < 0.00000001 Then Exit For
        Next
        Training = False
        ButtonTrain.Text = "Nauči mrežu"
    End Sub

    Private Sub TabControl1_Deselected(sender As Object, e As TabControlEventArgs) Handles TabControl1.Deselected
        If e.TabPage Is TabPageTraining Then
            RemoveHandler GesturePanel1.GestureDrawn, AddressOf GesturePanel1_GestureDrawn_Collection
        ElseIf e.TabPage Is TabPageRecognition Then
            RemoveHandler GesturePanel1.GestureDrawn, AddressOf GesturePanel1_GestureDrawn_Recognition
        End If
    End Sub

    Private Sub TabControl1_Selected(sender As Object, e As TabControlEventArgs) Handles TabControl1.Selected
        If e.TabPage Is TabPageTraining Then
            AddHandler GesturePanel1.GestureDrawn, AddressOf GesturePanel1_GestureDrawn_Collection
        ElseIf e.TabPage Is TabPageRecognition Then
            AddHandler GesturePanel1.GestureDrawn, AddressOf GesturePanel1_GestureDrawn_Recognition
        End If
    End Sub

#End Region

#Region "NeuronskaMreža"

    Private Sub CreateAppropriateNeuralNetwork()
        Dim inputNode = Nodes.Input(SampleCount * 2)
        Dim labelNode = Nodes.Input(ClassCount)
        Dim annOutput = inputNode.WeightedSums(20).Add().Logistic().WeightedSums(ClassCount).Add().Logistic()
        Dim trainer = New Trainer(
            inputNode:=inputNode, labelNode:=labelNode, lossNode:=annOutput.BernoulliCrossEntropyLoss(labelNode),
            optimizer:=New MomentumRMSPropOptimizer(learningRate:=0.05, gamma:=0.9, momentum:=0.95),
            batchSize:=1)
        ann = New GraphicalLearningMachine(trainer)
    End Sub

#End Region
End Class
