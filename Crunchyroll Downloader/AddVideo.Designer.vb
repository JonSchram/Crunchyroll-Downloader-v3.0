<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AddVideo
    Inherits MetroFramework.Forms.MetroForm

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
        Me.components = New System.ComponentModel.Container()
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.MetroStyleExtender1 = New MetroFramework.Components.MetroStyleExtender(Me.components)
        Me.downloadUrlTextBox = New MetroFramework.Controls.MetroTextBox()
        Me.nameFormatTextBox = New MetroFramework.Controls.MetroTextBox()
        Me.outputTextBox = New MetroFramework.Controls.MetroTextBox()
        Me.parametersGroupBox = New System.Windows.Forms.GroupBox()
        Me.downloadTypeComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.subfolderComboBox = New MetroFramework.Controls.MetroComboBox()
        Me.StatusLabel = New MetroFramework.Controls.MetroLabel()
        Me.downloadButton = New MetroFramework.Controls.MetroButton()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.parametersGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Me
        Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Orange
        '
        'downloadUrlTextBox
        '
        Me.downloadUrlTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.downloadUrlTextBox.CustomButton.Image = Nothing
        Me.downloadUrlTextBox.CustomButton.Location = New System.Drawing.Point(549, 1)
        Me.downloadUrlTextBox.CustomButton.Name = ""
        Me.downloadUrlTextBox.CustomButton.Size = New System.Drawing.Size(21, 21)
        Me.downloadUrlTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.downloadUrlTextBox.CustomButton.TabIndex = 1
        Me.downloadUrlTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.downloadUrlTextBox.CustomButton.UseSelectable = True
        Me.downloadUrlTextBox.CustomButton.Visible = False
        Me.downloadUrlTextBox.Lines = New String() {"URL"}
        Me.downloadUrlTextBox.Location = New System.Drawing.Point(6, 22)
        Me.downloadUrlTextBox.MaxLength = 32767
        Me.downloadUrlTextBox.Name = "downloadUrlTextBox"
        Me.downloadUrlTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.downloadUrlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.downloadUrlTextBox.SelectedText = ""
        Me.downloadUrlTextBox.SelectionLength = 0
        Me.downloadUrlTextBox.SelectionStart = 0
        Me.downloadUrlTextBox.ShortcutsEnabled = True
        Me.downloadUrlTextBox.Size = New System.Drawing.Size(571, 23)
        Me.downloadUrlTextBox.TabIndex = 0
        Me.downloadUrlTextBox.Text = "URL"
        Me.downloadUrlTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.downloadUrlTextBox.UseSelectable = True
        Me.downloadUrlTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.downloadUrlTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'nameFormatTextBox
        '
        Me.nameFormatTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.nameFormatTextBox.CustomButton.Image = Nothing
        Me.nameFormatTextBox.CustomButton.Location = New System.Drawing.Point(549, 1)
        Me.nameFormatTextBox.CustomButton.Name = ""
        Me.nameFormatTextBox.CustomButton.Size = New System.Drawing.Size(21, 21)
        Me.nameFormatTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.nameFormatTextBox.CustomButton.TabIndex = 1
        Me.nameFormatTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.nameFormatTextBox.CustomButton.UseSelectable = True
        Me.nameFormatTextBox.CustomButton.Visible = False
        Me.nameFormatTextBox.Lines = New String() {"Use custom name"}
        Me.nameFormatTextBox.Location = New System.Drawing.Point(6, 51)
        Me.nameFormatTextBox.MaxLength = 32767
        Me.nameFormatTextBox.Name = "nameFormatTextBox"
        Me.nameFormatTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.nameFormatTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.nameFormatTextBox.SelectedText = ""
        Me.nameFormatTextBox.SelectionLength = 0
        Me.nameFormatTextBox.SelectionStart = 0
        Me.nameFormatTextBox.ShortcutsEnabled = True
        Me.nameFormatTextBox.Size = New System.Drawing.Size(571, 23)
        Me.nameFormatTextBox.TabIndex = 1
        Me.nameFormatTextBox.Text = "Use custom name"
        Me.nameFormatTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nameFormatTextBox.UseSelectable = True
        Me.nameFormatTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.nameFormatTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'outputTextBox
        '
        Me.outputTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.outputTextBox.CustomButton.Image = Nothing
        Me.outputTextBox.CustomButton.Location = New System.Drawing.Point(549, 1)
        Me.outputTextBox.CustomButton.Name = ""
        Me.outputTextBox.CustomButton.Size = New System.Drawing.Size(21, 21)
        Me.outputTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.outputTextBox.CustomButton.TabIndex = 1
        Me.outputTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.outputTextBox.CustomButton.UseSelectable = True
        Me.outputTextBox.CustomButton.Visible = False
        Me.outputTextBox.Lines = New String() {"Save location"}
        Me.outputTextBox.Location = New System.Drawing.Point(6, 80)
        Me.outputTextBox.MaxLength = 32767
        Me.outputTextBox.Name = "outputTextBox"
        Me.outputTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.outputTextBox.SelectedText = ""
        Me.outputTextBox.SelectionLength = 0
        Me.outputTextBox.SelectionStart = 0
        Me.outputTextBox.ShortcutsEnabled = True
        Me.outputTextBox.Size = New System.Drawing.Size(571, 23)
        Me.outputTextBox.TabIndex = 2
        Me.outputTextBox.Text = "Save location"
        Me.outputTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.outputTextBox.UseSelectable = True
        Me.outputTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.outputTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'parametersGroupBox
        '
        Me.parametersGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.parametersGroupBox.Controls.Add(Me.downloadTypeComboBox)
        Me.parametersGroupBox.Controls.Add(Me.subfolderComboBox)
        Me.parametersGroupBox.Controls.Add(Me.StatusLabel)
        Me.parametersGroupBox.Controls.Add(Me.nameFormatTextBox)
        Me.parametersGroupBox.Controls.Add(Me.downloadUrlTextBox)
        Me.parametersGroupBox.Controls.Add(Me.outputTextBox)
        Me.parametersGroupBox.Location = New System.Drawing.Point(23, 63)
        Me.parametersGroupBox.Name = "parametersGroupBox"
        Me.parametersGroupBox.Size = New System.Drawing.Size(583, 220)
        Me.parametersGroupBox.TabIndex = 5
        Me.parametersGroupBox.TabStop = False
        '
        'downloadTypeComboBox
        '
        Me.downloadTypeComboBox.FormattingEnabled = True
        Me.downloadTypeComboBox.ItemHeight = 23
        Me.downloadTypeComboBox.Location = New System.Drawing.Point(6, 144)
        Me.downloadTypeComboBox.Name = "downloadTypeComboBox"
        Me.downloadTypeComboBox.Size = New System.Drawing.Size(571, 29)
        Me.downloadTypeComboBox.TabIndex = 7
        Me.downloadTypeComboBox.UseSelectable = True
        '
        'subfolderComboBox
        '
        Me.subfolderComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.subfolderComboBox.FormattingEnabled = True
        Me.subfolderComboBox.ItemHeight = 23
        Me.subfolderComboBox.Location = New System.Drawing.Point(6, 109)
        Me.subfolderComboBox.Name = "subfolderComboBox"
        Me.subfolderComboBox.Size = New System.Drawing.Size(571, 29)
        Me.subfolderComboBox.TabIndex = 6
        Me.subfolderComboBox.UseSelectable = True
        '
        'StatusLabel
        '
        Me.StatusLabel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.StatusLabel.Location = New System.Drawing.Point(3, 176)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(577, 41)
        Me.StatusLabel.TabIndex = 5
        Me.StatusLabel.Text = "Status:"
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'downloadButton
        '
        Me.downloadButton.FontSize = MetroFramework.MetroButtonSize.Tall
        Me.downloadButton.Location = New System.Drawing.Point(159, 291)
        Me.downloadButton.Name = "downloadButton"
        Me.downloadButton.Size = New System.Drawing.Size(320, 47)
        Me.downloadButton.Style = MetroFramework.MetroColorStyle.Orange
        Me.downloadButton.TabIndex = 6
        Me.downloadButton.Text = "Download"
        Me.downloadButton.UseSelectable = True
        '
        'AddVideo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 361)
        Me.Controls.Add(Me.downloadButton)
        Me.Controls.Add(Me.parametersGroupBox)
        Me.MaximizeBox = False
        Me.Name = "AddVideo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Video"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        Me.Theme = MetroFramework.MetroThemeStyle.[Default]
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.parametersGroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
    Friend WithEvents MetroStyleExtender1 As MetroFramework.Components.MetroStyleExtender
    Friend WithEvents downloadUrlTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents nameFormatTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents outputTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents parametersGroupBox As GroupBox
    Friend WithEvents StatusLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents downloadButton As MetroFramework.Controls.MetroButton
    Friend WithEvents downloadTypeComboBox As MetroFramework.Controls.MetroComboBox
    Friend WithEvents subfolderComboBox As MetroFramework.Controls.MetroComboBox
End Class
