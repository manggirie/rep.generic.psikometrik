if((Test-Path .\packages) -eq $false){
        mkdir .\packages
}
$pwd = pwd
$packagesFiles = ls -Path .\web -Recurse -Filter packages.config 
$packagesFiles | foreach{
    Write-Host "Reading "  $_.FullName.Replace($pwd,"").Replace("packages.config","") -ForegroundColor Cyan
    [xml]$packagesXml = Get-Content $_.FullName
    $packagesXml.packages.package | foreach    {
        $folder = ".\packages\" + $_.id + "." + $_.version
       
        if((Test-Path $folder) -eq $false){
            Write-Host ($folder.Replace(".\packages\","") + "....")
            $arg = "install " + $_.id + " -Version " + $_.version
           # Write-Host $arg
           Start-Process -FilePath ".\.nuget\NuGet.exe" -WorkingDirectory ".\packages" -WindowStyle Hidden -ArgumentList $arg > output.txt
            
        }
    }    
}

$nuget = gps nuget* | measure
while($nuget.Count -gt 0){
    Start-Sleep -Milliseconds 500
    Write-Host "." -NoNewline    
    $nuget = gps nuget* | measure
}
Write-Host "Done downloading nuget. packages"

if((Test-Path .\web\bin\roslyn) -eq $false){
        mkdir .\web\bin\roslyn
}

copy .\packages\Microsoft.Net.Compilers.1.0.0\tools\*.* .\web\bin\roslyn
copy .\lib\Aspose.Pdf.dll .\web\bin\
copy .\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\lib\net45\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.dll .\web\bin\

Write-Host -ForegroundColor Yellow 'Please insert this line into your web.config file on the second last line just before </configuration>'
Write-Host ' 
<system.codedom>
    <compilers>
        <compiler language="c#;cs;csharp" 
            extension=".cs" 
            type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" 
            warningLevel="4" 
            compilerOptions="/langversion:6 /nowarn:1659;1699;1701" />
    </compilers>
</system.codedom>
'