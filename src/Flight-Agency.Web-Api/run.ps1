$env:DatabaseSettings__Password = gcloud secrets versions access latest --secret=db_pass
$env:DatabaseSettings__DatabaseName = gcloud secrets versions access latest --secret=db_name
$env:DatabaseSettings__Username = gcloud secrets versions access latest --secret=db_username
$env:DatabaseSettings__Host = gcloud secrets versions access latest --secret=db_host
$env:GOOGLE_API_KEY = gcloud secrets versions access latest --secret=google-api-key
$env:ASPNETCORE_ENVIRONMENT = "Development"

dotnet run