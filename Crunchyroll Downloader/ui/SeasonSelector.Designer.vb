Namespace ui
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class SeasonSelector
        Inherits MetroFramework.Forms.MetroForm

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
            Me.components = New System.ComponentModel.Container()
            Me.MetroStyleExtender1 = New MetroFramework.Components.MetroStyleExtender(Me.components)
            Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
            Me.selectVideosGroupBox = New System.Windows.Forms.GroupBox()
            Me.CancelDialogButton = New MetroFramework.Controls.MetroButton()
            Me.endEpisodeComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.startEpisodeComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.seasonSelectComboBox = New MetroFramework.Controls.MetroComboBox()
            Me.downloadButton = New MetroFramework.Controls.MetroButton()
            CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.selectVideosGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'MetroStyleManager1
            '
            Me.MetroStyleManager1.Owner = Me
            Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Orange
            '
            'selectVideosGroupBox
            '
            Me.selectVideosGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.selectVideosGroupBox.Controls.Add(Me.CancelDialogButton)
            Me.selectVideosGroupBox.Controls.Add(Me.endEpisodeComboBox)
            Me.selectVideosGroupBox.Controls.Add(Me.startEpisodeComboBox)
            Me.selectVideosGroupBox.Controls.Add(Me.seasonSelectComboBox)
            Me.selectVideosGroupBox.Location = New System.Drawing.Point(23, 75)
            Me.selectVideosGroupBox.Name = "selectVideosGroupBox"
            Me.selectVideosGroupBox.Size = New System.Drawing.Size(551, 195)
            Me.selectVideosGroupBox.TabIndex = 8
            Me.selectVideosGroupBox.TabStop = False
            '
            'CancelDialogButton
            '
            Me.CancelDialogButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.CancelDialogButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelDialogButton.Location = New System.Drawing.Point(190, 164)
            Me.CancelDialogButton.Name = "CancelDialogButton"
            Me.CancelDialogButton.Size = New System.Drawing.Size(159, 25)
            Me.CancelDialogButton.TabIndex = 3
            Me.CancelDialogButton.Text = "Cancel"
            Me.CancelDialogButton.UseSelectable = True
            '
            'endEpisodeComboBox
            '
            Me.endEpisodeComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.endEpisodeComboBox.FormattingEnabled = True
            Me.endEpisodeComboBox.ItemHeight = 23
            Me.endEpisodeComboBox.Location = New System.Drawing.Point(7, 96)
            Me.endEpisodeComboBox.Name = "endEpisodeComboBox"
            Me.endEpisodeComboBox.Size = New System.Drawing.Size(538, 29)
            Me.endEpisodeComboBox.TabIndex = 2
            Me.endEpisodeComboBox.UseSelectable = True
            '
            'startEpisodeComboBox
            '
            Me.startEpisodeComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.startEpisodeComboBox.FormattingEnabled = True
            Me.startEpisodeComboBox.ItemHeight = 23
            Me.startEpisodeComboBox.Location = New System.Drawing.Point(7, 60)
            Me.startEpisodeComboBox.Name = "startEpisodeComboBox"
            Me.startEpisodeComboBox.Size = New System.Drawing.Size(538, 29)
            Me.startEpisodeComboBox.TabIndex = 1
            Me.startEpisodeComboBox.UseSelectable = True
            '
            'seasonSelectComboBox
            '
            Me.seasonSelectComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.seasonSelectComboBox.FormattingEnabled = True
            Me.seasonSelectComboBox.ItemHeight = 23
            Me.seasonSelectComboBox.Location = New System.Drawing.Point(6, 24)
            Me.seasonSelectComboBox.Name = "seasonSelectComboBox"
            Me.seasonSelectComboBox.Size = New System.Drawing.Size(539, 29)
            Me.seasonSelectComboBox.TabIndex = 0
            Me.seasonSelectComboBox.UseSelectable = True
            '
            'downloadButton
            '
            Me.downloadButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.downloadButton.FontSize = MetroFramework.MetroButtonSize.Tall
            Me.downloadButton.ForeColor = System.Drawing.Color.White
            Me.downloadButton.Highlight = True
            Me.downloadButton.Location = New System.Drawing.Point(133, 300)
            Me.downloadButton.Name = "downloadButton"
            Me.downloadButton.Size = New System.Drawing.Size(320, 47)
            Me.downloadButton.Style = MetroFramework.MetroColorStyle.Orange
            Me.downloadButton.TabIndex = 9
            Me.downloadButton.Text = "Download"
            Me.downloadButton.UseCustomBackColor = True
            Me.downloadButton.UseCustomForeColor = True
            Me.downloadButton.UseSelectable = True
            '
            'SeasonSelector
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(597, 370)
            Me.Controls.Add(Me.downloadButton)
            Me.Controls.Add(Me.selectVideosGroupBox)
            Me.MaximizeBox = False
            Me.Name = "SeasonSelector"
            Me.Resizable = False
            Me.ShowIcon = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Style = MetroFramework.MetroColorStyle.Orange
            Me.Text = "Select video"
            CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.selectVideosGroupBox.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents MetroStyleExtender1 As MetroFramework.Components.MetroStyleExtender
        Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
        Friend WithEvents selectVideosGroupBox As GroupBox
        Friend WithEvents CancelDialogButton As MetroFramework.Controls.MetroButton
        Friend WithEvents endEpisodeComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents startEpisodeComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents seasonSelectComboBox As MetroFramework.Controls.MetroComboBox
        Friend WithEvents downloadButton As MetroFramework.Controls.MetroButton
    End Class
End Namespace