Imports System.IO
Imports System.Text
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dateiname As String = String.Empty
        Dim enc As Encoding = Encoding.Default
        Dim csv As List(Of String) = New List(Of String)
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            dateiname = OpenFileDialog1.FileName
            Label1.Text = dateiname
        End If

        Dim query = From Line In File.ReadAllLines(dateiname, enc)
                    Let fields = Line.Split(";")
                    Select fields

        Dim customer = query.ToList()

        Label2.Text = customer.Count.ToString
        ProgressBar1.Maximum = customer.Count

        Dim ColumnCount = customer(0).Count
        For i As Integer = 0 To ColumnCount - 1
            Debug.Print(customer(0).ElementAt(i).ToString)
            Dim ch As New ColumnHeader
            ch.Name = customer(0).ElementAt(i)
            ch.Text = customer(0).ElementAt(i)
            ListView1.Columns.Add(ch)
        Next

        For j As Integer = 1 To customer.Count - 1
            Dim listItem As New ListViewItem(customer(j).ElementAt(0))
            For k As Integer = 1 To ColumnCount - 1
                listItem.SubItems.Add(customer(j).ElementAt(k))
            Next
            ListView1.Items.Add(listItem)
            ProgressBar1.Value += 1
        Next

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView1.View = View.Details
        ListView1.AllowColumnReorder = True
        ListView1.GridLines = True
    End Sub
End Class
