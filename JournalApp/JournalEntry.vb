''' <summary>
''' Represents a single journal entry
''' </summary>
Public Class JournalEntry
    Public Property Id As Integer
    Public Property EntryDate As Date
    Public Property Title As String
    Public Property Description As String
    
    Public Sub New()
        Me.EntryDate = Date.Today
        Me.Title = ""
        Me.Description = ""
    End Sub
    
    Public Sub New(id As Integer, entryDate As Date, title As String, description As String)
        Me.Id = id
        Me.EntryDate = entryDate
        Me.Title = title
        Me.Description = description
    End Sub
    
    Public Overrides Function ToString() As String
        Return $"{EntryDate.ToShortDateString()} - {Title}"
    End Function
End Class
