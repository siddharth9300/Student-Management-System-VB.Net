Imports System.Data.OleDb

Public Class InsertForm
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim saveSQL As String
            Module1.connectDB()

            saveSQL = "INSERT INTO students (rollno, fname, lname, fathername, mobno, altmobno, gender, course, yoa, dob) VALUES (@RollNo, @FName, @LName, @FatherName, @MobNo, @AltMobNo, @Gender, @Course, @yoa, @DOB)"

            Dim stcommand As New OleDb.OleDbCommand(saveSQL, dbcon)
            stcommand.Parameters.AddWithValue("@RollNo", txtRollNo.Text)
            stcommand.Parameters.AddWithValue("@FName", txtFirstName.Text)
            stcommand.Parameters.AddWithValue("@LName", txtLastName.Text)
            stcommand.Parameters.AddWithValue("@FatherName", txtFatherName.Text)
            stcommand.Parameters.AddWithValue("@MobNo", txtMobNo.Text)
            stcommand.Parameters.AddWithValue("@AltMobNo", txtAltMobNo.Text)
            stcommand.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString())
            stcommand.Parameters.AddWithValue("@Course", cmbCourse.SelectedItem.ToString())
            stcommand.Parameters.AddWithValue("@yoa", cmbyoa.SelectedItem.ToString())


            stcommand.Parameters.AddWithValue("@DOB", dtpDOB.Text)

            stcommand.ExecuteNonQuery()
            MsgBox("Record inserted successfully", vbInformation)
            Form1.LoadData()
            ViewForm.LoadData()

            ClearFields()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbExclamation)
        End Try
    End Sub






    Private Sub ClearFields()
        txtRollNo.Text = ""
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtFatherName.Text = ""
        txtMobNo.Text = ""
        txtAltMobNo.Text = ""
        cmbGender.Text = ""
        cmbCourse.Text = ""
        cmbyoa.Text = ""
        dtpDOB.Value = Date.Now
        ' Clear or reset any other fields related to the photo upload if applicable
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearFields()
        Me.Close()
    End Sub


End Class
