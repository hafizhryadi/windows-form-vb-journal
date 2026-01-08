# Troubleshooting Guide

This guide helps resolve common issues when building and running the Journal Application.

## Table of Contents
1. [Build Errors](#build-errors)
2. [Runtime Errors](#runtime-errors)
3. [Database Issues](#database-issues)
4. [SQLite Installation](#sqlite-installation)
5. [Common Questions](#common-questions)

---

## Build Errors

### Error: "Could not load file or assembly 'System.Data.SQLite'"

**Cause**: System.Data.SQLite package is not installed or not found.

**Solution**:

**Option 1: Install via NuGet Package Manager (Recommended)**
1. Open Visual Studio
2. Right-click on the JournalApp project in Solution Explorer
3. Select "Manage NuGet Packages"
4. Click "Browse" tab
5. Search for "System.Data.SQLite"
6. Click Install on "System.Data.SQLite" (by System.Data.SQLite Team)
7. Accept the license agreement
8. Rebuild the project

**Option 2: Install via Package Manager Console**
```powershell
Install-Package System.Data.SQLite
```

**Option 3: Install via .NET CLI**
```bash
dotnet add package System.Data.SQLite
```

**Option 4: Manual Installation**
1. Download System.Data.SQLite from https://system.data.sqlite.org/downloads.html
2. Choose the appropriate version for .NET Framework 4.7.2
3. Extract the ZIP file
4. Copy the following files to JournalApp folder:
   - System.Data.SQLite.dll
   - SQLite.Interop.dll (x86 and x64 versions)
5. The project file already references System.Data.SQLite.dll

### Error: "Type 'SQLiteConnection' is not defined"

**Cause**: Missing Imports statement at the top of the file.

**Solution**: Ensure this line is at the top of DatabaseHelper.vb:
```vb
Imports System.Data.SQLite
```

### Error: "Namespace or type specified in Imports 'System.Data.SQLite' doesn't contain any public member"

**Cause**: SQLite assembly is not referenced in the project.

**Solution**: Install System.Data.SQLite package (see first solution above)

### Error: "Could not find a part of the path"

**Cause**: The database file path is incorrect or the application doesn't have permission to create files.

**Solution**:
1. Run Visual Studio as Administrator
2. Or modify DatabaseHelper.vb to use full path:
```vb
Private Shared ReadOnly connectionString As String = "Data Source=" & 
    System.IO.Path.Combine(Application.StartupPath, "journal.db") & ";Version=3;"
```

---

## Runtime Errors

### Error: "Unable to load DLL 'SQLite.Interop.dll'"

**Cause**: The native SQLite library is missing or not in the correct location.

**Solution**:

1. Ensure you have the correct architecture (x86/x64) SQLite.Interop.dll
2. Copy SQLite.Interop.dll to the output directory (bin\Debug or bin\Release)
3. Or reinstall System.Data.SQLite via NuGet (which handles this automatically)

### Error: "Database is locked"

**Cause**: Another process or connection is accessing the database.

**Solution**:
1. Close all connections properly (the code uses `Using` statements which should handle this)
2. Close any SQLite browser tools accessing journal.db
3. Restart the application
4. If persistent, delete journal.db and let the app recreate it

### Application doesn't start

**Cause**: Missing .NET Framework or dependencies.

**Solution**:
1. Install .NET Framework 4.7.2 or later from Microsoft
2. Ensure all DLL files are in the output directory
3. Check Windows Event Viewer for detailed error messages

---

## Database Issues

### Database file not created

**Cause**: Application doesn't have write permissions or incorrect path.

**Solution**:
1. Run Visual Studio as Administrator
2. Check the bin\Debug folder - journal.db should be created there
3. Verify DatabaseHelper.InitializeDatabase() is being called in MainForm_Load

### Data not persisting

**Cause**: Database operations are failing silently.

**Solution**:
1. Check for error messages - the code should show MessageBox on errors
2. Verify the database file exists and is not read-only
3. Check file permissions on journal.db
4. Look at the Try-Catch blocks - they should display error messages

### Database schema errors

**Cause**: Table structure is incorrect or database is corrupted.

**Solution**:
1. Delete journal.db file
2. Run the application - it will recreate the database
3. Or manually create the database using database_schema.sql:
```bash
# Using SQLite command line
sqlite3 journal.db < database_schema.sql
```

---

## SQLite Installation

### Installing System.Data.SQLite via NuGet (Detailed Steps)

1. **Open Visual Studio**
2. **Open Solution**: File → Open → Project/Solution → Select JournalApp.sln
3. **Open NuGet Package Manager**: 
   - Right-click on "JournalApp" project in Solution Explorer
   - Select "Manage NuGet Packages..."
4. **Install Package**:
   - Click on "Browse" tab
   - Search for "System.Data.SQLite"
   - Select "System.Data.SQLite" by System.Data.SQLite Team
   - Click "Install" button
   - Review changes and click "OK"
   - Accept license agreement
5. **Verify Installation**:
   - Expand "References" in Solution Explorer
   - You should see "System.Data.SQLite" listed
6. **Build Project**: Build → Build Solution (or press Ctrl+Shift+B)

### Package Manager Console Commands

```powershell
# Install specific version
Install-Package System.Data.SQLite -Version 1.0.118.0

# Update existing package
Update-Package System.Data.SQLite

# Uninstall and reinstall
Uninstall-Package System.Data.SQLite
Install-Package System.Data.SQLite
```

---

## Common Questions

### Q: The application compiles but shows no UI

**A**: Check that:
1. MainForm is set as the startup form in My Project\Application.Designer.vb
2. The Application.myapp file specifies MainForm as the MainForm
3. No exceptions are being thrown in MainForm_Load

### Q: DataGridView shows no columns

**A**: Ensure:
1. AutoGenerateColumns is set to False
2. SetupDataGridView() is being called in MainForm_Load
3. DataPropertyName matches the property names in JournalEntry class

### Q: Dates are displaying incorrectly

**A**: Check:
1. DateTimePicker Format is set to Short
2. Date format in database is "yyyy-MM-dd"
3. DataGridView column format is set: `.DefaultCellStyle = New DataGridViewCellStyle With {.Format = "yyyy-MM-dd"}`

### Q: Search functionality not working

**A**: Verify:
1. Search button is wired to btnSearch_Click event
2. SQL query uses LIKE operator for text search
3. Parameters are properly added to command
4. No SQL syntax errors in SearchEntries function

### Q: Arrays not showing data

**A**: Check:
1. LoadEntriesIntoArrays() is being called after database operations
2. ArrayManager.InitializeArrays() is called in MainForm_Load
3. entryCount is being incremented correctly

### Q: Can I use SQL Server or Access instead of SQLite?

**A**: Yes, but you'll need to:
1. Change the connection string in App.config
2. Modify DatabaseHelper.vb to use appropriate ADO.NET provider
3. Update SQL syntax (SQLite, SQL Server, and Access have slight differences)
4. Reference appropriate DLLs (System.Data.SqlClient for SQL Server, System.Data.OleDb for Access)

Example for SQL Server:
```vb
Imports System.Data.SqlClient

Private Shared ReadOnly connectionString As String = 
    "Server=localhost;Database=JournalDB;Integrated Security=true;"

Using connection As New SqlConnection(connectionString)
    ' Rest of the code is similar
End Using
```

Example for MS Access:
```vb
Imports System.Data.OleDb

Private Shared ReadOnly connectionString As String = 
    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=journal.accdb;"

Using connection As New OleDbConnection(connectionString)
    ' Rest of the code is similar
End Using
```

### Q: How do I deploy this application?

**A**: 
1. Build in Release mode: Build → Configuration Manager → Select "Release"
2. Build the solution
3. Find the executable in bin\Release folder
4. Include these files when distributing:
   - JournalApp.exe
   - JournalApp.exe.config
   - System.Data.SQLite.dll
   - SQLite.Interop.dll (both x86 and x64 folders)
   - Any other referenced DLLs

5. For professional distribution, create an installer:
   - Use Visual Studio Installer Projects extension
   - Or use ClickOnce deployment
   - Or use third-party tools like Inno Setup or WiX

### Q: How do I backup my journal data?

**A**: Simply copy the journal.db file to a safe location. To restore, copy it back to the application directory.

### Q: Can I add more fields to the journal entries?

**A**: Yes:
1. Modify the database schema (add columns to JournalEntries table)
2. Update JournalEntry.vb to include new properties
3. Update EntryForm to add new input fields
4. Update DatabaseHelper CRUD operations to handle new fields
5. Update MainForm DataGridView columns

---

## Getting Help

If you encounter issues not covered here:

1. **Check the code comments** - Most methods have XML documentation
2. **Review PROGRAMMING_CONCEPTS.md** - Explains how each feature works
3. **Check Windows Event Viewer** - For detailed error information
4. **Enable Debug Mode** - Run in Visual Studio with breakpoints
5. **Open an Issue** - Report bugs on the GitHub repository

## System Requirements

- **Operating System**: Windows 7 SP1 or later
- **Framework**: .NET Framework 4.7.2 or later
- **IDE**: Visual Studio 2017 or later (Community, Professional, or Enterprise)
- **Memory**: 2 GB RAM minimum, 4 GB recommended
- **Disk Space**: 500 MB for development, 50 MB for deployment
- **Display**: 1280x720 minimum resolution

## Performance Tips

1. **Database Optimization**:
   - The schema includes indexes on EntryDate and Title
   - Keep database file size reasonable (under 100 MB)
   - Run VACUUM command periodically to optimize database

2. **Array Management**:
   - Arrays are automatically resized in chunks (doubles in size)
   - Clear arrays when not needed to free memory

3. **UI Responsiveness**:
   - Database operations run on the main thread (for simplicity)
   - For large databases, consider async operations
   - Limit DataGridView rows with paging

## Additional Resources

- [System.Data.SQLite Documentation](https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki)
- [Visual Basic .NET Documentation](https://docs.microsoft.com/en-us/dotnet/visual-basic/)
- [Windows Forms Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
- [ADO.NET Documentation](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/)
