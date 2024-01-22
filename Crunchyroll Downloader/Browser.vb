Option Strict On
Imports System.Net
Imports Crunchyroll_Downloader.settings.general
Imports Crunchyroll_Downloader.ui
Imports Microsoft.Web.WebView2.Core
Imports SiteAPI.api

Public Class Browser
    Implements IInteractiveCookieProvider

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


    Public Async Function RequestCookies(uri As String) As Task(Of List(Of Cookie)) Implements IInteractiveCookieProvider.RequestCookies
        ' Ensure any current navigation is stopped before starting a new navigation, don't want to handle the wrong page loaded event.
        WebView2.Stop()

        Dim request As New CookieRequest(uri, Me)
        Return Await request.BeginRequest()
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

    ''' <summary>
    ''' Helper class to monitor for a page load.
    ''' </summary>
    Private Class CookieRequest
        Private ReadOnly uri As String
        Private ReadOnly parent As Browser

        Private requestSource As TaskCompletionSource(Of List(Of Cookie))

        Public Sub New(uri As String, parent As Browser)
            Me.uri = uri
            Me.parent = parent
        End Sub

        Public Async Function BeginRequest() As Task(Of List(Of Cookie))
            If requestSource Is Nothing Then
                AddHandler parent.WebView2.NavigationCompleted, AddressOf HandlePageLoaded
                Await parent.WebView2.EnsureCoreWebView2Async()
                parent.Navigate(uri)

                requestSource = New TaskCompletionSource(Of List(Of Cookie))()
            End If

            Return Await requestSource.Task
        End Function

        Private Async Sub HandlePageLoaded(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)
            RemoveHandler parent.WebView2.NavigationCompleted, AddressOf HandlePageLoaded

            If e.IsSuccess Then
                Dim loadedCookies = Await parent.GetCookies(uri)
                requestSource.SetResult(loadedCookies)
            Else
                requestSource.SetException(New Exception("Error navigating to page"))
            End If
        End Sub
    End Class
End Class

