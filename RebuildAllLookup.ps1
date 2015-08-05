$root = "C:\project\rep.generic.psikometrik\"
$files = ls -Filter *.json -Path "$root\sources\EntityDefinition"
foreach($t in $files){
    $name = "$t".Replace(".json", "")
    $json =[System.IO.File]::ReadAllText("$root\sources\EntityDefinition\$t")
    #Write-Host $json
    if($json.ToString().Contains("`"TreatDataAsSource`": true,") -eq $true)
    {
        
        $folder = "$root\sources\$name";
        if((Test-Path($folder)) -eq $false)
        {
            mkdir $folder
        }
        Write-Host "Compiling $name"
        .\tools\sph.builder.exe /q "$root\sources\EntityDefinition\$t"

    }
 
}

Write-Host "Deploying dll, Please wait...."
sleep -Seconds 1

foreach($t in $files){
    $name = "$t".Replace(".json", "")
    $json =[System.IO.File]::ReadAllText("$root\sources\EntityDefinition\$t")
  
    
    if($json.ToString().Contains("`"TreatDataAsSource`": true,") -eq $true)
    {
       copy "$root\output\epsikologi.$name.dll" "$root\subscribers\"
       copy "$root\output\epsikologi.$name.pdb" "$root\subscribers\"

       copy "$root\output\epsikologi.$name.dll" "$root\web\bin\"
       copy "$root\output\epsikologi.$name.pdb" "$root\web\bin\"
    }
}