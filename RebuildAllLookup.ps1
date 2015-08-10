$root = "C:\project\rep.generic.psikometrik\"
$sw = [System.Diagnostics.Stopwatch]::StartNew();

$names = ls -Path "$root\sources\EntityDefinition" -Filter "*.mapping"
$names = [System.String]::Join("|", $names).Replace(".mapping", "")
#Write-Host $names
$xml = (Get-Content "$root\web\Web.config") -as [xml]

$xml.SelectSingleNode('//system.webServer/rewrite/rules/rule[@name="entity.search"]/match/@url').'#text' = "search/($names)/"
$xml.SelectSingleNode('//system.webServer/rewrite/rules/rule[@name="entity.api"]/match/@url').'#text' = "api/($names)/"

$xml.Save("$root\web\web.config")


$files = ls -Filter *.json -Path "$root\sources\EntityDefinition"
foreach($t in $files){
    $name = "$t".Replace(".json", "")
    $json =[System.IO.File]::ReadAllText("$root\sources\EntityDefinition\$t")
    #Write-Host $json
    if($json.ToString().Contains("`"TreatDataAsSource`": true,") -eq $true)
    {
        Write-Host "Copying $name ...."
        $folder = "$root\sources\$name";
        if((Test-Path($folder)) -eq $false)
        {
            mkdir $folder
        }
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
  
    
    if($json.ToString().Contains("`"TreatDataAsSource`": true,") -eq $true)
    {
        Write-Host "Coying $name ...."
       copy "$root\output\epsikologi.$name.dll" "$root\subscribers\"
       copy "$root\output\epsikologi.$name.pdb" "$root\subscribers\"

       copy "$root\output\epsikologi.$name.dll" "$root\web\bin\"
       copy "$root\output\epsikologi.$name.pdb" "$root\web\bin\"
    }
}

Write-Host "Done..."

copy .\lib\EPPlus.dll .\web\bin\

