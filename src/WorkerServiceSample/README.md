An overly complicated way of getting one's public IPv4 address using .NET Core 3.0 worker service.
The address is then put into a txt file in Azure Blob Container.

dotnet publish -c Release
sc.exe create WorkerServiceSample start=auto binpath=<PATH>

