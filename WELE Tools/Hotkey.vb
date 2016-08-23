Public Class Hotkey
#Region "Declarations - WinAPI, Hotkey constant and Modifier Enum"
    ''' <summary>
    ''' Declaration of winAPI function wrappers. The winAPI functions are used to register / unregister a hotkey
    ''' </summary>
    Public Declare Function RegisterHotKey Lib "user32" _
        (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer

    Public Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    Public Const WM_HOTKEY As Integer = &H312

    Enum KeyModifier
        None = 0
        Alt = &H1
        Control = &H2
        Shift = &H4
        Winkey = &H8
    End Enum 'This enum is just to make it easier to call the registerHotKey function: The modifier integer codes are replaced by a friendly "Alt","Shift" etc.
#End Region


#Region "Hotkey registration, unregistration and handling"
    Public Shared Sub registerHotkey(ByRef sourceForm As Form, ByVal hotkeyID As Integer, ByVal triggerKey As String, ByVal modifier As KeyModifier)
        RegisterHotKey(sourceForm.Handle, hotkeyID, modifier, Asc(triggerKey.ToUpper))
    End Sub

    Public Shared Sub unregisterHotkeys(ByRef sourceForm As Form)
        UnregisterHotKey(sourceForm.Handle, 1)  'Remember to call unregisterHotkeys() when closing your application.
        UnregisterHotKey(sourceForm.Handle, 2)
    End Sub
    Public Shared Sub handleHotKeyEvent(ByVal hotkeyID As IntPtr)
        Select Case hotkeyID
            Case 1
                If (Form1.AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying) Then
                    Form1.AxWindowsMediaPlayer1.Ctlcontrols.pause()
                Else
                    Form1.AxWindowsMediaPlayer1.Ctlcontrols.play()
                End If
            Case 2
                Form1.AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = Convert.ToDouble(Math.Max(0, Form1.AxWindowsMediaPlayer1.Ctlcontrols.currentPosition - Form1.num1))
                Form1.AxWindowsMediaPlayer1.Ctlcontrols.play()
        End Select
    End Sub
#End Region
End Class
