Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status

Public Class ViewForm


    Private editForm As Form

    Dim stdataadapter As New OleDb.OleDbDataAdapter
    Private Sub ViewForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.connectDB()
        'LoadData()
        RefreshData()
        DataGridView1.AutoGenerateColumns = True
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill



        Dim headerStyle As DataGridViewCellStyle = New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(255, 192, 192, 255) ' Set the background color of column headers
        headerStyle.ForeColor = SystemColors.WindowText ' Set the text color of column headers
        headerStyle.SelectionBackColor = SystemColors.Highlight ' Set the background color of selected column headers
        headerStyle.SelectionForeColor = SystemColors.HighlightText ' Set the text color of selected column headers
        headerStyle.Font = New Font("Century Gothic", 14.25, FontStyle.Bold) ' Set the font of column headers
        headerStyle.WrapMode = DataGridViewTriState.True ' Enable text wrapping in column headers
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft ' Set the alignment of column headers

        DataGridView1.ColumnHeadersDefaultCellStyle = headerStyle

    End Sub



    Public Sub RefreshData()
        LoadData()
    End Sub
    Public Sub LoadData()

        stdataadapter = New OleDb.OleDbDataAdapter("SELECT * FROM students", dbcon)

        Dim stDS As New DataSet
        stdataadapter.Fill(stDS, "students")

        DataGridView1.DataSource = stDS.Tables("students")
    End Sub

    Public Sub LoadSData(searchQuery As String)
        Dim query As String = "SELECT * FROM students"
        If Not String.IsNullOrWhiteSpace(searchQuery) Then
            query += " WHERE rollno = '" + searchQuery + "' OR fname LIKE '%" + searchQuery + "%'"
        End If

        stdataadapter = New OleDb.OleDbDataAdapter(query, dbcon)
        Dim stDS As New DataSet
        stdataadapter.Fill(stDS, "students")

        DataGridView1.DataSource = stDS.Tables("students")
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim rollNo As String = DataGridView1.SelectedRows(0).Cells("rollno").Value.ToString()

            If editForm Is Nothing OrElse editForm.IsDisposed Then
                editForm = New EditForm(rollNo)
            Else
                DirectCast(editForm, EditForm).UpdateRollNo(rollNo)
            End If

            editForm.TopLevel = False

            Form1.Panel5.Controls.Add(editForm)
            editForm.BringToFront()
            editForm.Show()
            AddHandler editForm.FormClosed, AddressOf EditFormClosed ' Attach event handler for EditForm closing


        Else
            MsgBox("Please select a row to edit", vbExclamation)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim rollNo As String = selectedRow.Cells("rollno").Value.ToString()

            If MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Module1.connectDB()
                Dim delquery As String = "DELETE FROM students WHERE rollno=@rollno"
                Dim stcommand As New OleDb.OleDbCommand(delquery, dbcon)
                stcommand.Parameters.AddWithValue("@rollno", rollNo)
                stcommand.ExecuteNonQuery()
                dbcon.Close()
                MsgBox("Record deleted successfully", vbInformation)

                LoadData() ' Reload the data after deleting the record
            End If
        Else
            MsgBox("Please select a row to delete", vbExclamation)
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim searchQuery As String = txtSearch.Text.Trim()

        ' Call the LoadData method with the search query
        LoadSData(searchQuery)
    End Sub

    Private Sub EditFormClosed(sender As Object, e As FormClosedEventArgs)
        LoadData() ' Reload the data after closing the EditForm
    End Sub


End Class
