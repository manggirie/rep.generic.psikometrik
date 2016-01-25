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

> You also can copy and paste ALL file in Rx Package BUT make sure you not REPLACE the JPA files and just click SKIP when the dialog appeared.


## First Time Setup
1. Click on ControlCenter.bat in `C:\project\rep.generic.psikometrik` for Installation Rx Developer.
2. Give your application a name as '**epsikologi**'.
3. Click NEXT and NEXT, BUT make sure your JAVA_HOME set properly before click SETUP.
4. After Setup Completed and Display congratulation message, run all Rx Engine: RabbitMQ, SQL LocalDB, Elasticsearch, SPH Worker and IIS Express.

```
You can run Rx Developer via 'View App' link or on Browser with 'http://localhost:50230/'
```


## Building your solution
Open a command prompt and run this command (Make sure all Rx Engine started before run this command / script)
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
If still error, please install these package (open solution with **Visual Studio**).
tool>NuGet Package Manager>Package Manager Console
```
* Install-Package Microsoft.Net.Http
* Install-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform
* Install-Package Aspose.Pdf
* Install-Package EPPlus


> Normally after this step, you will successfully enter Rx Developer with JPA Psikometrik system.
