Param(
       [string]$DatabaseServer = '(localdb)\Projects',
       [string]$ElasticsearchHost = 'localhost',
       [int]$ElasticsearchPort = 9200
     )
 Write-Host "This script will delete ALL your data, type [Yes] to continue" -ForegroundColor Yellow -BackgroundColor Red
$continue = Read-Host
if($continue -ne "Yes"){
    exit
    return
}

$es = $ElasticsearchHost + ":" + $ElasticsearchPort
$root = "C:\project\rep.generic.psikometrik\"
$sw = [System.Diagnostics.Stopwatch]::StartNew();

$names = ls -Path "$root\sources\EntityDefinition" -Filter "*.mapping"
$names = [System.String]::Join("|", $names).Replace(".mapping", "")
#Write-Host $names
$xml = (Get-Content "$root\web\Web.config") -as [xml]

$xml.SelectSingleNode('//system.webServer/rewrite/rules/rule[@name="entity.search"]/match/@url').'#text' = "search/($names)/"
$xml.SelectSingleNode('//system.webServer/rewrite/rules/rule[@name="entity.api"]/match/@url').'#text' = "api/($names)/"

$xml.Save("$root\web\web.config")

#Import-Module .\utils\sqlcmd.dll

$files = ls -Filter *.json -Path "$root\sources\EntityDefinition"
foreach($t in $files){
    $name = "$t".Replace(".json", "")
    $json =[System.IO.File]::ReadAllText("$root\sources\EntityDefinition\$t")
    #Write-Host $json
    if($json.ToString().Contains("`"TreatDataAsSource`": true,") -eq $false)
    {
        Write-Host "TRUNCATE TABLE $name ...."
        Invoke-SqlCmdRx -TrustedConnection -Server $DatabaseServer -Database "epsikologi" -CommandQuery "TRUNCATE TABLE [epsikologi].[$name]"

        $lowered = $name.ToLowerInvariant()
        curl -Method Delete -Uri "http://$es/epsikologi/$lowered/_query" -Body '{"query": {"match_all": {}}}'

        Write-Host "Compiling $name"
        .\tools\sph.builder.exe /q "$root\sources\EntityDefinition\$t"
        $elapse = $sw.Elapsed;
        Write-Host "$elapse taken to compile $name"

    }
 
}


$systems = @("audittrail","binarystore","message","organization","page","reportdelivery","tracker","userprofile","watcher","workflow");
foreach($name in $systems){
 
        Write-Host "TRUNCATE TABLE $name ...."
        Invoke-SqlCmdRx -TrustedConnection -Server $DatabaseServer -Database "epsikologi" -CommandQuery "TRUNCATE TABLE [sph].[$name]"


        curl -Method Delete -Uri "http://$es/epsikologi_sys/$name/_query" -Body '{"query": {"match_all": {}}}'    
 
}

$sw.Stop();



Write-Host "Deploying dll, Please wait...."
sleep -Seconds 1

foreach($t in $files){
    $name = "$t".Replace(".json", "")
    $json =[System.IO.File]::ReadAllText("$root\sources\EntityDefinition\$t")
  
    
    if($json.ToString().Contains("`"TreatDataAsSource`": true,") -eq $false)
    {
        Write-Host "Coying $name ...."
        copy "$root\output\epsikologi.$name.dll" "$root\subscribers\"
        copy "$root\output\epsikologi.$name.pdb" "$root\subscribers\"

        copy "$root\output\epsikologi.$name.dll" "$root\web\bin\"
        copy "$root\output\epsikologi.$name.pdb" "$root\web\bin\"
    }
}

Write-Host "Done..."
