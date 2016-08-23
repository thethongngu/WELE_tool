Public Class Form1
    Public char1 As String = "3"
    Public char2 As String = "4"
    Public num1 As Integer = 10
    Public useWith As String = "None"


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If (OpenFileDialog1.ShowDialog = DialogResult.OK) Then
            AxWindowsMediaPlayer1.URL = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Options.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (Button1.Text = "Start") Then
            Button1.Text = "Stop"
            Button3.Enabled = False
            Button4.Enabled = False

            If (useWith = "Alt") Then
                Hotkey.registerHotkey(Me, 1, char1, Hotkey.KeyModifier.Alt)
                Hotkey.registerHotkey(Me, 2, char2, Hotkey.KeyModifier.Alt)
            End If

            If (useWith = "Ctrl") Then
                Hotkey.registerHotkey(Me, 1, char1, Hotkey.KeyModifier.Control)
                Hotkey.registerHotkey(Me, 2, char2, Hotkey.KeyModifier.Control)
            End If

            If (useWith = "Shift") Then
                Hotkey.registerHotkey(Me, 1, char1, Hotkey.KeyModifier.Shift)
                Hotkey.registerHotkey(Me, 2, char2, Hotkey.KeyModifier.Shift)
            End If

            If (useWith = "None") Then
                Hotkey.registerHotkey(Me, 1, char1, Hotkey.KeyModifier.None)
                Hotkey.registerHotkey(Me, 2, char2, Hotkey.KeyModifier.None)
            End If
        Else
            Button1.Text = "Start"
            Button3.Enabled = True
            Button4.Enabled = True
            Hotkey.UnregisterHotKey(Me.Handle, 1)
            Hotkey.UnregisterHotKey(Me.Handle, 2)
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Console.WriteLine(Application.StartupPath())

        Dim file As String = Application.StartupPath() & "\data.txt"
        If System.IO.File.Exists(file) = True Then
            Dim objReader As New System.IO.StreamReader(file)
            char1 = objReader.ReadLine()
            Options.TextBox1.Text = char1

            char2 = objReader.ReadLine()
            Options.TextBox2.Text = char2

            num1 = CInt(objReader.ReadLine())
            Options.TextBox3.Text = num1.ToString()

            useWith = objReader.ReadLine()
            If (useWith = "Alt") Then
                Options.RadioButton1.Checked = True
            End If
            If (useWith = "Ctrl") Then
                Options.RadioButton2.Checked = True
            End If
            If (useWith = "Shift") Then
                Options.RadioButton3.Checked = True
            End If
            If (useWith = "None") Then
                Options.RadioButton4.Checked = True
            End If

            objReader.Close()
        Else
            Dim objWriter As New System.IO.StreamWriter(file, False)
            objWriter.WriteLine(char1)
            objWriter.WriteLine(char2)
            objWriter.WriteLine(num1)
            objWriter.WriteLine(useWith)
            Options.TextBox1.Text = char1
            Options.TextBox2.Text = char2
            Options.TextBox3.Text = num1.ToString()

            If (useWith = "Alt") Then
                Options.RadioButton1.Checked = True
            End If
            If (useWith = "Ctrl") Then
                Options.RadioButton2.Checked = True
            End If
            If (useWith = "Shift") Then
                Options.RadioButton3.Checked = True
            End If
            If (useWith = "None") Then
                Options.RadioButton4.Checked = True
            End If

            objWriter.Close()
        End If
    End Sub

    Private Sub Form_Closed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Hotkey.unregisterHotkeys(Me)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = Hotkey.WM_HOTKEY Then
            Hotkey.handleHotKeyEvent(m.WParam)
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If AxWindowsMediaPlayer1.settings.isAvailable("Rate") Then
            AxWindowsMediaPlayer1.settings.rate = NumericUpDown1.Value + 1
        End If

    End Sub
End Class
