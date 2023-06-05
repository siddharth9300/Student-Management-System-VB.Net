Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status

Public Class EditForm
    Private rollNo As String

    Public Sub New(rollNo As String)
        InitializeComponent()
        Me.rollNo = rollNo
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim selectSQL As String = "SELECT * FROM students WHERE rollno = @rollno"
        Module1.connectDB()
        Dim stcommand As New OleDb.OleDbCommand(selectSQL, dbcon)
        stcommand.Parameters.AddWithValue("@rollno", rollNo)
        Dim stReader As OleDb.OleDbDataReader = stcommand.ExecuteReader()

        If stReader.Read() Then
            txtRollNo.Text = stReader("rollno").ToString()
            txtFirstName.Text = stReader("fname").ToString()
            txtLastName.Text = stReader("lname").ToString()
            txtFatherName.Text = stReader("fathername").ToString()
            txtMobNo.Text = stReader("mobno").ToString()
            txtAltMobNo.Text = stReader("altmobno").ToString()
            cmbGender.SelectedItem = stReader("gender").ToString()
            cmbCourse.SelectedItem = stReader("course").ToString()
            cmbyoa.SelectedItem = stReader("yoa").ToString()
            dtpDOB.Value = DateTime.Parse(stReader("dob").ToString())
        End If

        stReader.Close()

    End Sub

    Public Sub UpdateRollNo(rollNo As String)
        Me.rollNo = rollNo
        LoadData()
    End Sub


 

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim updateSQL As String = "UPDATE students SET fname = @fname, lname = @lname, fathername = @fathername, mobno = @mobno, altmobno = @altmobno, gender = @gender, course = @course, yoa = @yoa, dob = @dob WHERE rollno = @rollno"
            Module1.connectDB()
            Dim stcommand As New OleDb.OleDbCommand(updateSQL, dbcon)
            stcommand.Parameters.AddWithValue("@fname", txtFirstName.Text)
            stcommand.Parameters.AddWithValue("@lname", txtLastName.Text)
            stcommand.Parameters.AddWithValue("@fathername", txtFatherName.Text)
            stcommand.Parameters.AddWithValue("@mobno", txtMobNo.Text)
            stcommand.Parameters.AddWithValue("@altmobno", txtAltMobNo.Text)
            stcommand.Parameters.AddWithValue("@gender", cmbGender.SelectedItem.ToString())
            stcommand.Parameters.AddWithValue("@course", cmbCourse.SelectedItem.ToString())
            stcommand.Parameters.AddWithValue("@yoa", cmbyoa.SelectedItem.ToString())
            stcommand.Parameters.AddWithValue("@dob", dtpDOB.Text)
            stcommand.Parameters.AddWithValue("@rollno", rollNo)
            stcommand.ExecuteNonQuery()

            MsgBox("Record updated successfully", vbInformation)
            Form1.LoadData()

            Me.Close()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbExclamation)
        End Try
    End Sub


End Class
