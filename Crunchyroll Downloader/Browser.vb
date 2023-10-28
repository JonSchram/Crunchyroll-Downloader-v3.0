Option Strict On
Imports System.Net
Imports Crunchyroll_Downloader.settings.general
Imports Crunchyroll_Downloader.ui
Imports Microsoft.Web.WebView2.Core
Imports SiteAPI.api

Public Class Browser
    Implements ICookieProvider

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

    Delegate Function GetCookieFunction(Uri As String) As Task(Of List(Of Cookie))

    Public Async Function GetCookies(Uri As String) As Task(Of List(Of Cookie)) Implements ICookieProvider.GetCookies
        If InvokeRequired Then
            Dim getCookiesFn As GetCookieFunction = AddressOf GetCookies
            Dim task = CType(Invoke(getCookiesFn, Uri), Task(Of List(Of Cookie)))
            Return Await task
        Else
            Try
                Return Await GetCookiesInner(Uri)
            Catch ex As Exception
                Debug.WriteLine("Error getting cookies from browser.")
                Debug.WriteLine(ex)
                Return New List(Of Cookie)
            End Try
        End If
    End Function

    ''' <summary>
    ''' Must be run on the UI thread.
    ''' </summary>
    ''' <param name="Uri"></param>
    ''' <returns></returns>
    Private Async Function GetCookiesInner(Uri As String) As Task(Of List(Of Cookie))
        Return ConvertCookies(Await WebView2.CoreWebView2.CookieManager.GetCookiesAsync(Uri))
    End Function

    ''' <summary>
    ''' Must be run on the UI thread.
    ''' </summary>
    ''' <param name="webView2Cookies"></param>
    ''' <returns></returns>
    Private Function ConvertCookies(webView2Cookies As List(Of CoreWebView2Cookie)) As List(Of Cookie)
        Dim result As New List(Of Cookie)
        For Each cookie In webView2Cookies
            result.Add(cookie.ToSystemNetCookie())
        Next
        Return result
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

