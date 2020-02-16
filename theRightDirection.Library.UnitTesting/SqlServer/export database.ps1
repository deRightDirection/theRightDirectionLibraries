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
		[string] $serverName,
	## sql-script om uit te voeren
	[Parameter(
		Mandatory = $false,
		Position = 0,
		ValueFromPipeline = $false,
		ValueFromPipelineByPropertyName = $false)]
		[string] $sqlScript
)
import-Module SQLPS -DisableNameChecking
$dt = Get-Date -Format ddMMyyyy
$fileName = "c:\temp\$($databaseName)_db_$($dt).bak"
If (Test-Path $fileName)
{
	Remove-Item $fileName
}
Backup-SqlDatabase -ServerInstance $serverName -Database $databaseName -BackupFile $fileName
if ($variablename) 
{
	invoke-sqlcmd -inputfile $sqlScript -serverinstance $serverName -database $databaseName
}