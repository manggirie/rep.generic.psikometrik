$root = "C:\project\rep.generic.psikometrik\"
$files = ls -Filter *.json -Path "$root\sources\WorkflowDefinition"
foreach($t in $files){

    Write-Host "Compiling $name"
    .\tools\sph.builder.exe /q "$root\sources\WorkflowDefinition\$t"

    
 
}

Write-Host "Deploying dll, Please wait...."
sleep -Seconds 1

foreach($t in $files){
    $name = "$t".Replace(".json", "")

    Write-Host "Copying workflows.$name.0.dll"
   
    copy "$root\output\workflows.$name.0.dll" "$root\subscribers\"
    copy "$root\output\workflows.$name.0.pdb" "$root\subscribers\"
    
}