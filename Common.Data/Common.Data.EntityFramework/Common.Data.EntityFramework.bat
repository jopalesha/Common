for /f %%i in ('dir Common.Data\Common.Data.EntityFramework\src\bin\Release /b/a-d/od/t:c') do set LAST=%%i
echo The most recently created file is %LAST%

Nuget\nuget.exe push -Source jopalesha -ApiKey AzureDevOps Common.Data\Common.Data.EntityFramework\src\bin\Release\%LAST%

