# Run the application
## Prerequisites
- Installed .NET 7 SDK
- Installed dotnet-ef tool. Could be installed by command `dotnet tool install --global dotnet-ef`
## Launching the app
- Execute the `.\launch.ps1` script to compose the Web API and Postgres, and apply database migrations.
- Open http://localhost:5935/swagger/index.html in browser

# To Improve
- Use Full Text Index for JournalEntry Text column
- Generate sequential EventId using database Sequence
- Do not load Text column when loading Pageable JournalEntries
- Use ParentId to load a Tree with all child nodes instead of CTE
