# SongLibrary

WinForms application that manages a local song library stored in a SQLite database.
Datagridview works well with datatables using the binding source
Has column sorting enabled by default
Used Column button for edit/delete since it reduces uneccessary buttons on the form elsewhere
Used search box filter method that allow for filtering through all columns 
Included Date Range filtering
Included Unit tests for building filters and testing inserting song records

## Summary
- Framework: .NET 8 (net8.0-windows)  
- UI: Windows Forms  
- Data: SQLite database (`Data Sources\SongLibrary.db`)  
- Project layout: `SongLibrary` (app) and `SongLibraryTest` (unit tests)

## Requirements
- Windows (WinForms)
- .NET 8 SDK
- Visual Studio 2022/2023 or later (with Windows desktop workload) for IDE experience

## Quick start

## End User
- Download and Unzip the latest release here https://github.com/txk6632/SongLibrary/releases
- Double Click SongLibrary application to run
- Unknown Publisher Warning - Click Run Anyway

## Developers

### Clone
git clone https://github.com/txk6632/SongLibrary
cd SongLibrary

### Fix Security Settings on MainForm.resx
-  SongLibrary/MainForm/Right Click MainForm.resx -> General Tab -> Check "Unblock" at the bottom next to Security
  
### Open in Visual Studio
- Open the solution in Visual Studio.
- Build configuration: `Release` or `Debug`.

## Run from Visual Studio
- Open the solution, set `SongLibrary` as startup project and press F5 or use __Debug > Start Debugging__.


CLI:
- Framework-dependent publish:
