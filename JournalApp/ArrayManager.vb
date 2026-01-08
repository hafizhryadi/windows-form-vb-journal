''' <summary>
''' Array Manager class demonstrating one-dimensional, multi-dimensional, and dynamic arrays
''' Implements array manipulation for in-memory journal entry management
''' </summary>
Public Class ArrayManager
    ' One-dimensional array for storing entry titles
    Private Shared titleArray() As String
    
    ' Multi-dimensional array for storing entry data (Date, Title, Description)
    Private Shared entryDataArray(,) As String
    
    ' Dynamic count of entries in arrays
    Private Shared entryCount As Integer = 0
    
    ''' <summary>
    ''' Initializes arrays with specified capacity (Procedure)
    ''' </summary>
    Public Shared Sub InitializeArrays(capacity As Integer)
        ReDim titleArray(capacity - 1)
        ReDim entryDataArray(capacity - 1, 2) ' 3 columns: Date, Title, Description
        entryCount = 0
    End Sub
    
    ''' <summary>
    ''' Adds an entry to the arrays (Procedure)
    ''' Demonstrates dynamic array resizing
    ''' </summary>
    Public Shared Sub AddToArrays(entryDate As Date, title As String, description As String)
        ' Check if arrays need to be resized (dynamic arrays)
        If titleArray Is Nothing OrElse entryCount >= titleArray.Length Then
            Dim newSize As Integer = If(titleArray Is Nothing, 10, titleArray.Length * 2)
            ReDim Preserve titleArray(newSize - 1)
            
            ' For multi-dimensional arrays, we need to copy manually
            Dim newEntryDataArray(newSize - 1, 2) As String
            If entryDataArray IsNot Nothing Then
                For i As Integer = 0 To entryCount - 1
                    For j As Integer = 0 To 2
                        newEntryDataArray(i, j) = entryDataArray(i, j)
                    Next
                Next
            End If
            entryDataArray = newEntryDataArray
        End If
        
        ' Add entry to arrays
        titleArray(entryCount) = title
        entryDataArray(entryCount, 0) = entryDate.ToString("yyyy-MM-dd")
        entryDataArray(entryCount, 1) = title
        entryDataArray(entryCount, 2) = description
        
        entryCount += 1
    End Sub
    
    ''' <summary>
    ''' Loads entries from database into arrays (Procedure using loops)
    ''' </summary>
    Public Shared Sub LoadEntriesIntoArrays()
        Dim entries As List(Of JournalEntry) = DatabaseHelper.GetAllEntries()
        
        InitializeArrays(Math.Max(10, entries.Count))
        
        ' Loop through entries and add to arrays
        For Each entry In entries
            AddToArrays(entry.EntryDate, entry.Title, entry.Description)
        Next
    End Sub
    
    ''' <summary>
    ''' Gets all titles from the one-dimensional array (Function)
    ''' Demonstrates loop usage
    ''' </summary>
    Public Shared Function GetAllTitles() As String()
        If titleArray Is Nothing OrElse entryCount = 0 Then
            Return New String() {}
        End If
        
        Dim result(entryCount - 1) As String
        
        ' Loop to copy titles
        For i As Integer = 0 To entryCount - 1
            result(i) = titleArray(i)
        Next
        
        Return result
    End Function
    
    ''' <summary>
    ''' Gets entry data by index from multi-dimensional array (Function)
    ''' </summary>
    Public Shared Function GetEntryDataByIndex(index As Integer) As String()
        If entryDataArray Is Nothing OrElse index < 0 OrElse index >= entryCount Then
            Return Nothing
        End If
        
        Dim data(2) As String
        data(0) = entryDataArray(index, 0) ' Date
        data(1) = entryDataArray(index, 1) ' Title
        data(2) = entryDataArray(index, 2) ' Description
        
        Return data
    End Function
    
    ''' <summary>
    ''' Searches titles using one-dimensional array and loops (Function)
    ''' </summary>
    Public Shared Function SearchTitles(searchText As String) As List(Of String)
        Dim results As New List(Of String)
        
        If titleArray Is Nothing OrElse String.IsNullOrEmpty(searchText) Then
            Return results
        End If
        
        ' Loop through titles to find matches
        For i As Integer = 0 To entryCount - 1
            If titleArray(i) IsNot Nothing AndAlso titleArray(i).IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 Then
                results.Add(titleArray(i))
            End If
        Next
        
        Return results
    End Function
    
    ''' <summary>
    ''' Gets the count of entries in arrays (Function)
    ''' </summary>
    Public Shared Function GetCount() As Integer
        Return entryCount
    End Function
    
    ''' <summary>
    ''' Clears all arrays (Procedure)
    ''' </summary>
    Public Shared Sub ClearArrays()
        titleArray = Nothing
        entryDataArray = Nothing
        entryCount = 0
    End Sub
    
    ''' <summary>
    ''' Removes an entry from arrays by index (Procedure)
    ''' Demonstrates array manipulation with loops
    ''' </summary>
    Public Shared Sub RemoveFromArrays(index As Integer)
        If index < 0 OrElse index >= entryCount Then
            Return
        End If
        
        ' Shift elements down
        For i As Integer = index To entryCount - 2
            titleArray(i) = titleArray(i + 1)
            For j As Integer = 0 To 2
                entryDataArray(i, j) = entryDataArray(i + 1, j)
            Next
        Next
        
        ' Clear the last element
        titleArray(entryCount - 1) = Nothing
        For j As Integer = 0 To 2
            entryDataArray(entryCount - 1, j) = Nothing
        Next
        
        entryCount -= 1
    End Sub
    
    ''' <summary>
    ''' Gets all entry data as formatted strings (Function with loops)
    ''' </summary>
    Public Shared Function GetAllEntryDataFormatted() As List(Of String)
        Dim results As New List(Of String)
        
        If entryDataArray Is Nothing OrElse entryCount = 0 Then
            Return results
        End If
        
        ' Loop through multi-dimensional array
        For i As Integer = 0 To entryCount - 1
            Dim formatted As String = $"Date: {entryDataArray(i, 0)}, Title: {entryDataArray(i, 1)}, Description: {entryDataArray(i, 2)}"
            results.Add(formatted)
        Next
        
        Return results
    End Function
End Class
