Public Class Options
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.char1 = TextBox1.Text
        Form1.char2 = TextBox2.Text
        Form1.num1 = CInt(TextBox3.Text)
        If (RadioButton1.Checked = True) Then
            Form1.useWith = "Alt"
        End If

        If (RadioButton2.Checked = True) Then
            Form1.useWith = "Ctrl"
        End If

        If (RadioButton3.Checked = True) Then
            Form1.useWith = "Shift"
        End If

        If (RadioButton4.Checked = True) Then
            Form1.useWith = "None"
        End If

        Dim file As String = Application.StartupPath() & "\data.txt"
        If System.IO.File.Exists(file) = False Then
            System.IO.File.Create(file).Dispose()
        End If

        Dim objWriter As New System.IO.StreamWriter(file, False)
        objWriter.WriteLine(TextBox1.Text)
        objWriter.WriteLine(TextBox2.Text)
        objWriter.WriteLine(TextBox3.Text)
        objWriter.WriteLine(Form1.useWith)
        objWriter.Close()

        Me.Close()
    End Sub

    Private Sub Options_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = Form1.char1
        TextBox2.Text = Form1.char2
        TextBox3.Text = Form1.num1.ToString()

        If (Form1.useWith = "Alt") Then
            RadioButton1.Checked = True
        End If
        If (Form1.useWith = "Ctrl") Then
            RadioButton2.Checked = True
        End If
        If (Form1.useWith = "Shift") Then
            RadioButton3.Checked = True
        End If
        If (Form1.useWith = "None") Then
            RadioButton4.Checked = True
        End If

    End Sub
End Class