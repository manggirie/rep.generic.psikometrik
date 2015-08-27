Write-Host "This script will delete all your data, Type [Yes] to continue"
$continue = Read-Host
if($continue -ne "Yes"){
    exit
    return
}

$root = "C:\project\rep.generic.psikometrik\"
$sw = [System.Diagnostics.Stopwatch]::StartNew();

$names = ls -Path "$root\sources\EntityDefinition" -Filter "*.mapping"
$names = [System.String]::Join("|", $names).Replace(".mapping", "")
#Write-Host $names
$xml = (Get-Content "$root\web\Web.config") -as [xml]

$xml.SelectSingleNode('//system.webServer/rewrite/rules/rule[@name="entity.search"]/match/@url').'#text' = "search/($names)/"
$xml.SelectSingleNode('//system.webServer/rewrite/rules/rule[@name="entity.api"]/match/@url').'#text' = "api/($names)/"

$xml.Save("$root\web\web.config")


Write-Host "Removing all users except developers and administrators only"
curl -Method Delete -Uri "http://psikometrik/jpa-admin/all-users"

Import-Module .\utils\sqlcmd.dll

$files = ls -Filter *.json -Path "$root\sources\EntityDefinition"
foreach($t in $files){
    $name = "$t".Replace(".json", "")
    $json =[System.IO.File]::ReadAllText("$root\sources\EntityDefinition\$t")
    #Write-Host $json
    if($json.ToString().Contains("`"TreatDataAsSource`": true,") -eq $false)
    {
        Write-Host "TRUNCATE TABLE $name ...."
       
        Invoke-SqlCmdRx -TrustedConnection -Server "PSIKOMETRIK" -Database "epsikologi" -CommandQuery "TRUNCATE TABLE [epsikologi].[$name]"

        Write-Host "Compiling $name"
        .\tools\sph.builder.exe /q "$root\sources\EntityDefinition\$t"
        $elapse = $sw.Elapsed;
        Write-Host "$elapse taken to compile $name"

    }
 
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
