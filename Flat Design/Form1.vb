Imports System.Data.OleDb

Public Class Form1
    Dim stReader As OleDb.OleDbDataReader
    Dim stcommand As OleDb.OleDbCommand
    Dim stdataadapter As New OleDb.OleDbDataAdapter
    Dim stDS As DataSet
    Dim rowno As Integer
    Dim reccount As Integer

    Private insertForm As Form
    Private editForm As Form
    Private viewForm As Form

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.connectDB()
        rowno = 0

        LoadData()
    End Sub

    Public Sub LoadData()
        ' Query to get the count of male students
        Dim maleQuery As String = "SELECT COUNT(*) FROM students WHERE gender = 'Male'"
        stcommand = New OleDb.OleDbCommand(maleQuery, dbcon)
        Dim maleCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Query to get the count of female students
        Dim femaleQuery As String = "SELECT COUNT(*) FROM students WHERE gender = 'Female'"
        stcommand = New OleDb.OleDbCommand(femaleQuery, dbcon)
        Dim femaleCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Update the TextBoxes with the counts
        txtMaleStudents.Text = maleCount.ToString()
        txtFemaleStudents.Text = femaleCount.ToString()

        ' Query to get the count of students in BCA course
        Dim bcaQuery As String = "SELECT COUNT(*) FROM students WHERE course = 'BCA'"
        stcommand = New OleDb.OleDbCommand(bcaQuery, dbcon)
        Dim bcaCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Query to get the count of students in MCA course
        Dim mcaQuery As String = "SELECT COUNT(*) FROM students WHERE course = 'MCA'"
        stcommand = New OleDb.OleDbCommand(mcaQuery, dbcon)
        Dim mcaCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Query to get the count of students in IIMCA course
        Dim iimcaQuery As String = "SELECT COUNT(*) FROM students WHERE course = 'IIMCA'"
        stcommand = New OleDb.OleDbCommand(iimcaQuery, dbcon)
        Dim iimcaCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Update the TextBoxes with the course counts
        txtBCAStudents.Text = bcaCount.ToString()
        txtMCAStudents.Text = mcaCount.ToString()
        txtIIMCAStudents.Text = iimcaCount.ToString()


        ' Calculate the number of students in each year of their respective courses
        Dim currentYear As Integer = 2023

        ' Query to get the count of students studying in the 1st year
        Dim firstYearQuery As String = "SELECT COUNT(*) FROM students WHERE yoa = '" & currentYear.ToString() & "'"
        stcommand = New OleDb.OleDbCommand(firstYearQuery, dbcon)
        Dim firstYearCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Query to get the count of students studying in the 2nd year
        Dim secondYearQuery As String = "SELECT COUNT(*) FROM students WHERE yoa = '" & (currentYear - 1).ToString() & "'"
        stcommand = New OleDb.OleDbCommand(secondYearQuery, dbcon)
        Dim secondYearCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Query to get the count of students studying in the 3rd year
        Dim thirdYearQuery As String = "SELECT COUNT(*) FROM students WHERE yoa = '" & (currentYear - 2).ToString() & "'"
        stcommand = New OleDb.OleDbCommand(thirdYearQuery, dbcon)
        Dim thirdYearCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Query to get the count of students studying in the 4th year
        Dim fourthYearQuery As String = "SELECT COUNT(*) FROM students WHERE yoa = '" & (currentYear - 3).ToString() & "'"
        stcommand = New OleDb.OleDbCommand(fourthYearQuery, dbcon)
        Dim fourthYearCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Query to get the count of students studying in the 5th year
        Dim fifthYearQuery As String = "SELECT COUNT(*) FROM students WHERE yoa = '" & (currentYear - 4).ToString() & "'"
        stcommand = New OleDb.OleDbCommand(fifthYearQuery, dbcon)
        Dim fifthYearCount As Integer = CInt(stcommand.ExecuteScalar())

        ' Update the TextBoxes with the counts
        txt1stYear.Text = firstYearCount.ToString()
        txt2ndYear.Text = secondYearCount.ToString()
        txt3rdYear.Text = thirdYearCount.ToString()
        txt4thYear.Text = fourthYearCount.ToString()
        txt5thYear.Text = fifthYearCount.ToString()

    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Me.Close()
        Application.Exit()
    End Sub


    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        If insertForm Is Nothing OrElse insertForm.IsDisposed Then
            insertForm = New InsertForm()
        End If



        insertForm.TopLevel = False
        Panel5.Controls.Add(insertForm)
        insertForm.BringToFront()
        insertForm.Show()
    End Sub







    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        If insertForm IsNot Nothing AndAlso Not insertForm.IsDisposed Then
            insertForm.Hide()
        End If

        If editForm IsNot Nothing AndAlso Not editForm.IsDisposed Then
            editForm.Hide()
        End If

        If viewForm IsNot Nothing AndAlso Not viewForm.IsDisposed Then
            viewForm.Hide()
        End If
        LoadData()
        ' Show the main panel
        Panel5.Visible = True

    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        If viewForm Is Nothing OrElse viewForm.IsDisposed Then
            viewForm = New ViewForm()
        End If

        viewForm.TopLevel = False
        Panel5.Controls.Add(viewForm)
        viewForm.BringToFront()
        viewForm.Show()

        If Panel5.Controls.Contains(viewForm) Then
            DirectCast(viewForm, ViewForm).RefreshData()
        End If

    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim url As String = "https://github.com/siddharth9300/Student-Management-System-VB.Net/blob/master/Siddharth%20Jain%20Project%20Report.pdf"  ' Replace with your desired web link
        System.Diagnostics.Process.Start(url)
    End Sub
End Class

