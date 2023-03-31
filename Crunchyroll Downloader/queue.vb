﻿Option Strict On
Imports Crunchyroll_Downloader.download
Imports Crunchyroll_Downloader.settings.general
Imports MetroFramework
Imports MetroFramework.Components

Public Class Queue

    Private ReadOnly DARK_MODE_FOREGROUND_COLOR As Color = Color.FromArgb(243, 243, 243)
    Private ReadOnly DARK_MODE_BACKGROUND_COLOR As Color = Color.FromArgb(50, 50, 50)

    Private ReadOnly LIGHT_MODE_FOREGROUND_COLOR As Color = SystemColors.WindowText
    Private ReadOnly LIGHT_MODE_BACKGROUND_COLOR As Color = Color.FromArgb(243, 243, 243)

    Dim Manager As MetroStyleManager = Main.Manager

    Private episodeQueue As DownloadQueue = DownloadQueue.getInstance()

    Private Sub Reso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler ProgramSettings.DarkModeChanged, AddressOf HandleDarkModeChanged
        HandleDarkModeChanged(ProgramSettings.GetInstance().DarkMode)

        Manager.Owner = Me
        Me.StyleManager = Manager

        ListBox1.DataSource = episodeQueue

        Btn_min.Image = Main.MinImg
        Btn_Close.Image = Main.CloseImg

    End Sub

    Private Sub HandleDarkModeChanged(isDarkMode As Boolean)
        If isDarkMode Then
            ListBox1.BackColor = DARK_MODE_BACKGROUND_COLOR
            ListBox1.ForeColor = DARK_MODE_FOREGROUND_COLOR
        Else
            ListBox1.BackColor = LIGHT_MODE_BACKGROUND_COLOR
            ListBox1.ForeColor = LIGHT_MODE_FOREGROUND_COLOR
        End If
    End Sub


    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub


    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Close.MouseEnter, Btn_Close.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Close.Image = My.Resources.main_close_dark_hover
        Else
            Btn_Close.Image = My.Resources.main_close_hover
        End If
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Close.MouseLeave, Btn_Close.LostFocus
        Btn_Close.Image = Main.CloseImg
    End Sub

    Private Sub Queue_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Btn_Close.Location = New Point(Me.Width - 36, 1)
        Btn_min.Location = New Point(Me.Width - 67, 1)
    End Sub

    Private Sub Btn_min_Click(sender As Object, e As EventArgs) Handles Btn_min.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub RunQueue_CheckedChanged(sender As Object, e As EventArgs) Handles RunQueue.CheckedChanged
        RunQueueTimer.Enabled = RunQueue.Checked
    End Sub

    Private Sub RunQueueTimer_Tick(sender As Object, e As EventArgs) Handles RunQueueTimer.Tick

        Try
            Dim ItemFinshedCount As Integer = 0
            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Main.Panel1.Controls.OfType(Of CRD_List_Item))

            For i As Integer = 0 To Item.Count - 1
                Debug.WriteLine(Item(i).GetIsStatusFinished().ToString)
                If Item(i).GetIsStatusFinished() = True Then
                    ItemFinshedCount = ItemFinshedCount + 1
                End If
            Next

            Main.RunningDownloads = Item.Count - ItemFinshedCount

        Catch ex As Exception
            Main.RunningDownloads = Main.Panel1.Controls.Count
        End Try

        If Main.RunningDownloads < ProgramSettings.GetInstance().SimultaneousDownloads Then
            If Main.ListBoxList.Count > 0 Then
                If CBool(InStr(ListBox1.GetItemText(Main.ListBoxList(0)), "funimation.com")) Then
                    ' TODO
                    'If Main.Funimation_Grapp_RDY = True Then
                    '    Dim UriUsed As String = ListBox1.GetItemText(Main.ListBoxList(0))

                    '    If CBool(InStr(UriUsed, "funimation.com/v/")) Then
                    '        Dim Episode0() As String = UriUsed.Split(New String() {"?"}, System.StringSplitOptions.RemoveEmptyEntries)
                    '        Dim Episode() As String = Episode0(0).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)

                    '        Dim v1JsonUrl As String = "https://d33et77evd9bgg.cloudfront.net/data/v1/episodes/" + Episode(Episode.Length - 1) + ".json"
                    '        Dim v1Json As String = Nothing
                    '        Try
                    '            Using client As New WebClient()
                    '                client.Encoding = System.Text.Encoding.UTF8
                    '                client.Headers.Add(My.Resources.ffmpeg_user_agend)
                    '                v1Json = client.DownloadString(v1JsonUrl)
                    '            End Using
                    '            Main.Funimation_Grapp_RDY = False
                    '            Main.WebbrowserURL = UriUsed
                    '            Main.ListBoxList.Remove(UriUsed)
                    '            Main.b = False
                    '            Main.Invalidate()
                    '            Main.GetFunimationNewJS_VideoProxy(Nothing, v1Json)
                    '            Exit Sub
                    '        Catch ex As Exception
                    '            Debug.WriteLine("error- getting v1Json data for the bypasss")
                    '            Debug.WriteLine(ex.ToString)
                    '        End Try

                    '    End If

                    '    Main.Funimation_Grapp_RDY = False
                    '    Main.WebbrowserURL = UriUsed
                    '    Main.ListBoxList.Remove(UriUsed)
                    '    Main.b = False


                    '    Main.Text = "Status: loading in browser"
                    '    Main.LoadBrowser(UriUsed)
                    'End If

                Else
                    Dim UriUsed As String = ListBox1.GetItemText(Main.ListBoxList(0))

                    If Main.Grapp_RDY = True Then
                        Main.Grapp_RDY = False
                        Main.Text = "Status: loading ..."
                        Main.LoadBrowser(UriUsed)
                        Main.ListBoxList.Remove(UriUsed)
                        Main.b = False
                    End If
                End If



            End If
        End If
    End Sub
End Class