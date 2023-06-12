namespace TravelAgency.Application;

public class Config
{
	public required string AppUrl { get; set; }
	public required string AdminUserEmail { get; set; }
	public required string AdminUserPassword { get; set; }
    public required string AdminUserNumber { get; set; }
    public required string AzureStorageName { get; set; }
    public required string AzureStorageKey { get; set; }
    public string? AzureStorageContainerName { get; set; }
}
