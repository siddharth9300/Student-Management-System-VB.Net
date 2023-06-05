Imports System.Data.OleDb

Module Module1
    Public dbcon As New OleDb.OleDbConnection
    Public Sub connectDB()
        If dbcon.State = ConnectionState.Closed Then
            dbcon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; data source=D:\PROGRAMING\Visual Basic\Flat Design\Flat Design\Flat Design\studentDB.accdb;Persist Security Info=False;"
            dbcon.Open()
        End If
    End Sub

End Module

