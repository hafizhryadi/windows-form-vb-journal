Imports System.Data.SQLite
Imports System.IO

''' <summary>
''' Database helper class for managing SQLite database operations
''' Demonstrates functions, procedures, and database connectivity
''' </summary>
Public Class DatabaseHelper
    Private Shared ReadOnly connectionString As String = "Data Source=journal.db;Version=3;"
    
    ''' <summary>
    ''' Initializes the database and creates tables if they don't exist (Procedure)
    ''' </summary>
    Public Shared Sub InitializeDatabase()
        Try
            ' Create database file if it doesn't exist
            If Not File.Exists("journal.db") Then
                SQLiteConnection.CreateFile("journal.db")
            End If
            
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
        Catch ex As Exception
            MessageBox.Show("Error initializing database: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    
    ''' <summary>
    ''' Adds a new journal entry to the database (Function returning Boolean)
    ''' </summary>
    Public Shared Function AddEntry(entry As JournalEntry) As Boolean
        Try
            Using connection As New SQLiteConnection(connectionString)
                connection.Open()
                
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
            MessageBox.Show("Error adding entry: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Updates an existing journal entry (Function returning Boolean)
    ''' </summary>
    Public Shared Function UpdateEntry(entry As JournalEntry) As Boolean
        Try
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
        Catch ex As Exception
            MessageBox.Show("Error updating entry: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Deletes a journal entry by ID (Function returning Boolean)
    ''' </summary>
    Public Shared Function DeleteEntry(id As Integer) As Boolean
        Try
            Using connection As New SQLiteConnection(connectionString)
                connection.Open()
                
                Dim query As String = "DELETE FROM JournalEntries WHERE Id = @Id"
                
                Using command As New SQLiteCommand(query, connection)
                    command.Parameters.AddWithValue("@Id", id)
                    command.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("Error deleting entry: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Gets all journal entries from database (Function returning List)
    ''' Demonstrates loops for data retrieval
    ''' </summary>
    Public Shared Function GetAllEntries() As List(Of JournalEntry)
        Dim entries As New List(Of JournalEntry)
        
        Try
            Using connection As New SQLiteConnection(connectionString)
                connection.Open()
                
                Dim query As String = "SELECT Id, EntryDate, Title, Description FROM JournalEntries ORDER BY EntryDate DESC"
                
                Using command As New SQLiteCommand(query, connection)
                    Using reader As SQLiteDataReader = command.ExecuteReader()
                        ' Loop through results
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
        Catch ex As Exception
            MessageBox.Show("Error retrieving entries: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        
        Return entries
    End Function
    
    ''' <summary>
    ''' Searches journal entries by title or date (Function with loop and conditional logic)
    ''' </summary>
    Public Shared Function SearchEntries(searchText As String, searchDate As Date?) As List(Of JournalEntry)
        Dim entries As New List(Of JournalEntry)
        
        Try
            Using connection As New SQLiteConnection(connectionString)
                connection.Open()
                
                Dim query As String = "SELECT Id, EntryDate, Title, Description FROM JournalEntries WHERE 1=1"
                
                ' Build dynamic query based on search criteria
                If Not String.IsNullOrEmpty(searchText) Then
                    query &= " AND (Title LIKE @SearchText OR Description LIKE @SearchText)"
                End If
                
                If searchDate.HasValue Then
                    query &= " AND EntryDate = @SearchDate"
                End If
                
                query &= " ORDER BY EntryDate DESC"
                
                Using command As New SQLiteCommand(query, connection)
                    If Not String.IsNullOrEmpty(searchText) Then
                        command.Parameters.AddWithValue("@SearchText", "%" & searchText & "%")
                    End If
                    
                    If searchDate.HasValue Then
                        command.Parameters.AddWithValue("@SearchDate", searchDate.Value.ToString("yyyy-MM-dd"))
                    End If
                    
                    Using reader As SQLiteDataReader = command.ExecuteReader()
                        ' Loop through search results
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
        Catch ex As Exception
            MessageBox.Show("Error searching entries: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        
        Return entries
    End Function
    
    ''' <summary>
    ''' Gets entry count from database (Function returning Integer)
    ''' </summary>
    Public Shared Function GetEntryCount() As Integer
        Try
            Using connection As New SQLiteConnection(connectionString)
                connection.Open()
                
                Dim query As String = "SELECT COUNT(*) FROM JournalEntries"
                
                Using command As New SQLiteCommand(query, connection)
                    Return Convert.ToInt32(command.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error getting entry count: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        End Try
    End Function
End Class
