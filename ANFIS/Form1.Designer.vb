<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ARulesPlot = New OxyPlot.WindowsForms.PlotView()
        Me.ConclusionsPlot = New OxyPlot.WindowsForms.PlotView()
        Me.OutputPlot = New OxyPlot.WindowsForms.PlotView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.FsPlot = New OxyPlot.WindowsForms.PlotView()
        Me.BRulesPlot = New OxyPlot.WindowsForms.PlotView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TrainButton = New System.Windows.Forms.ToolStripButton()
        Me.ResetButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.RuleCountComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.ErrorLabel = New System.Windows.Forms.ToolStripLabel()
        Me.EpochCountLabel = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.BatchSizeComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.LearningRateTextbox = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.OriginalFunctionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PredictionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AntecedentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConclusionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ErrorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ARulesPlot
        '
        Me.ARulesPlot.BackColor = System.Drawing.Color.White
        Me.ARulesPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ARulesPlot.Enabled = False
        Me.ARulesPlot.Location = New System.Drawing.Point(2, 2)
        Me.ARulesPlot.Margin = New System.Windows.Forms.Padding(2)
        Me.ARulesPlot.Name = "ARulesPlot"
        Me.ARulesPlot.PanCursor = System.Windows.Forms.Cursors.Hand
        Me.ARulesPlot.Size = New System.Drawing.Size(219, 171)
        Me.ARulesPlot.TabIndex = 2
        Me.ARulesPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE
        Me.ARulesPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE
        Me.ARulesPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS
        '
        'ConclusionsPlot
        '
        Me.ConclusionsPlot.BackColor = System.Drawing.Color.White
        Me.ConclusionsPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConclusionsPlot.Enabled = False
        Me.ConclusionsPlot.Location = New System.Drawing.Point(225, 2)
        Me.ConclusionsPlot.Margin = New System.Windows.Forms.Padding(2)
        Me.ConclusionsPlot.Name = "ConclusionsPlot"
        Me.ConclusionsPlot.PanCursor = System.Windows.Forms.Cursors.Hand
        Me.ConclusionsPlot.Size = New System.Drawing.Size(219, 171)
        Me.ConclusionsPlot.TabIndex = 3
        Me.ConclusionsPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE
        Me.ConclusionsPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE
        Me.ConclusionsPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS
        '
        'OutputPlot
        '
        Me.OutputPlot.BackColor = System.Drawing.Color.White
        Me.OutputPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputPlot.Enabled = False
        Me.OutputPlot.Location = New System.Drawing.Point(448, 2)
        Me.OutputPlot.Margin = New System.Windows.Forms.Padding(2)
        Me.OutputPlot.Name = "OutputPlot"
        Me.OutputPlot.PanCursor = System.Windows.Forms.Cursors.Hand
        Me.TableLayoutPanel1.SetRowSpan(Me.OutputPlot, 2)
        Me.OutputPlot.Size = New System.Drawing.Size(444, 343)
        Me.OutputPlot.TabIndex = 4
        Me.OutputPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE
        Me.OutputPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE
        Me.OutputPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.FsPlot, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.BRulesPlot, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ARulesPlot, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ConclusionsPlot, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OutputPlot, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.55556!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.44444!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(894, 347)
        Me.TableLayoutPanel1.TabIndex = 7
        '
        'FsPlot
        '
        Me.FsPlot.BackColor = System.Drawing.Color.White
        Me.FsPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FsPlot.Enabled = False
        Me.FsPlot.Location = New System.Drawing.Point(225, 177)
        Me.FsPlot.Margin = New System.Windows.Forms.Padding(2)
        Me.FsPlot.Name = "FsPlot"
        Me.FsPlot.PanCursor = System.Windows.Forms.Cursors.Hand
        Me.FsPlot.Size = New System.Drawing.Size(219, 168)
        Me.FsPlot.TabIndex = 7
        Me.FsPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE
        Me.FsPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE
        Me.FsPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS
        '
        'BRulesPlot
        '
        Me.BRulesPlot.BackColor = System.Drawing.Color.White
        Me.BRulesPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BRulesPlot.Enabled = False
        Me.BRulesPlot.Location = New System.Drawing.Point(2, 177)
        Me.BRulesPlot.Margin = New System.Windows.Forms.Padding(2)
        Me.BRulesPlot.Name = "BRulesPlot"
        Me.BRulesPlot.PanCursor = System.Windows.Forms.Cursors.Hand
        Me.BRulesPlot.Size = New System.Drawing.Size(219, 168)
        Me.BRulesPlot.TabIndex = 6
        Me.BRulesPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE
        Me.BRulesPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE
        Me.BRulesPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TrainButton, Me.ResetButton, Me.ToolStripSeparator1, Me.ToolStripLabel1, Me.RuleCountComboBox, Me.ErrorLabel, Me.EpochCountLabel, Me.ToolStripLabel2, Me.BatchSizeComboBox, Me.ToolStripLabel3, Me.LearningRateTextbox, Me.ToolStripSeparator2, Me.ToolStripDropDownButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 347)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(894, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TrainButton
        '
        Me.TrainButton.AutoSize = False
        Me.TrainButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TrainButton.Image = CType(resources.GetObject("TrainButton.Image"), System.Drawing.Image)
        Me.TrainButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TrainButton.Name = "TrainButton"
        Me.TrainButton.Size = New System.Drawing.Size(50, 22)
        Me.TrainButton.Text = "Train"
        '
        'ResetButton
        '
        Me.ResetButton.AutoSize = False
        Me.ResetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ResetButton.Image = CType(resources.GetObject("ResetButton.Image"), System.Drawing.Image)
        Me.ResetButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ResetButton.Name = "ResetButton"
        Me.ResetButton.Size = New System.Drawing.Size(50, 22)
        Me.ResetButton.Text = "Reset"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(67, 22)
        Me.ToolStripLabel1.Text = "Rule count:"
        '
        'RuleCountComboBox
        '
        Me.RuleCountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RuleCountComboBox.DropDownWidth = 75
        Me.RuleCountComboBox.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8"})
        Me.RuleCountComboBox.Name = "RuleCountComboBox"
        Me.RuleCountComboBox.Size = New System.Drawing.Size(75, 25)
        '
        'ErrorLabel
        '
        Me.ErrorLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ErrorLabel.AutoSize = False
        Me.ErrorLabel.Name = "ErrorLabel"
        Me.ErrorLabel.Size = New System.Drawing.Size(100, 22)
        Me.ErrorLabel.Text = "Error"
        '
        'EpochCountLabel
        '
        Me.EpochCountLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.EpochCountLabel.AutoSize = False
        Me.EpochCountLabel.Name = "EpochCountLabel"
        Me.EpochCountLabel.Size = New System.Drawing.Size(100, 22)
        Me.EpochCountLabel.Text = "Epoch"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(62, 22)
        Me.ToolStripLabel2.Text = "Batch size:"
        '
        'BatchSizeComboBox
        '
        Me.BatchSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BatchSizeComboBox.DropDownWidth = 75
        Me.BatchSizeComboBox.Items.AddRange(New Object() {"1", "2", "3", "9", "27", "-1"})
        Me.BatchSizeComboBox.Name = "BatchSizeComboBox"
        Me.BatchSizeComboBox.Size = New System.Drawing.Size(75, 25)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(79, 22)
        Me.ToolStripLabel3.Text = "Learning rate:"
        '
        'LearningRateTextbox
        '
        Me.LearningRateTextbox.MaxLength = 15
        Me.LearningRateTextbox.Name = "LearningRateTextbox"
        Me.LearningRateTextbox.Size = New System.Drawing.Size(96, 25)
        Me.LearningRateTextbox.Text = "5e-3"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OriginalFunctionToolStripMenuItem, Me.PredictionToolStripMenuItem, Me.AntecedentsToolStripMenuItem, Me.ConclusionsToolStripMenuItem, Me.ErrorToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(79, 22)
        Me.ToolStripDropDownButton1.Text = "Plot to CSV"
        '
        'OriginalFunctionToolStripMenuItem
        '
        Me.OriginalFunctionToolStripMenuItem.Name = "OriginalFunctionToolStripMenuItem"
        Me.OriginalFunctionToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.OriginalFunctionToolStripMenuItem.Text = "Target"
        '
        'PredictionToolStripMenuItem
        '
        Me.PredictionToolStripMenuItem.Name = "PredictionToolStripMenuItem"
        Me.PredictionToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.PredictionToolStripMenuItem.Text = "Prediction"
        '
        'AntecedentsToolStripMenuItem
        '
        Me.AntecedentsToolStripMenuItem.Name = "AntecedentsToolStripMenuItem"
        Me.AntecedentsToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.AntecedentsToolStripMenuItem.Text = "Antecedents"
        '
        'ConclusionsToolStripMenuItem
        '
        Me.ConclusionsToolStripMenuItem.Name = "ConclusionsToolStripMenuItem"
        Me.ConclusionsToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.ConclusionsToolStripMenuItem.Text = "Conclusions"
        '
        'ErrorToolStripMenuItem
        '
        Me.ErrorToolStripMenuItem.Name = "ErrorToolStripMenuItem"
        Me.ErrorToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.ErrorToolStripMenuItem.Text = "Error"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 372)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents ARulesPlot As OxyPlot.WindowsForms.PlotView
    Private WithEvents ConclusionsPlot As OxyPlot.WindowsForms.PlotView
    Private WithEvents OutputPlot As OxyPlot.WindowsForms.PlotView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Private WithEvents BRulesPlot As OxyPlot.WindowsForms.PlotView
    Private WithEvents FsPlot As OxyPlot.WindowsForms.PlotView
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents TrainButton As ToolStripButton
    Friend WithEvents RuleCountComboBox As ToolStripComboBox
    Friend WithEvents ResetButton As ToolStripButton
    Friend WithEvents ErrorLabel As ToolStripLabel
    Friend WithEvents EpochCountLabel As ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents BatchSizeComboBox As ToolStripComboBox
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents LearningRateTextbox As ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripDropDownButton1 As ToolStripDropDownButton
    Friend WithEvents PredictionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OriginalFunctionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConclusionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ErrorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AntecedentsToolStripMenuItem As ToolStripMenuItem
End Class
