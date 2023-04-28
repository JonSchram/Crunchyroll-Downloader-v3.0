Option Strict On

Imports System.IO
Imports Microsoft.Web.WebView2.Core
Imports Crunchyroll_Downloader.settings.general
Imports Crunchyroll_Downloader.ui

Public Class Browser

    Public Shared Instance As Browser = Nothing

    Private Sub New()
        InitializeComponent()
    End Sub

    Public Shared Function GetInstance() As Browser
        If Instance Is Nothing OrElse Instance.IsDisposed() Then
            ' Ensure the browser instance exists and hasn't been closed.
            Instance = New Browser()
        End If

        Return Instance
    End Function

    Private Sub WebView2_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles WebView2.CoreWebView2InitializationCompleted
        WebView2.CoreWebView2.Settings.UserAgent = My.Resources.ffmpeg_user_agend.Replace("User-Agent: ", "")
        If WebView2.CoreWebView2.Source = "about:blank" Or WebView2.CoreWebView2.Source = Nothing Then
            'TextBox1.Text = Main.Startseite
            WebView2.CoreWebView2.Navigate(ProgramSettings.GetInstance().DefaultWebsite)
        End If

    End Sub

    Public Sub Navigate(Url As String)
        WebView2.CoreWebView2.Navigate(Url)
    End Sub


    Private Sub WebView2_SourceChanged(sender As Object, e As CoreWebView2SourceChangedEventArgs) Handles WebView2.SourceChanged
        Try
            UrlTextBox.Text = WebView2.CoreWebView2.Source
        Catch ex As Exception
        End Try
    End Sub

    Private Sub WebView2_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles WebView2.NavigationCompleted
    End Sub

    Public Async Function GetCookies(ByVal Uri As String) As Task(Of List(Of CoreWebView2Cookie))
        Try
            Return Await WebView2.CoreWebView2.CookieManager.GetCookiesAsync(Uri)
        Catch ex As Exception
            Return New List(Of CoreWebView2Cookie)
        End Try
    End Function

    Public Function GetCookieManager() As CoreWebView2CookieManager
        If WebView2.CoreWebView2 IsNot Nothing Then
            Return WebView2.CoreWebView2.CookieManager
        Else
            Return Nothing
        End If
    End Function

    Private Sub Browser_Load(sender As Object, e As EventArgs) Handles Me.Load
        Main.waveOutSetVolume(0, 0)
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        WebView2.Source = New Uri(ProgramSettings.GetInstance().DefaultWebsite)
    End Sub

    Private Sub CopyUrlButton_Click(sender As Object, e As EventArgs) Handles CopyUrlButton.Click
        Try
            My.Computer.Clipboard.SetText(WebView2.CoreWebView2.Source)
            MsgBox($"copied: ""${WebView2.CoreWebView2.Source}""")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles UrlTextBox.KeyDown
        Try
            If e.KeyCode = Keys.Return Then
                e.SuppressKeyPress = True
                Debug.WriteLine("Start loading: " + Date.Now.ToString)
                WebView2.CoreWebView2.Navigate(UrlTextBox.Text)
            End If

        Catch ex As Exception
            MsgBox("Error in URL", MsgBoxStyle.Critical)
        End Try
    End Sub

End Class

