# Sistem Ujian Psikometrik - Rx Developer
This documents can be used for reference implementation for implementing **Sistem Ujian Psikometrik** for JPA using Rx Developer 1.x

##Installation
Download Rx Developer from [www.reactivedeveloper.com](http://www.reactivedeveloper.com/download) the latest 1.x.

## Getting the source code
You can clone this git repository to your `C:\project\rep.generic.psikometrik`, the copy these items from the RxDeveloper package into `C:\project\rep.generic.psikometrik`

* control.center
* IIS Express
* elasticsearch
* rabbitmq_server
* rabbitmq_base
* schedulers
* subscribers
* subscribers.host
* output
* tools
* utils
* web\bin
* web\Web.config
* ControlCenter.bat


## Building your solution
Open a command prompt and run this command
```
.\tools\sph.builder.exe
```

------------------------------

## Run Script
```
If still cannot run Rx and display error, please run these Rebuild Script.
```
* RebuildAllDataEntityDefinition
* RebuildAllLookup
* RebuildAllTrigger
* RebuildAllWorkflow

## Install Package
```
If still error, please install these package (open solution with Visual Basic).
tool>NuGet Package Manager>Package Manager Console
```
* Install-Package Microsoft.Net.Http
* Install-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform
* Install-Package Aspose.Pdf
* Install-Package EPPlus
