# Quick Start Guide

Get the Journal Application up and running in minutes!

## Prerequisites Check

Before you begin, ensure you have:
- ✅ Windows 7 SP1 or later
- ✅ Visual Studio 2017 or later (with VB.NET support)
- ✅ .NET Framework 4.7.2 or later

## Step-by-Step Setup

### 1. Get the Code

```bash
git clone https://github.com/hafizhryadi/windows-form-vb-journal.git
cd windows-form-vb-journal
```

### 2. Open in Visual Studio

1. Double-click `JournalApp.sln` or
2. Open Visual Studio → File → Open → Project/Solution → Select `JournalApp.sln`

### 3. Install SQLite Package

**Easy Method (Recommended):**
1. In Visual Studio, right-click "JournalApp" project in Solution Explorer
2. Select "Manage NuGet Packages..."
3. Click "Browse" tab
4. Search for "System.Data.SQLite"
5. Click "Install"
6. Accept the license

**Alternative - Package Manager Console:**
```powershell
Install-Package System.Data.SQLite
```

### 4. Build and Run

1. Press **F5** or click the **Start** button
2. The application will launch with an empty journal
3. A database file `journal.db` will be created automatically

## First Use

### Add Your First Entry

1. Click **"Add Entry"** button (blue button on bottom left)
2. Fill in the form:
   - **Date**: Select today's date (or any date)
   - **Title**: Enter a title (e.g., "My First Journal Entry")
   - **Description**: Write your thoughts
3. Click **"Save"**
4. Your entry appears in the main list!

### Edit an Entry

1. Click on an entry in the list to select it
2. Click **"Edit Entry"** button (green)
3. Modify the fields
4. Click **"Save"**

### Delete an Entry

1. Select an entry from the list
2. Click **"Delete Entry"** button (red)
3. Confirm the deletion

### Search Entries

**Search by Text:**
1. Enter text in "Search by Title/Text" field
2. Click **"Search"**

**Search by Date:**
1. Check "Search by Date" checkbox
2. Select a date
3. Click **"Search"**

**Combined Search:**
1. Enter text AND select a date
2. Click **"Search"**

**Clear Search:**
- Click **"Clear"** button to show all entries again

### View Array Contents

Click **"View Arrays"** button (purple) to see:
- One-dimensional array (entry titles)
- Multi-dimensional array (complete entry data)
- Current count of entries in arrays

## Project Structure Overview

```
JournalApp.sln                  # Solution file - open this
│
└── JournalApp/                 # Main project folder
    ├── MainForm.vb             # Main window code
    ├── EntryForm.vb            # Add/Edit window code
    ├── DatabaseHelper.vb       # Database operations
    ├── ArrayManager.vb         # Array demonstrations
    ├── JournalEntry.vb         # Data model
    └── journal.db              # SQLite database (created automatically)
```

## Key Features at a Glance

| Feature | How to Access |
|---------|---------------|
| Add Entry | Blue "Add Entry" button |
| Edit Entry | Select entry → Green "Edit Entry" button |
| Delete Entry | Select entry → Red "Delete Entry" button |
| Refresh List | Gray "Refresh" button |
| Search | Use search box and "Search" button |
| View Arrays | Purple "View Arrays" button |

## UI Color Guide

- **Blue** (#3498db) - Add/Create actions
- **Green** (#2ecc71) - Edit/Save actions
- **Red** (#e74c3c) - Delete/Remove actions
- **Gray** (#95a5a6) - Refresh/Secondary actions
- **Purple** (#9b59b6) - Special features (arrays)

## Common Tasks

### Backup Your Data
1. Close the application
2. Copy `JournalApp\bin\Debug\journal.db` to a safe location
3. To restore, copy it back

### Reset Database
1. Close the application
2. Delete `journal.db` from `JournalApp\bin\Debug\`
3. Run the application - it will create a fresh database

### View Data Externally
1. Download [DB Browser for SQLite](https://sqlitebrowser.org/)
2. Open `journal.db` with DB Browser
3. Browse the JournalEntries table

## Next Steps

Now that you have the application running:

1. **Explore the Code**: 
   - Open `MainForm.vb` to see the main logic
   - Check `DatabaseHelper.vb` for database operations
   - Review `ArrayManager.vb` for array demonstrations

2. **Learn the Concepts**:
   - Read `PROGRAMMING_CONCEPTS.md` for detailed explanations
   - See how functions, procedures, and arrays are used

3. **Customize**:
   - Modify colors in the Designer files
   - Add new fields to journal entries
   - Extend functionality

## Need Help?

- 📖 **Detailed Documentation**: See `README.md`
- 🔧 **Having Issues?**: Check `TROUBLESHOOTING.md`
- 💡 **Understanding Code**: Read `PROGRAMMING_CONCEPTS.md`
- 🐛 **Found a Bug?**: Open an issue on GitHub

## Keyboard Shortcuts

- **F5**: Run/Start application (in Visual Studio)
- **Ctrl+Shift+B**: Build solution
- **Enter**: Confirm dialog (in forms)
- **Esc**: Cancel dialog (in forms)

## Tips

1. ✨ **Status Bar**: Bottom right shows total entries and array count
2. 🔍 **Quick Search**: Type and press Enter in search box
3. 📅 **Date Selection**: Click the calendar icon in date pickers
4. 📝 **Multi-line**: Description field auto-scrolls for long text
5. 🔄 **Auto-refresh**: List updates automatically after add/edit/delete

## What You're Learning

This project demonstrates:
- ✅ Functions and Procedures
- ✅ One-dimensional Arrays
- ✅ Multi-dimensional Arrays
- ✅ Dynamic Array Resizing
- ✅ For/While/ForEach Loops
- ✅ Select Case Statements
- ✅ SQLite Database Operations
- ✅ Windows Forms UI Design
- ✅ Event-Driven Programming

## Success Indicators

You'll know everything is working when:
- ✅ Application opens without errors
- ✅ You can add a new entry
- ✅ Entry appears in the main list
- ✅ You can search and find entries
- ✅ "View Arrays" shows your entries
- ✅ Status bar shows correct count

## Quick Reference Card

```
┌─────────────────────────────────────────────┐
│     JOURNAL APPLICATION - QUICK REF         │
├─────────────────────────────────────────────┤
│ ADD ENTRY      │ Blue button / Add Entry    │
│ EDIT ENTRY     │ Select + Green button      │
│ DELETE ENTRY   │ Select + Red button        │
│ SEARCH TEXT    │ Type + Search button       │
│ SEARCH DATE    │ Check box + Select date    │
│ CLEAR SEARCH   │ Clear button               │
│ VIEW ARRAYS    │ Purple button              │
│ REFRESH        │ Gray Refresh button        │
├─────────────────────────────────────────────┤
│ DATABASE FILE  │ bin\Debug\journal.db       │
│ BACKUP         │ Copy journal.db file       │
│ RESET          │ Delete journal.db file     │
└─────────────────────────────────────────────┘
```

## Congratulations! 🎉

You're now ready to use and explore the Journal Application. Start by adding your first entry and exploring the features!

Happy journaling! ✍️
