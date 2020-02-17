###################
## M.M. Etten    ##
## 11-11-2013    ##
## StartReady BV ##
###################
param(
	## naam van de database
	[Parameter(
		Mandatory = $true,
		Position = 0,
		ValueFromPipeline = $false,
		ValueFromPipelineByPropertyName = $false)]
		[string] $databaseName,
	## machine naam voor SQl Server
	[Parameter(
		Mandatory = $true,
		Position = 0,
		ValueFromPipeline = $false,
		ValueFromPipelineByPropertyName = $false)]
		[string] $serverName
)
Import-Module SQLPS -DisableNameChecking
$dt = Get-Date -Format ddMMyyyy
$fileName = "c:\temp\$($databaseName)_db_$($dt).bak"
$servicename = "MSSQL`$SQLEXPRESS"
net stop $servicename
net start $servicename
Restore-SqlDatabase -ServerInstance $serverName -Database $databaseName -BackupFile $fileName -RestoreAction Database -ReplaceDatabase
If (Test-Path $fileName)
{
	Remove-Item $fileName
}
exit