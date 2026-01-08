# Journal Application - Windows Forms VB.NET

A comprehensive Windows Forms application built with Visual Basic .NET for managing personal journal entries. This project demonstrates key programming concepts including functions, procedures, arrays, loops, database connectivity, and modern UI design.

## Features

### 1. **Record Journal Entries**
- Add new journal entries with date, title, and description
- Display all entries in a DataGridView with sortable columns
- Data persists in SQLite database for long-term storage
- In-memory array management for fast access

### 2. **Edit and Delete Entries**
- Edit existing journal entries with pre-filled data
- Delete entries with confirmation dialog
- Real-time updates reflected in the UI

### 3. **Search Functionality**
- Search entries by title or description text
- Filter entries by specific date
- Combine text and date searches for precise results
- Uses loops and conditional logic for efficient searching

### 4. **Array Demonstrations**
- **One-dimensional arrays**: Store entry titles in memory
- **Multi-dimensional arrays**: Store complete entry data (date, title, description)
- **Dynamic arrays**: Automatically resize as entries are added
- View array contents with the "View Arrays" button

### 5. **Database Backend**
- SQLite database for persistent storage
- Automatic database initialization on first run
- CRUD operations (Create, Read, Update, Delete)
- Connection string management through App.config

### 6. **Modern UI Design**
- Clean, professional interface with color-coded buttons
- Responsive layout that adjusts to window size
- Status bar showing entry counts
- User-friendly forms with validation

## Programming Concepts Demonstrated

### Functions
- `GetAllEntries()` - Returns list of journal entries from database
- `SearchEntries()` - Returns filtered entries based on criteria
- `AddEntry()` - Returns Boolean indicating success/failure
- `ValidateInput()` - Returns Boolean after validating form data
- `GetAllTitles()` - Returns one-dimensional array of titles

### Procedures (Subroutines)
- `InitializeDatabase()` - Sets up database and tables
- `LoadEntries()` - Loads entries into DataGridView
- `HandleMenuAction()` - Processes menu selections
- `AddToArrays()` - Adds entries to in-memory arrays
- `ClearArrays()` - Clears all array data

### Arrays
- **One-dimensional array**: `titleArray()` stores entry titles
- **Multi-dimensional array**: `entryDataArray(,)` stores complete entry data
- **Dynamic resizing**: Arrays automatically grow as needed
- **Array manipulation**: Add, remove, search operations with loops

### Loops
- `For...Next` loops for iterating through arrays
- `While` loops for reading database records
- `For Each` loops for processing collections
- Loop-based search algorithms

### Select Case
- Menu navigation with `HandleMenuAction()` procedure
- Demonstrates multiple action handling:
  - Add, Edit, Delete operations
  - Search and refresh functionality
  - Array viewing

### Database Connectivity
- SQLite database with ADO.NET
- Parameterized queries to prevent SQL injection
- Transaction management
- Connection string configuration

## Project Structure

```
JournalApp/
├── JournalApp.sln              # Visual Studio solution file
└── JournalApp/
    ├── JournalApp.vbproj       # Project file
    ├── App.config              # Configuration (connection strings)
    ├── MainForm.vb             # Main application form
    ├── MainForm.Designer.vb    # Main form designer code
    ├── MainForm.resx           # Main form resources
    ├── EntryForm.vb            # Add/Edit entry form
    ├── EntryForm.Designer.vb   # Entry form designer code
    ├── EntryForm.resx          # Entry form resources
    ├── JournalEntry.vb         # Entry data model
    ├── DatabaseHelper.vb       # Database operations
    ├── ArrayManager.vb         # Array management class
    └── My Project/             # Project properties and settings
        ├── Application.Designer.vb
        ├── Application.myapp
        ├── AssemblyInfo.vb
        ├── Resources.Designer.vb
        ├── Resources.resx
        ├── Settings.Designer.vb
        └── Settings.settings
```

## Database Schema

### JournalEntries Table
```sql
CREATE TABLE JournalEntries (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EntryDate TEXT NOT NULL,
    Title TEXT NOT NULL,
    Description TEXT
);
```

## Setup Instructions

