for /f %%i in ('dir Common.Hosting\Common.Hosting.WindowsService\src\bin\Release /b/a-d/od/t:c') do set LAST=%%i
echo The most recently created file is %LAST%

Nuget\nuget.exe push -Source jopalesha -ApiKey AzureDevOps Common.Hosting\Common.Hosting.WindowsService\src\bin\Release\%LAST%

