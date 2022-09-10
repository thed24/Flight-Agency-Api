$env:DB_PASS = gcloud secrets versions access latest --secret=db_pass
$env:DB_NAME = gcloud secrets versions access latest --secret=db_name
$env:DB_USER = gcloud secrets versions access latest --secret=db_username
$env:DB_HOST = gcloud secrets versions access latest --secret=db_host
$env:GOOGLE_API_KEY = gcloud secrets versions access latest --secret=google-api-key
$env:ASPNETCORE_ENVIRONMENT = "Development"

dotnet run