### Prerequisites
- Visual Studio 2017 or later (with VB.NET support)
- .NET Framework 4.7.2 or later
- System.Data.SQLite package

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/hafizhryadi/windows-form-vb-journal.git
   cd windows-form-vb-journal
   ```

2. **Install SQLite Package**
   
   You have two options:

   **Option A: Using NuGet Package Manager (Recommended)**
   - Open Visual Studio
   - Open the solution file `JournalApp.sln`
   - Right-click on the project in Solution Explorer
   - Select "Manage NuGet Packages"
   - Search for "System.Data.SQLite"
   - Install the package

   **Option B: Manual Installation**
   - Download System.Data.SQLite from https://system.data.sqlite.org/
   - Copy `System.Data.SQLite.dll` to the JournalApp project folder
   - The project is already configured to reference it

3. **Build the project**
   - Open `JournalApp.sln` in Visual Studio
   - Press `Ctrl+Shift+B` or select Build → Build Solution
   - The database will be automatically created on first run

4. **Run the application**
   - Press `F5` or click the Start button
   - The main form will appear with an empty journal

## Usage Guide

### Adding a New Entry
1. Click the **"Add Entry"** button
2. Select the date using the date picker
3. Enter a title (required, max 200 characters)
4. Enter a description (optional)
5. Click **"Save"** to add the entry

### Editing an Entry
1. Select an entry from the list
2. Click the **"Edit Entry"** button
3. Modify the fields as needed
4. Click **"Save"** to update

### Deleting an Entry
1. Select an entry from the list
2. Click the **"Delete Entry"** button
3. Confirm the deletion

### Searching Entries
1. Enter search text in the "Search by Title/Text" field, OR
2. Check "Search by Date" and select a date, OR
3. Use both criteria together
4. Click **"Search"** to filter results
5. Click **"Clear"** to show all entries again

### Viewing Arrays
- Click the **"View Arrays"** button to see:
  - One-dimensional array (entry titles)
  - Multi-dimensional array (complete entry data)
  - Current array count

## Key Classes and Methods

### DatabaseHelper
- `InitializeDatabase()` - Creates database and tables
- `AddEntry(entry)` - Adds new entry to database
- `UpdateEntry(entry)` - Updates existing entry
- `DeleteEntry(id)` - Deletes entry by ID
- `GetAllEntries()` - Returns all entries
- `SearchEntries(text, date)` - Searches with filters

### ArrayManager
- `InitializeArrays(capacity)` - Sets up arrays
- `AddToArrays(date, title, desc)` - Adds to arrays
- `GetAllTitles()` - Returns title array
- `GetEntryDataByIndex(index)` - Gets multi-dimensional data
- `SearchTitles(text)` - Searches title array
- `RemoveFromArrays(index)` - Removes entry from arrays

### MainForm
- `HandleMenuAction(action)` - Select Case for menu navigation
- `LoadEntries()` - Loads entries from database
- `PerformSearch()` - Searches with loops
- `ShowArrayContents()` - Displays array data

### EntryForm
- `ValidateInput()` - Validates form fields
- `LoadEntryData()` - Loads entry for editing
- Event handlers for Save and Cancel

## Technical Highlights

### Array Operations
The application demonstrates three types of arrays:
- **Static sizing**: Initial array allocation
- **Dynamic resizing**: `ReDim Preserve` for one-dimensional arrays
- **Manual copying**: Required for multi-dimensional array resizing

### Database Operations
- Uses ADO.NET with SQLite provider
- Parameterized queries for security
- Proper connection disposal with `Using` statements
- Error handling with Try-Catch blocks

### UI Design
- Flat design with modern color scheme
- Responsive layout with anchoring
- Custom colors for different button types:
  - Blue (#3498db) - Primary actions
  - Green (#2ecc71) - Success/Save
  - Red (#e74c3c) - Delete/Danger
  - Gray (#95a5a6) - Secondary actions
  - Purple (#9b59b6) - Special features

## Future Enhancements

Potential improvements for the application:
- Export entries to PDF or text file
- Import entries from external files
- Categories/tags for entries
- Rich text formatting for descriptions
- Image attachments
- Cloud synchronization
- Search result highlighting
- Entry statistics and analytics

## License

This project is created for educational purposes as part of a programming course demonstrating Visual Basic .NET concepts.

## Author

Hafizh Ryadi

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.
