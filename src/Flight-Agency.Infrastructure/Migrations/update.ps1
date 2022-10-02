$env:DB_PASS = gcloud secrets versions access latest --secret=db_pass
$env:DB_NAME = gcloud secrets versions access latest --secret=db_name
$env:DB_USER = gcloud secrets versions access latest --secret=db_username
$env:DB_HOST = gcloud secrets versions access latest --secret=db_host

$delta = Get-Date -Format "MM-dd-yyyy"
dotnet ef database update --project ..\Flight-Agency.Infrastructure.csproj