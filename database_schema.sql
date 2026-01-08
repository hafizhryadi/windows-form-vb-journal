-- Journal Application Database Schema
-- SQLite Database for storing journal entries
-- Created: 2026-01-08

-- Drop table if exists (for clean reinstall)
DROP TABLE IF EXISTS JournalEntries;

-- Create JournalEntries table
CREATE TABLE IF NOT EXISTS JournalEntries (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EntryDate TEXT NOT NULL,
    Title TEXT NOT NULL,
    Description TEXT
);

-- Create index on EntryDate for faster searching
CREATE INDEX IF NOT EXISTS idx_entry_date ON JournalEntries(EntryDate);

-- Create index on Title for faster text searches
CREATE INDEX IF NOT EXISTS idx_title ON JournalEntries(Title);

-- Sample data for testing (optional)
-- Uncomment the following lines to add sample entries

/*
INSERT INTO JournalEntries (EntryDate, Title, Description) VALUES 
    ('2026-01-01', 'New Year Resolution', 'Started learning VB.NET and Windows Forms development. Excited to build my first journal application!'),
    ('2026-01-05', 'Project Progress', 'Completed the database schema and helper classes. Arrays are working well for in-memory management.'),
    ('2026-01-08', 'First Entry', 'Successfully created the journal application with full CRUD operations. The UI looks great!');
*/

-- Verify table structure
-- SELECT sql FROM sqlite_master WHERE type='table' AND name='JournalEntries';

-- Notes:
-- 1. EntryDate is stored as TEXT in ISO format (YYYY-MM-DD) for compatibility
-- 2. Id is auto-incremented integer primary key
-- 3. Title is required (NOT NULL)
-- 4. Description is optional
-- 5. Indexes improve search performance for date and title searches
