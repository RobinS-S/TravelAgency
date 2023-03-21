param (
    [Parameter(Mandatory=$true)]
    [string]$MigrationName
)

if (-not [string]::IsNullOrWhiteSpace($MigrationName)) {
    $invalidChars = [System.IO.Path]::GetInvalidFileNameChars()
    $isValid = $MigrationName.IndexOfAny($invalidChars) -eq -1

    if ($isValid) {
        Add-Migration $MigrationName -Project TravelAgency.Infrastructure -StartupProject TravelAgency -OutputDir Data\Migrations
    } else {
        Write-Error "Invalid migration name: '$MigrationName'. It contains invalid characters."
    }
} else {
    Write-Error "Migration name is empty."
}
