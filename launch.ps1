Push-Location $PSScriptRoot
    docker-compose -f .\docker-compose.yml up --detach
    dotnet.exe ef database update `
        --project TreesApi.DataAccess\TreesApi.DataAccess.csproj `
        --startup-project TreesApi.Web\TreesApi.Web.csproj `
        --context TreesApi.DataAccess.ApplicationDbContext `
        --configuration Debug
Pop-Location
