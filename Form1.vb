Public Class Form1
    ' Declare a list to store tasks
    Private taskList As New List(Of String)

    ' Load event of the form
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up DataGridView columns
        dgvTasks.Columns.Add("Task", "Task")
        dgvTasks.AllowUserToAddRows = False ' Prevent the empty row from appearing
    End Sub

    ' Add Task
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim task As String = txtTask.Text.Trim()

        If task <> "" Then
            taskList.Add(task) ' Add task to the list
            UpdateGridView() ' Update the DataGridView
            txtTask.Clear() ' Clear the input textbox
        Else
            MessageBox.Show("Please enter a task to add.")
        End If
    End Sub

    ' Update Task (Only when the button is clicked)
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If dgvTasks.CurrentRow IsNot Nothing AndAlso dgvTasks.CurrentRow.Index >= 0 Then
            Dim index As Integer = dgvTasks.CurrentRow.Index
            Dim updatedTask As String = txtTask.Text.Trim()

            If updatedTask <> "" Then
                taskList(index) = updatedTask ' Update the task in the list
                UpdateGridView() ' Update the DataGridView
                txtTask.Clear()
            Else
                MessageBox.Show("Please enter a task to update.")
            End If
        Else
            MessageBox.Show("Please select a task to update.")
        End If
    End Sub

    ' Delete Task
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvTasks.CurrentRow IsNot Nothing AndAlso dgvTasks.CurrentRow.Index >= 0 Then
            Dim index As Integer = dgvTasks.CurrentRow.Index
            taskList.RemoveAt(index) ' Remove task from the list
            UpdateGridView() ' Update the DataGridView
        Else
            MessageBox.Show("Please select a task to delete.")
        End If
    End Sub

    ' Clear all tasks
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        taskList.Clear() ' Clear the list
        UpdateGridView() ' Clear the DataGridView
        txtTask.Clear()
    End Sub

    ' Update the DataGridView
    Private Sub UpdateGridView()
        dgvTasks.Rows.Clear()

        For Each task As String In taskList
            dgvTasks.Rows.Add(task)
        Next
    End Sub

    ' Do not auto-populate the TextBox when a row is selected
    Private Sub dgvTasks_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTasks.SelectionChanged
        ' No action here to prevent auto-populating the TextBox
    End Sub
End Class

