for /f %%i in ('dir Common.Client\Common.Client.Http\src\bin\Release /b/a-d/od/t:c') do set LAST=%%i
echo The most recently created file is %LAST%

Nuget\nuget.exe push -Source jopalesha -ApiKey AzureDevOps Common.Client\Common.Client.Http\src\bin\Release\%LAST%

