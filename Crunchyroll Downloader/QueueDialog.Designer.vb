<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class QueueDialog
    Inherits MetroFramework.Forms.MetroForm

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.QueueDisplayListBox = New System.Windows.Forms.ListBox()
        Me.RunQueueToggle = New MetroFramework.Controls.MetroToggle()
        Me.Label1 = New MetroFramework.Controls.MetroLabel()
        Me.RunQueueTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MetroStyleExtender1 = New MetroFramework.Components.MetroStyleExtender(Me.components)
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'QueueDisplayListBox
        '
        Me.QueueDisplayListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueDisplayListBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.QueueDisplayListBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QueueDisplayListBox.FormattingEnabled = True
        Me.QueueDisplayListBox.ItemHeight = 20
        Me.QueueDisplayListBox.Location = New System.Drawing.Point(25, 65)
        Me.QueueDisplayListBox.Name = "QueueDisplayListBox"
        Me.QueueDisplayListBox.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.QueueDisplayListBox.Size = New System.Drawing.Size(700, 304)
        Me.QueueDisplayListBox.TabIndex = 0
        '
        'RunQueueToggle
        '
        Me.RunQueueToggle.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.RunQueueToggle.Location = New System.Drawing.Point(325, 415)
        Me.RunQueueToggle.Name = "RunQueueToggle"
        Me.RunQueueToggle.Size = New System.Drawing.Size(96, 20)
        Me.RunQueueToggle.TabIndex = 50
        Me.RunQueueToggle.Text = "Off"
        Me.RunQueueToggle.UseSelectable = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.Label1.Location = New System.Drawing.Point(25, 385)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(700, 22)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Process Queue"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RunQueueTimer
        '
        Me.RunQueueTimer.Interval = 2500
        '
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Me
        Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Orange
        '
        'QueueDialog
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(750, 450)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RunQueueToggle)
        Me.Controls.Add(Me.QueueDisplayListBox)
        Me.MaximizeBox = False
        Me.Name = "QueueDialog"
        Me.Text = "Queue"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents QueueDisplayListBox As ListBox
    Friend WithEvents RunQueueToggle As MetroFramework.Controls.MetroToggle
    Friend WithEvents Label1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents RunQueueTimer As Timer
    Friend WithEvents MetroStyleExtender1 As MetroFramework.Components.MetroStyleExtender
    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
End Class
