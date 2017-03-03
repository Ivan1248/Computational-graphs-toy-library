<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelError = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageTraining = New System.Windows.Forms.TabPage()
        Me.ListViewClasses = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.GroupBoxKlase = New System.Windows.Forms.GroupBox()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.ButtonUndoLastInClass = New System.Windows.Forms.Button()
        Me.GroupBoxPodaci = New System.Windows.Forms.GroupBox()
        Me.ButtonLoad = New System.Windows.Forms.Button()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.GroupBoxNN = New System.Windows.Forms.GroupBox()
        Me.ButtonTrain = New System.Windows.Forms.Button()
        Me.TabPageRecognition = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GesturePanel1 = New NeuralNetworksGestureDemo.GesturePanel()
        Me.StatusStrip1.SuspendLayout
        Me.TabControl1.SuspendLayout
        Me.TabPageTraining.SuspendLayout
        Me.FlowLayoutPanel2.SuspendLayout
        Me.GroupBoxKlase.SuspendLayout
        Me.GroupBoxPodaci.SuspendLayout
        Me.FlowLayoutPanel1.SuspendLayout
        Me.GroupBoxNN.SuspendLayout
        Me.TabPageRecognition.SuspendLayout
        Me.SuspendLayout
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabelError})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 329)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(489, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(442, 17)
        Me.ToolStripStatusLabel1.Spring = true
        '
        'ToolStripStatusLabelError
        '
        Me.ToolStripStatusLabelError.Name = "ToolStripStatusLabelError"
        Me.ToolStripStatusLabelError.Size = New System.Drawing.Size(32, 17)
        Me.ToolStripStatusLabelError.Text = "Error"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageTraining)
        Me.TabControl1.Controls.Add(Me.TabPageRecognition)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.TabControl1.Location = New System.Drawing.Point(269, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(220, 329)
        Me.TabControl1.TabIndex = 2
        Me.TabControl1.Tag = "training"
        '
        'TabPageTraining
        '
        Me.TabPageTraining.Controls.Add(Me.ListViewClasses)
        Me.TabPageTraining.Controls.Add(Me.FlowLayoutPanel2)
        Me.TabPageTraining.Controls.Add(Me.FlowLayoutPanel1)
        Me.TabPageTraining.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTraining.Name = "TabPageTraining"
        Me.TabPageTraining.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTraining.Size = New System.Drawing.Size(212, 303)
        Me.TabPageTraining.TabIndex = 0
        Me.TabPageTraining.Text = "Učenje"
        Me.TabPageTraining.UseVisualStyleBackColor = true
        '
        'ListViewClasses
        '
        Me.ListViewClasses.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListViewClasses.CheckBoxes = true
        Me.ListViewClasses.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListViewClasses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewClasses.Location = New System.Drawing.Point(3, 3)
        Me.ListViewClasses.MultiSelect = false
        Me.ListViewClasses.Name = "ListViewClasses"
        Me.ListViewClasses.Size = New System.Drawing.Size(113, 233)
        Me.ListViewClasses.TabIndex = 4
        Me.ListViewClasses.UseCompatibleStateImageBehavior = false
        Me.ListViewClasses.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Klasa"
        Me.ColumnHeader1.Width = 52
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Broj gesti"
        Me.ColumnHeader2.Width = 56
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.AutoSize = true
        Me.FlowLayoutPanel2.Controls.Add(Me.GroupBoxKlase)
        Me.FlowLayoutPanel2.Controls.Add(Me.GroupBoxPodaci)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(116, 3)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(93, 233)
        Me.FlowLayoutPanel2.TabIndex = 9
        '
        'GroupBoxKlase
        '
        Me.GroupBoxKlase.AutoSize = true
        Me.GroupBoxKlase.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBoxKlase.Controls.Add(Me.ButtonRemove)
        Me.GroupBoxKlase.Controls.Add(Me.ButtonAdd)
        Me.GroupBoxKlase.Controls.Add(Me.ButtonUndoLastInClass)
        Me.GroupBoxKlase.Location = New System.Drawing.Point(3, 3)
        Me.GroupBoxKlase.Name = "GroupBoxKlase"
        Me.GroupBoxKlase.Size = New System.Drawing.Size(87, 142)
        Me.GroupBoxKlase.TabIndex = 8
        Me.GroupBoxKlase.TabStop = false
        Me.GroupBoxKlase.Text = "Klase gesti"
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Location = New System.Drawing.Point(6, 48)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRemove.TabIndex = 1
        Me.ButtonRemove.Text = "Ukloni"
        Me.ButtonRemove.UseVisualStyleBackColor = true
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Location = New System.Drawing.Point(6, 19)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(75, 23)
        Me.ButtonAdd.TabIndex = 0
        Me.ButtonAdd.Text = "Dodaj"
        Me.ButtonAdd.UseVisualStyleBackColor = true
        '
        'ButtonUndoLastInClass
        '
        Me.ButtonUndoLastInClass.Location = New System.Drawing.Point(6, 77)
        Me.ButtonUndoLastInClass.Name = "ButtonUndoLastInClass"
        Me.ButtonUndoLastInClass.Size = New System.Drawing.Size(75, 46)
        Me.ButtonUndoLastInClass.TabIndex = 3
        Me.ButtonUndoLastInClass.Text = "Poništi zadnju gestu"
        Me.ButtonUndoLastInClass.UseVisualStyleBackColor = true
        '
        'GroupBoxPodaci
        '
        Me.GroupBoxPodaci.AutoSize = true
        Me.GroupBoxPodaci.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBoxPodaci.Controls.Add(Me.ButtonLoad)
        Me.GroupBoxPodaci.Controls.Add(Me.ButtonSave)
        Me.GroupBoxPodaci.Location = New System.Drawing.Point(3, 151)
        Me.GroupBoxPodaci.Name = "GroupBoxPodaci"
        Me.GroupBoxPodaci.Size = New System.Drawing.Size(87, 87)
        Me.GroupBoxPodaci.TabIndex = 7
        Me.GroupBoxPodaci.TabStop = false
        Me.GroupBoxPodaci.Text = "Podaci"
        '
        'ButtonLoad
        '
        Me.ButtonLoad.Location = New System.Drawing.Point(6, 19)
        Me.ButtonLoad.Name = "ButtonLoad"
        Me.ButtonLoad.Size = New System.Drawing.Size(75, 23)
        Me.ButtonLoad.TabIndex = 4
        Me.ButtonLoad.Text = "Učitaj"
        Me.ButtonLoad.UseVisualStyleBackColor = true
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(6, 45)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSave.TabIndex = 5
        Me.ButtonSave.Text = "Spremi"
        Me.ButtonSave.UseVisualStyleBackColor = true
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoSize = true
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBoxNN)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 236)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(206, 64)
        Me.FlowLayoutPanel1.TabIndex = 2
        '
        'GroupBoxNN
        '
        Me.GroupBoxNN.AutoSize = true
        Me.GroupBoxNN.Controls.Add(Me.ButtonTrain)
        Me.GroupBoxNN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBoxNN.Location = New System.Drawing.Point(3, 3)
        Me.GroupBoxNN.MinimumSize = New System.Drawing.Size(200, 0)
        Me.GroupBoxNN.Name = "GroupBoxNN"
        Me.GroupBoxNN.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.GroupBoxNN.Size = New System.Drawing.Size(200, 58)
        Me.GroupBoxNN.TabIndex = 6
        Me.GroupBoxNN.TabStop = false
        Me.GroupBoxNN.Text = "Neuronska mreža"
        '
        'ButtonTrain
        '
        Me.ButtonTrain.AutoSize = true
        Me.ButtonTrain.Location = New System.Drawing.Point(6, 19)
        Me.ButtonTrain.Name = "ButtonTrain"
        Me.ButtonTrain.Size = New System.Drawing.Size(75, 23)
        Me.ButtonTrain.TabIndex = 2
        Me.ButtonTrain.Text = "Nauči"
        Me.ButtonTrain.UseVisualStyleBackColor = true
        '
        'TabPageRecognition
        '
        Me.TabPageRecognition.BackColor = System.Drawing.Color.White
        Me.TabPageRecognition.Controls.Add(Me.Label1)
        Me.TabPageRecognition.Location = New System.Drawing.Point(4, 22)
        Me.TabPageRecognition.Name = "TabPageRecognition"
        Me.TabPageRecognition.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageRecognition.Size = New System.Drawing.Size(212, 303)
        Me.TabPageRecognition.TabIndex = 1
        Me.TabPageRecognition.Text = "Prepoznavanje"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "    "
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "gest"
        Me.SaveFileDialog1.FileName = "gestures"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "gest"
        '
        'GesturePanel1
        '
        Me.GesturePanel1.BackColor = System.Drawing.Color.White
        Me.GesturePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GesturePanel1.Location = New System.Drawing.Point(0, 0)
        Me.GesturePanel1.Name = "GesturePanel1"
        Me.GesturePanel1.Size = New System.Drawing.Size(269, 329)
        Me.GesturePanel1.TabIndex = 5
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 351)
        Me.Controls.Add(Me.GesturePanel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "Form1"
        Me.Text = "IG's Gesture Recognizing Neural Network"
        Me.StatusStrip1.ResumeLayout(false)
        Me.StatusStrip1.PerformLayout
        Me.TabControl1.ResumeLayout(false)
        Me.TabPageTraining.ResumeLayout(false)
        Me.TabPageTraining.PerformLayout
        Me.FlowLayoutPanel2.ResumeLayout(false)
        Me.FlowLayoutPanel2.PerformLayout
        Me.GroupBoxKlase.ResumeLayout(false)
        Me.GroupBoxPodaci.ResumeLayout(false)
        Me.FlowLayoutPanel1.ResumeLayout(false)
        Me.FlowLayoutPanel1.PerformLayout
        Me.GroupBoxNN.ResumeLayout(false)
        Me.GroupBoxNN.PerformLayout
        Me.TabPageRecognition.ResumeLayout(false)
        Me.TabPageRecognition.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageTraining As TabPage
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents ButtonAdd As Button
    Friend WithEvents ButtonRemove As Button
    Friend WithEvents ButtonTrain As Button
    Friend WithEvents TabPageRecognition As TabPage
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelError As ToolStripStatusLabel
    Friend WithEvents ListViewClasses As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents GesturePanel1 As GesturePanel
    Friend WithEvents ButtonUndoLastInClass As Button
    Friend WithEvents ButtonLoad As Button
    Friend WithEvents ButtonSave As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBoxKlase As GroupBox
    Friend WithEvents GroupBoxPodaci As GroupBox
    Friend WithEvents GroupBoxNN As GroupBox
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
End Class
