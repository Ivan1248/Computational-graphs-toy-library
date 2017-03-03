

Public Class GesturePanel
    Private mouseIsDown As Boolean = False
    Private ReadOnly points As New List(Of Point)

    Private Sub GesturePanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub GesturePanel_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        mouseIsDown = True
        points.Clear()
    End Sub

    Private Sub GesturePanel_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        mouseIsDown = False
        Me.CreateGraphics().Clear(Me.BackColor)
        If points.Count > 1 Then RaiseEvent GestureDrawn(Me, New GestureEventArgs() With {.gesture = points})
    End Sub

    Public Property Pen As New Pen(Color.FromArgb(255, 127, 127, 127), width := 4)
    'Private pen2 As New Pen(Color.FromArgb(255, 192, 192, 192), width:=3)

    Private Sub GesturePanel_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If Not mouseIsDown Then Return
        points.Add(New Point(e.X, e.Y))
        If points.Count < 2 Then Return
        Using g = Me.CreateGraphics()
            'g.Clear(Me.BackColor)
            'g.DrawCurve(pen2, points.ToArray())
            g.DrawCurve(Pen, points.ToArray())
        End Using
    End Sub

    Public Event GestureDrawn(sender As Object, gesture As GestureEventArgs)

    'Shadows ReadOnly Property CreateParams() As CreateParams
    '    Get
    '        Dim cp = MyBase.CreateParams
    '        cp.ExStyle = cp.ExStyle Or &H2000000  'Turn on WS_EX_COMPOSITED
    '        Return cp
    '    End Get
    'End Property

    'Private Sub GesturePanel_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
    '    If Not mouseIsDown OrElse points.Count < 2 Then Return
    'End Sub
End Class

Public Class GestureEventArgs
    Inherits EventArgs
    Public gesture As List(Of Point)
End Class