<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Browser
    Inherits System.Windows.Forms.Form

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
        Me.UrlTextBox = New System.Windows.Forms.TextBox()
        Me.CopyUrlButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.WebView2 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.Panel1.SuspendLayout()
        CType(Me.WebView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UrlTextBox
        '
        Me.UrlTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UrlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UrlTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UrlTextBox.Location = New System.Drawing.Point(114, 1)
        Me.UrlTextBox.Name = "UrlTextBox"
        Me.UrlTextBox.Size = New System.Drawing.Size(1153, 26)
        Me.UrlTextBox.TabIndex = 1
        '
        'CopyUrlButton
        '
        Me.CopyUrlButton.Location = New System.Drawing.Point(12, 1)
        Me.CopyUrlButton.Name = "CopyUrlButton"
        Me.CopyUrlButton.Size = New System.Drawing.Size(96, 26)
        Me.CopyUrlButton.TabIndex = 2
        Me.CopyUrlButton.Text = "Copy URL"
        Me.CopyUrlButton.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.WebView2)
        Me.Panel1.Location = New System.Drawing.Point(0, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1280, 720)
        Me.Panel1.TabIndex = 4
        '
        'WebView2
        '
        Me.WebView2.AllowExternalDrop = True
        Me.WebView2.CreationProperties = Nothing
        Me.WebView2.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebView2.Location = New System.Drawing.Point(0, 0)
        Me.WebView2.Name = "WebView2"
        Me.WebView2.Size = New System.Drawing.Size(1280, 720)
        Me.WebView2.TabIndex = 0
        Me.WebView2.ZoomFactor = 1.0R
        '
        'Browser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1279, 750)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CopyUrlButton)
        Me.Controls.Add(Me.UrlTextBox)
        Me.MinimumSize = New System.Drawing.Size(480, 480)
        Me.Name = "Browser"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Browser"
        Me.Panel1.ResumeLayout(False)
        CType(Me.WebView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UrlTextBox As TextBox
    Friend WithEvents CopyUrlButton As Button
    Friend WithEvents Panel1 As Panel
    Private WithEvents WebView2 As Microsoft.Web.WebView2.WinForms.WebView2
End Class
