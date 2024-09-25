Imports System.Data.OleDb

Public Class Form1
    Private connectionString As String = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=ToDoList.accdb;Persist Security Info=False;"


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up DataGridView columns
        dgvTasks.Columns.Add("TaskDescription", "Task")
        Dim checkBoxColumn As New DataGridViewCheckBoxColumn()
        checkBoxColumn.HeaderText = "Done"
        checkBoxColumn.Name = "IsDone"
        dgvTasks.Columns.Add(checkBoxColumn)
        dgvTasks.AllowUserToAddRows = False ' Prevent adding empty rows

        LoadTasksFromDatabase()
    End Sub

    ' Add Task to Database
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim taskDescription As String = txtTask.Text.Trim()

        If taskDescription <> "" Then
            Using conn As New OleDbConnection(connectionString)
                Dim query As String = "INSERT INTO Tasks (TaskDescription, IsDone) VALUES (@TaskDescription, False)"
                Using cmd As New OleDbCommand(query, conn)
                    cmd.Parameters.AddWithValue("@TaskDescription", taskDescription)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            txtTask.Clear()
            LoadTasksFromDatabase()
        Else
            MessageBox.Show("Please enter a task.")
        End If
    End Sub

    ' Load Tasks from Database
    Private Sub LoadTasksFromDatabase()
        dgvTasks.Rows.Clear()
        Using conn As New OleDbConnection(connectionString)
            Dim query As String = "SELECT TaskDescription, IsDone FROM Tasks"
            Using cmd As New OleDbCommand(query, conn)
                conn.Open()
                Using reader As OleDbDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim taskDescription As String = reader("TaskDescription").ToString()
                        Dim isDone As Boolean = CBool(reader("IsDone"))
                        dgvTasks.Rows.Add(taskDescription, isDone)
                    End While
                End Using
            End Using
        End Using
    End Sub

    ' Update Task in Database
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If dgvTasks.CurrentRow IsNot Nothing AndAlso dgvTasks.CurrentRow.Index >= 0 Then
            Dim taskDescription As String = txtTask.Text.Trim()
            If taskDescription <> "" Then
                Dim oldTaskDescription As String = dgvTasks.CurrentRow.Cells("TaskDescription").Value.ToString()
                Dim query As String = "UPDATE Tasks SET TaskDescription = @NewTaskDescription WHERE TaskDescription = @OldTaskDescription"
                Using conn As New OleDbConnection(connectionString)
                    Using cmd As New OleDbCommand(query, conn)
                        cmd.Parameters.AddWithValue("@NewTaskDescription", taskDescription)
                        cmd.Parameters.AddWithValue("@OldTaskDescription", oldTaskDescription)
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                txtTask.Clear()
                LoadTasksFromDatabase()
            Else
                MessageBox.Show("Please enter a task to update.")
            End If
        Else
            MessageBox.Show("Please select a task to update.")
        End If
    End Sub

    ' Delete Task from Database
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvTasks.CurrentRow IsNot Nothing AndAlso dgvTasks.CurrentRow.Index >= 0 Then
            Dim taskDescription As String = dgvTasks.CurrentRow.Cells("TaskDescription").Value.ToString()
            Dim query As String = "DELETE FROM Tasks WHERE TaskDescription = @TaskDescription"
            Using conn As New OleDbConnection(connectionString)
                Using cmd As New OleDbCommand(query, conn)
                    cmd.Parameters.AddWithValue("@TaskDescription", taskDescription)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            LoadTasksFromDatabase()
        Else
            MessageBox.Show("Please select a task to delete.")
        End If
    End Sub

    ' Clear All Tasks from Database
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim query As String = "DELETE FROM Tasks"
        Using conn As New OleDbConnection(connectionString)
            Using cmd As New OleDbCommand(query, conn)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
        LoadTasksFromDatabase()
        txtTask.Clear()
    End Sub

    ' Mark Task as Done or Not Done in Database
    Private Sub dgvTasks_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTasks.CellContentClick
        If e.ColumnIndex = dgvTasks.Columns("IsDone").Index AndAlso e.RowIndex >= 0 Then
            Dim taskDescription As String = dgvTasks.Rows(e.RowIndex).Cells("TaskDescription").Value.ToString()
            Dim isDone As Boolean = CBool(dgvTasks.Rows(e.RowIndex).Cells("IsDone").Value)
            Dim query As String = "UPDATE Tasks SET IsDone = @IsDone WHERE TaskDescription = @TaskDescription"
            Using conn As New OleDbConnection(connectionString)
                Using cmd As New OleDbCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IsDone", Not isDone) ' Toggle the completion status
                    cmd.Parameters.AddWithValue("@TaskDescription", taskDescription)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            LoadTasksFromDatabase()
        End If
    End Sub
End Class
