$root = "C:\project\rep.generic.psikometrik\"
$files = ls -Filter *.json -Path "$root\sources\Trigger"
foreach($t in $files){

    Write-Host "Compiling $name"
    .\tools\sph.builder.exe /q "$root\sources\Trigger\$t"

    
 
}

Write-Host "Deploying dll, Please wait...."
sleep -Seconds 1

foreach($t in $files){
    $name = "$t".Replace(".json", "")
    Write-Host "Copying subscriber.trigger.$name.dll"
   
    copy "$root\output\subscriber.trigger.$name.dll" "$root\subscribers\"
    copy "$root\output\subscriber.trigger.$name.pdb" "$root\subscribers\"
    
}