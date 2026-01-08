# Programming Concepts Reference Guide

This document provides a detailed reference of all programming concepts demonstrated in the Journal Application.

## Table of Contents
1. [Functions](#functions)
2. [Procedures (Subroutines)](#procedures)
3. [One-Dimensional Arrays](#one-dimensional-arrays)
4. [Multi-Dimensional Arrays](#multi-dimensional-arrays)
5. [Dynamic Arrays](#dynamic-arrays)
6. [Loops](#loops)
7. [Select Case](#select-case)
8. [Database Connectivity](#database-connectivity)

---

## Functions

Functions are code blocks that return a value. In VB.NET, they are declared with the `Function` keyword.

### Examples from DatabaseHelper.vb:

```vb
' Function returning Boolean
Public Shared Function AddEntry(entry As JournalEntry) As Boolean
    ' ... code ...
    Return True
End Function

' Function returning List(Of JournalEntry)
Public Shared Function GetAllEntries() As List(Of JournalEntry)
    Dim entries As New List(Of JournalEntry)
    ' ... code ...
    Return entries
End Function

' Function returning Integer
Public Shared Function GetEntryCount() As Integer
    ' ... code ...
    Return Convert.ToInt32(command.ExecuteScalar())
End Function
```

### Key Characteristics:
- Must return a value using `Return` statement
- Return type specified after `As` keyword
- Can accept parameters
- Used when you need to get a result back

---

## Procedures

Procedures (Subroutines) perform actions but don't return values. Declared with `Sub` keyword.

### Examples from DatabaseHelper.vb:

```vb
' Procedure with no return value
Public Shared Sub InitializeDatabase()
    ' Creates database and tables
    ' No return value needed
End Sub
```

### Examples from MainForm.vb:

```vb
' Procedure that loads entries
Private Sub LoadEntries()
    currentEntries = DatabaseHelper.GetAllEntries()
    dgvEntries.DataSource = Nothing
    dgvEntries.DataSource = currentEntries
    UpdateStatusLabel()
End Sub

' Procedure that handles button clicks
Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
    HandleMenuAction("Add")
End Sub
```

### Key Characteristics:
- No return value
- Used for actions and side effects
- Can modify class properties or parameters
- Event handlers are procedures

---

## One-Dimensional Arrays

Arrays that store elements in a single row (like a list).

### Examples from ArrayManager.vb:

```vb
' Declaration
Private Shared titleArray() As String

' Initialization with size
ReDim titleArray(9)  ' Creates array with 10 elements (0-9)

' Adding elements
titleArray(0) = "First Title"
titleArray(1) = "Second Title"

' Accessing elements in a loop
For i As Integer = 0 To titleArray.Length - 1
    Console.WriteLine(titleArray(i))
Next

' Returning the array
Public Shared Function GetAllTitles() As String()
    Dim result(entryCount - 1) As String
    For i As Integer = 0 To entryCount - 1
        result(i) = titleArray(i)
    Next
    Return result
End Function
```

### Use Cases in Application:
- Storing journal entry titles
- Quick title lookups
- Simple list operations

---

## Multi-Dimensional Arrays

Arrays that store elements in rows and columns (like a table).

### Examples from ArrayManager.vb:

```vb
' Declaration (2D array: rows x columns)
Private Shared entryDataArray(,) As String

' Initialization: 10 rows x 3 columns
ReDim entryDataArray(9, 2)

' Adding data
entryDataArray(0, 0) = "2026-01-08"  ' Date
entryDataArray(0, 1) = "My Title"     ' Title
entryDataArray(0, 2) = "Description"  ' Description

' Accessing with nested loops
For i As Integer = 0 To rows - 1
    For j As Integer = 0 To columns - 1
        Console.WriteLine(entryDataArray(i, j))
    Next
Next

' Getting a specific row
Public Shared Function GetEntryDataByIndex(index As Integer) As String()
    Dim data(2) As String
    data(0) = entryDataArray(index, 0)  ' Date
    data(1) = entryDataArray(index, 1)  ' Title
    data(2) = entryDataArray(index, 2)  ' Description
    Return data
End Function
```

### Use Cases in Application:
- Storing complete entry data (date, title, description)
- Table-like data representation
- Temporary data storage before database commit

---

## Dynamic Arrays

Arrays that can change size at runtime.

### Examples from ArrayManager.vb:

```vb
' Dynamic resizing of one-dimensional array
Public Shared Sub AddToArrays(entryDate As Date, title As String, description As String)
    ' Check if array needs resizing
    If titleArray Is Nothing OrElse entryCount >= titleArray.Length Then
        Dim newSize As Integer = If(titleArray Is Nothing, 10, titleArray.Length * 2)
        
        ' ReDim Preserve keeps existing data
        ReDim Preserve titleArray(newSize - 1)
    End If
    
    ' Add new element
    titleArray(entryCount) = title
    entryCount += 1
End Sub

' Manual resizing for multi-dimensional array
Dim newEntryDataArray(newSize - 1, 2) As String
If entryDataArray IsNot Nothing Then
    ' Must copy manually for multi-dimensional arrays
    For i As Integer = 0 To entryCount - 1
        For j As Integer = 0 To 2
            newEntryDataArray(i, j) = entryDataArray(i, j)
        Next
    Next
End If
entryDataArray = newEntryDataArray
```

### Key Concepts:
- **ReDim**: Redimensions an array
- **ReDim Preserve**: Keeps existing data while resizing (1D only)
- **Manual copying**: Required for multi-dimensional arrays
- **Growth strategy**: Often double the size for efficiency

---

## Loops

Loops allow code to execute repeatedly.

### For...Next Loop

```vb
' Standard For loop
For i As Integer = 0 To 9
    Console.WriteLine(titleArray(i))
Next

' Loop with Step
For i As Integer = 0 To 10 Step 2
    Console.WriteLine(i)  ' Prints 0, 2, 4, 6, 8, 10
Next

' Nested loops for 2D array
For i As Integer = 0 To entryCount - 1
    For j As Integer = 0 To 2
        entryDataArray(i, j) = newEntryDataArray(i, j)
    Next
Next
```

### While Loop (from DatabaseHelper.vb)

```vb
' Database reader loop
Using reader As SQLiteDataReader = command.ExecuteReader()
    While reader.Read()
        Dim entry As New JournalEntry With {
            .Id = Convert.ToInt32(reader("Id")),
            .EntryDate = Date.Parse(reader("EntryDate").ToString()),
            .Title = reader("Title").ToString(),
            .Description = reader("Description").ToString()
        }
        entries.Add(entry)
    End While
End Using
```

### For Each Loop

```vb
' Iterate through collection
For Each entry In entries
    AddToArrays(entry.EntryDate, entry.Title, entry.Description)
Next
```

### Use Cases in Application:
- Iterating through arrays
- Reading database results
- Processing collections
- Search operations

---

## Select Case

Select Case provides multi-way branching based on a value.

### Example from MainForm.vb:

```vb
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
            ' ... deletion code ...
            
        Case "Refresh"
            ' Refresh the list
            LoadEntries()
            
        Case "Search"
            ' Search entries
            PerformSearch()
            
        Case "ClearSearch"
            ' Clear search filters
            txtSearch.Text = ""
            LoadEntries()
            
        Case "ViewArrays"
            ' View array contents
            ShowArrayContents()
            
        Case Else
            ' Default case
            MessageBox.Show("Unknown action.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Select
End Sub
```

### Benefits:
- More readable than multiple If-ElseIf statements
- Better performance for many conditions
- Clear case-by-case logic
- Easy to add new cases

### Alternative (If-ElseIf):

```vb
' Less readable version
If action = "Add" Then
    ' Add code
ElseIf action = "Edit" Then
    ' Edit code
ElseIf action = "Delete" Then
    ' Delete code
' ... many more ElseIf statements ...
Else
    ' Default code
End If
```

---

## Database Connectivity

Demonstrates SQLite database operations using ADO.NET.

### Connection String (from App.config):

```xml
<connectionStrings>
    <add name="JournalDB" 
         connectionString="Data Source=journal.db;Version=3;" 
         providerName="System.Data.SQLite" />
</connectionStrings>
```

### Database Operations:

#### 1. Initialize Database

```vb
Public Shared Sub InitializeDatabase()
    ' Create database file
    If Not File.Exists("journal.db") Then
        SQLiteConnection.CreateFile("journal.db")
    End If
    
    ' Create tables
    Using connection As New SQLiteConnection(connectionString)
        connection.Open()
        
        Dim createTableQuery As String = "
            CREATE TABLE IF NOT EXISTS JournalEntries (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                EntryDate TEXT NOT NULL,
                Title TEXT NOT NULL,
                Description TEXT
            );"
        
        Using command As New SQLiteCommand(createTableQuery, connection)
            command.ExecuteNonQuery()
        End Using
    End Using
End Sub
```

#### 2. Insert Data (Create)

```vb
Public Shared Function AddEntry(entry As JournalEntry) As Boolean
    Try
        Using connection As New SQLiteConnection(connectionString)
            connection.Open()
            
            ' Parameterized query prevents SQL injection
            Dim query As String = "INSERT INTO JournalEntries (EntryDate, Title, Description) VALUES (@Date, @Title, @Description)"
            
            Using command As New SQLiteCommand(query, connection)
                command.Parameters.AddWithValue("@Date", entry.EntryDate.ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@Title", entry.Title)
                command.Parameters.AddWithValue("@Description", entry.Description)
                
                command.ExecuteNonQuery()
            End Using
        End Using
        Return True
    Catch ex As Exception
        MessageBox.Show("Error: " & ex.Message)
        Return False
    End Try
End Function
```

#### 3. Read Data (Select)

```vb
Public Shared Function GetAllEntries() As List(Of JournalEntry)
    Dim entries As New List(Of JournalEntry)
    
    Using connection As New SQLiteConnection(connectionString)
        connection.Open()
        
        Dim query As String = "SELECT Id, EntryDate, Title, Description FROM JournalEntries ORDER BY EntryDate DESC"
        
        Using command As New SQLiteCommand(query, connection)
            Using reader As SQLiteDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim entry As New JournalEntry With {
                        .Id = Convert.ToInt32(reader("Id")),
                        .EntryDate = Date.Parse(reader("EntryDate").ToString()),
                        .Title = reader("Title").ToString(),
                        .Description = reader("Description").ToString()
                    }
                    entries.Add(entry)
                End While
            End Using
        End Using
    End Using
    
    Return entries
End Function
```

#### 4. Update Data

```vb
Public Shared Function UpdateEntry(entry As JournalEntry) As Boolean
    Using connection As New SQLiteConnection(connectionString)
        connection.Open()
        
        Dim query As String = "UPDATE JournalEntries SET EntryDate = @Date, Title = @Title, Description = @Description WHERE Id = @Id"
        
        Using command As New SQLiteCommand(query, connection)
            command.Parameters.AddWithValue("@Id", entry.Id)
            command.Parameters.AddWithValue("@Date", entry.EntryDate.ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@Title", entry.Title)
            command.Parameters.AddWithValue("@Description", entry.Description)
            
            command.ExecuteNonQuery()
        End Using
    End Using
    Return True
End Function
```

#### 5. Delete Data

```vb
Public Shared Function DeleteEntry(id As Integer) As Boolean
    Using connection As New SQLiteConnection(connectionString)
        connection.Open()
        
        Dim query As String = "DELETE FROM JournalEntries WHERE Id = @Id"
        
        Using command As New SQLiteCommand(query, connection)
            command.Parameters.AddWithValue("@Id", id)
            command.ExecuteNonQuery()
        End Using
    End Using
    Return True
End Function
```

### Best Practices Demonstrated:

1. **Using Statement**: Automatically disposes connections
2. **Parameterized Queries**: Prevents SQL injection
3. **Try-Catch**: Proper error handling
4. **Connection Management**: Open/close connections properly
5. **Transactions**: For multiple operations (can be added)

---

## Summary

This journal application demonstrates all the required programming concepts:

- ✅ **Functions**: Return values (GetAllEntries, ValidateInput, etc.)
- ✅ **Procedures**: Perform actions (LoadEntries, InitializeDatabase, etc.)
- ✅ **One-Dimensional Arrays**: Store titles (titleArray)
- ✅ **Multi-Dimensional Arrays**: Store complete data (entryDataArray)
- ✅ **Dynamic Arrays**: Resize as needed (ReDim Preserve)
- ✅ **Loops**: For, While, For Each (array iteration, database reading)
- ✅ **Select Case**: Menu navigation (HandleMenuAction)
- ✅ **Database Connectivity**: SQLite with CRUD operations

Each concept is used in practical, real-world scenarios within a functional application.
