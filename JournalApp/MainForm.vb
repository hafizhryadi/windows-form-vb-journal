''' <summary>
''' Main Form for the Journal Application
''' Demonstrates Select Case, loops, and Windows Forms controls
''' </summary>
Public Class MainForm
    Private currentEntries As List(Of JournalEntry)
    
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize database
        DatabaseHelper.InitializeDatabase()
        
        ' Initialize arrays
        ArrayManager.InitializeArrays(10)
        
        ' Load all entries
        LoadEntries()
        
        ' Set up DataGridView
        SetupDataGridView()
    End Sub
    
    ''' <summary>
    ''' Sets up the DataGridView columns (Procedure)
    ''' </summary>
    Private Sub SetupDataGridView()
        dgvEntries.AutoGenerateColumns = False
        dgvEntries.Columns.Clear()
        
        ' Add columns
        dgvEntries.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Id",
            .HeaderText = "ID",
            .DataPropertyName = "Id",
            .Width = 50,
            .Visible = False
        })
        
        dgvEntries.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "EntryDate",
            .HeaderText = "Date",
            .DataPropertyName = "EntryDate",
            .Width = 100,
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "yyyy-MM-dd"}
        })
        
        dgvEntries.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Title",
            .HeaderText = "Title",
            .DataPropertyName = "Title",
            .Width = 200
        })
        
        dgvEntries.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Description",
            .HeaderText = "Description",
            .DataPropertyName = "Description",
            .Width = 300
        })
        
        dgvEntries.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvEntries.MultiSelect = False
    End Sub
    
    ''' <summary>
    ''' Loads all journal entries from database (Procedure)
    ''' </summary>
    Private Sub LoadEntries()
        currentEntries = DatabaseHelper.GetAllEntries()
        dgvEntries.DataSource = Nothing
        dgvEntries.DataSource = currentEntries
        
        ' Update status
        UpdateStatusLabel()
        
        ' Load into arrays for demonstration
        ArrayManager.LoadEntriesIntoArrays()
    End Sub
    
    ''' <summary>
    ''' Updates the status label (Procedure)
    ''' </summary>
    Private Sub UpdateStatusLabel()
        Dim count As Integer = If(currentEntries IsNot Nothing, currentEntries.Count, 0)
        lblStatus.Text = $"Total Entries: {count} | Array Count: {ArrayManager.GetCount()}"
    End Sub
    
    ''' <summary>
    ''' Button click handler - uses Select Case for menu navigation
    ''' </summary>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        HandleMenuAction("Add")
    End Sub
    
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        HandleMenuAction("Edit")
    End Sub
    
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        HandleMenuAction("Delete")
    End Sub
    
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        HandleMenuAction("Refresh")
    End Sub
    
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        HandleMenuAction("Search")
    End Sub
    
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        HandleMenuAction("ClearSearch")
    End Sub
    
    Private Sub btnViewArrays_Click(sender As Object, e As EventArgs) Handles btnViewArrays.Click
        HandleMenuAction("ViewArrays")
    End Sub
    
    ''' <summary>
    ''' Handles menu actions using Select Case statement (Procedure)
    ''' Demonstrates Select Case usage
    ''' </summary>
    Private Sub HandleMenuAction(action As String)
        Select Case action
            Case "Add"
                ' Open form to add new entry
                Dim entryForm As New EntryForm()
                If entryForm.ShowDialog() = DialogResult.OK Then
                    LoadEntries()
                End If
                
            Case "Edit"
                ' Edit selected entry
                If dgvEntries.SelectedRows.Count > 0 Then
                    Dim selectedEntry As JournalEntry = CType(dgvEntries.SelectedRows(0).DataBoundItem, JournalEntry)
                    Dim entryForm As New EntryForm(selectedEntry)
                    If entryForm.ShowDialog() = DialogResult.OK Then
                        LoadEntries()
                    End If
                Else
                    MessageBox.Show("Please select an entry to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                
            Case "Delete"
                ' Delete selected entry
                If dgvEntries.SelectedRows.Count > 0 Then
                    Dim selectedEntry As JournalEntry = CType(dgvEntries.SelectedRows(0).DataBoundItem, JournalEntry)
                    Dim result As DialogResult = MessageBox.Show($"Are you sure you want to delete the entry '{selectedEntry.Title}'?",
                                                                  "Confirm Delete",
                                                                  MessageBoxButtons.YesNo,
                                                                  MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        If DatabaseHelper.DeleteEntry(selectedEntry.Id) Then
                            MessageBox.Show("Entry deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            LoadEntries()
                        End If
                    End If
                Else
                    MessageBox.Show("Please select an entry to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                
            Case "Refresh"
                ' Refresh the list
                LoadEntries()
                MessageBox.Show("Entries refreshed.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information)
                
            Case "Search"
                ' Search entries
                PerformSearch()
                
            Case "ClearSearch"
                ' Clear search and reload all entries
                txtSearch.Text = ""
                dtpSearch.Value = Date.Today
                chkSearchByDate.Checked = False
                LoadEntries()
                
            Case "ViewArrays"
                ' View array contents
                ShowArrayContents()
                
            Case Else
                MessageBox.Show("Unknown action.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
    End Sub
    
    ''' <summary>
    ''' Performs search operation (Procedure with loops)
    ''' </summary>
    Private Sub PerformSearch()
        Dim searchText As String = txtSearch.Text.Trim()
        Dim searchDate As Date? = If(chkSearchByDate.Checked, CType(dtpSearch.Value.Date, Date?), Nothing)
        
        If String.IsNullOrEmpty(searchText) AndAlso Not searchDate.HasValue Then
            MessageBox.Show("Please enter search text or select a date.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        
        ' Search using database
        currentEntries = DatabaseHelper.SearchEntries(searchText, searchDate)
        dgvEntries.DataSource = Nothing
        dgvEntries.DataSource = currentEntries
        
        UpdateStatusLabel()
        
        MessageBox.Show($"Found {currentEntries.Count} matching entries.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    
    ''' <summary>
    ''' Shows array contents in a message box (Procedure with loops)
    ''' Demonstrates array iteration
    ''' </summary>
    Private Sub ShowArrayContents()
        Dim message As String = "Array Contents:" & vbCrLf & vbCrLf
        
        ' Show one-dimensional array (titles)
        message &= "One-Dimensional Array (Titles):" & vbCrLf
        Dim titles() As String = ArrayManager.GetAllTitles()
        
        If titles.Length > 0 Then
            ' Loop through titles array
            For i As Integer = 0 To Math.Min(titles.Length - 1, 4) ' Show first 5
                message &= $"  [{i}] {titles(i)}" & vbCrLf
            Next
            
            If titles.Length > 5 Then
                message &= $"  ... and {titles.Length - 5} more" & vbCrLf
            End If
        Else
            message &= "  (empty)" & vbCrLf
        End If
        
        message &= vbCrLf & "Multi-Dimensional Array (Entry Data):" & vbCrLf
        
        ' Show multi-dimensional array data
        Dim formattedData As List(Of String) = ArrayManager.GetAllEntryDataFormatted()
        
        If formattedData.Count > 0 Then
            ' Loop through formatted data
            For i As Integer = 0 To Math.Min(formattedData.Count - 1, 2) ' Show first 3
                message &= $"  [{i}] {formattedData(i)}" & vbCrLf
            Next
            
            If formattedData.Count > 3 Then
                message &= $"  ... and {formattedData.Count - 3} more" & vbCrLf
            End If
        Else
            message &= "  (empty)" & vbCrLf
        End If
        
        message &= vbCrLf & $"Total count in arrays: {ArrayManager.GetCount()}"
        
        MessageBox.Show(message, "Array Contents", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
