''' <summary>
''' Form for adding or editing journal entries
''' </summary>
Public Class EntryForm
    Private currentEntry As JournalEntry
    Private isEditMode As Boolean = False
    
    ''' <summary>
    ''' Constructor for adding new entry
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        
        currentEntry = New JournalEntry()
        isEditMode = False
        Me.Text = "Add New Journal Entry"
    End Sub
    
    ''' <summary>
    ''' Constructor for editing existing entry
    ''' </summary>
    Public Sub New(entry As JournalEntry)
        InitializeComponent()
        
        currentEntry = entry
        isEditMode = True
        Me.Text = "Edit Journal Entry"
        
        ' Load entry data
        LoadEntryData()
    End Sub
    
    ''' <summary>
    ''' Loads entry data into form controls (Procedure)
    ''' </summary>
    Private Sub LoadEntryData()
        dtpDate.Value = currentEntry.EntryDate
        txtTitle.Text = currentEntry.Title
        txtDescription.Text = currentEntry.Description
    End Sub
    
    ''' <summary>
    ''' Validates form input (Function returning Boolean)
    ''' </summary>
    Private Function ValidateInput() As Boolean
        If String.IsNullOrWhiteSpace(txtTitle.Text) Then
            MessageBox.Show("Please enter a title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTitle.Focus()
            Return False
        End If
        
        If txtTitle.Text.Length > 200 Then
            MessageBox.Show("Title cannot exceed 200 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTitle.Focus()
            Return False
        End If
        
        Return True
    End Function
    
    ''' <summary>
    ''' Save button click handler (Procedure)
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateInput() Then
            Return
        End If
        
        ' Update entry object
        currentEntry.EntryDate = dtpDate.Value.Date
        currentEntry.Title = txtTitle.Text.Trim()
        currentEntry.Description = txtDescription.Text.Trim()
        
        ' Save to database
        Dim success As Boolean = False
        
        If isEditMode Then
            success = DatabaseHelper.UpdateEntry(currentEntry)
        Else
            success = DatabaseHelper.AddEntry(currentEntry)
        End If
        
        If success Then
            MessageBox.Show("Entry saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub
    
    ''' <summary>
    ''' Cancel button click handler (Procedure)
    ''' </summary>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    
    ''' <summary>
    ''' Form load event handler (Procedure)
    ''' </summary>
    Private Sub EntryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set focus to title field
        txtTitle.Focus()
    End Sub
End Class
