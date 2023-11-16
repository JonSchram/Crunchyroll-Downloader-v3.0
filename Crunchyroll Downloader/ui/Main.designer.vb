Namespace ui
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class Main
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
            Me.PictureBox5 = New System.Windows.Forms.PictureBox()
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
            Me.MetroStyleExtender1 = New MetroFramework.Components.MetroStyleExtender(Me.components)
            Me.TaskFlowPanel = New System.Windows.Forms.FlowLayoutPanel()
            Me.MainStyleManager = New MetroFramework.Components.MetroStyleManager(Me.components)
            Me.Btn_add = New System.Windows.Forms.Button()
            Me.Btn_Browser = New System.Windows.Forms.Button()
            Me.Btn_Settings = New System.Windows.Forms.Button()
            Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.QueueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SaveThumbnailAsImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToggleDebugModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.Funimation_Token = New System.Windows.Forms.ToolStripMenuItem()
            Me.CheckCRBetaTokenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ThreadCount = New System.Windows.Forms.ToolStripMenuItem()
            Me.CRCookieToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.UrlJsonsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.Btn_Queue = New System.Windows.Forms.Button()
            Me.DebugButton = New MetroFramework.Controls.MetroButton()
            CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MainStyleManager, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ContextMenuStrip1.SuspendLayout()
            Me.SuspendLayout()
            '
            'PictureBox5
            '
            Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
            Me.PictureBox5.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.balken
            resources.ApplyResources(Me.PictureBox5, "PictureBox5")
            Me.PictureBox5.Name = "PictureBox5"
            Me.PictureBox5.TabStop = False
            '
            'Timer2
            '
            Me.Timer2.Enabled = True
            Me.Timer2.Interval = 3000
            '
            'TaskFlowPanel
            '
            resources.ApplyResources(Me.TaskFlowPanel, "TaskFlowPanel")
            Me.MetroStyleExtender1.SetApplyMetroTheme(Me.TaskFlowPanel, True)
            Me.TaskFlowPanel.Name = "TaskFlowPanel"
            '
            'MainStyleManager
            '
            Me.MainStyleManager.Owner = Me
            Me.MainStyleManager.Style = MetroFramework.MetroColorStyle.Orange
            '
            'Btn_add
            '
            Me.Btn_add.BackColor = System.Drawing.Color.Transparent
            resources.ApplyResources(Me.Btn_add, "Btn_add")
            Me.Btn_add.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Btn_add.FlatAppearance.BorderSize = 0
            Me.Btn_add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.Btn_add.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_add
            Me.Btn_add.Name = "Btn_add"
            Me.Btn_add.UseVisualStyleBackColor = False
            '
            'Btn_Browser
            '
            Me.Btn_Browser.BackColor = System.Drawing.Color.Transparent
            resources.ApplyResources(Me.Btn_Browser, "Btn_Browser")
            Me.Btn_Browser.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Btn_Browser.FlatAppearance.BorderSize = 0
            Me.Btn_Browser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.Btn_Browser.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_browser
            Me.Btn_Browser.Name = "Btn_Browser"
            Me.Btn_Browser.UseVisualStyleBackColor = False
            '
            'Btn_Settings
            '
            resources.ApplyResources(Me.Btn_Settings, "Btn_Settings")
            Me.Btn_Settings.BackColor = System.Drawing.Color.Transparent
            Me.Btn_Settings.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Btn_Settings.FlatAppearance.BorderSize = 0
            Me.Btn_Settings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.Btn_Settings.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_settings
            Me.Btn_Settings.Name = "Btn_Settings"
            Me.Btn_Settings.UseVisualStyleBackColor = False
            '
            'ContextMenuStrip1
            '
            Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QueueToolStripMenuItem, Me.SaveThumbnailAsImageToolStripMenuItem, Me.ToggleDebugModeToolStripMenuItem, Me.Funimation_Token, Me.CheckCRBetaTokenToolStripMenuItem, Me.ThreadCount, Me.CRCookieToolStripMenuItem, Me.UrlJsonsToolStripMenuItem})
            Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
            resources.ApplyResources(Me.ContextMenuStrip1, "ContextMenuStrip1")
            '
            'QueueToolStripMenuItem
            '
            Me.QueueToolStripMenuItem.Name = "QueueToolStripMenuItem"
            resources.ApplyResources(Me.QueueToolStripMenuItem, "QueueToolStripMenuItem")
            '
            'SaveThumbnailAsImageToolStripMenuItem
            '
            Me.SaveThumbnailAsImageToolStripMenuItem.Name = "SaveThumbnailAsImageToolStripMenuItem"
            resources.ApplyResources(Me.SaveThumbnailAsImageToolStripMenuItem, "SaveThumbnailAsImageToolStripMenuItem")
            '
            'ToggleDebugModeToolStripMenuItem
            '
            Me.ToggleDebugModeToolStripMenuItem.Name = "ToggleDebugModeToolStripMenuItem"
            resources.ApplyResources(Me.ToggleDebugModeToolStripMenuItem, "ToggleDebugModeToolStripMenuItem")
            '
            'Funimation_Token
            '
            Me.Funimation_Token.Name = "Funimation_Token"
            resources.ApplyResources(Me.Funimation_Token, "Funimation_Token")
            '
            'CheckCRBetaTokenToolStripMenuItem
            '
            Me.CheckCRBetaTokenToolStripMenuItem.Name = "CheckCRBetaTokenToolStripMenuItem"
            resources.ApplyResources(Me.CheckCRBetaTokenToolStripMenuItem, "CheckCRBetaTokenToolStripMenuItem")
            '
            'ThreadCount
            '
            Me.ThreadCount.Name = "ThreadCount"
            resources.ApplyResources(Me.ThreadCount, "ThreadCount")
            '
            'CRCookieToolStripMenuItem
            '
            Me.CRCookieToolStripMenuItem.Name = "CRCookieToolStripMenuItem"
            resources.ApplyResources(Me.CRCookieToolStripMenuItem, "CRCookieToolStripMenuItem")
            '
            'UrlJsonsToolStripMenuItem
            '
            Me.UrlJsonsToolStripMenuItem.Name = "UrlJsonsToolStripMenuItem"
            resources.ApplyResources(Me.UrlJsonsToolStripMenuItem, "UrlJsonsToolStripMenuItem")
            '
            'Btn_Queue
            '
            resources.ApplyResources(Me.Btn_Queue, "Btn_Queue")
            Me.Btn_Queue.BackColor = System.Drawing.Color.Transparent
            Me.Btn_Queue.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Btn_Queue.FlatAppearance.BorderSize = 0
            Me.Btn_Queue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.Btn_Queue.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_queue
            Me.Btn_Queue.Name = "Btn_Queue"
            Me.Btn_Queue.UseVisualStyleBackColor = False
            '
            'DebugButton
            '
            resources.ApplyResources(Me.DebugButton, "DebugButton")
            Me.DebugButton.Name = "DebugButton"
            Me.DebugButton.UseSelectable = True
            '
            'Main
            '
            Me.ApplyImageInvert = True
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
            resources.ApplyResources(Me, "$this")
            Me.Controls.Add(Me.TaskFlowPanel)
            Me.Controls.Add(Me.DebugButton)
            Me.Controls.Add(Me.Btn_Queue)
            Me.Controls.Add(Me.Btn_Settings)
            Me.Controls.Add(Me.Btn_Browser)
            Me.Controls.Add(Me.Btn_add)
            Me.Controls.Add(Me.PictureBox5)
            Me.ForeColor = System.Drawing.Color.Black
            Me.MaximizeBox = False
            Me.Name = "Main"
            Me.Style = MetroFramework.MetroColorStyle.Orange
            Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
            CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MainStyleManager, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ContextMenuStrip1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents PictureBox5 As PictureBox
        Friend WithEvents ToolTip1 As ToolTip
        Friend WithEvents Timer2 As Timer
        Friend WithEvents MetroStyleExtender1 As MetroFramework.Components.MetroStyleExtender
        Friend WithEvents MainStyleManager As MetroFramework.Components.MetroStyleManager
        Friend WithEvents Btn_add As Button
        Friend WithEvents Btn_Browser As Button
        Friend WithEvents Btn_Settings As Button
        Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
        Friend WithEvents ToggleDebugModeToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents Funimation_Token As ToolStripMenuItem
        Friend WithEvents CheckCRBetaTokenToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents ThreadCount As ToolStripMenuItem
        Friend WithEvents CRCookieToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents UrlJsonsToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents QueueToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents Btn_Queue As Button
        Friend WithEvents SaveThumbnailAsImageToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents DebugButton As MetroFramework.Controls.MetroButton
        Friend WithEvents TaskFlowPanel As FlowLayoutPanel
    End Class
End Namespace