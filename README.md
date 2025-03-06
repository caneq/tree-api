# Run the application
- Execute the `.\deploy.ps1` script to compose the Web API and Postgres, and apply database migrations.
- Open http://localhost:5935/swagger/index.html in browser

# To Improve
- Use Full Text Index for JournalEntry Text column
- Generate sequential EventId using database Sequence
- Do not load Text column when loading Pageable JournalEntries
