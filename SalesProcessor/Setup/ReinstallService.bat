net stop SalesProcessorService

sc delete SalesProcessorService

cd %~dp0

cd ..\bin\Debug\

SalesProcessor.exe install

net start SalesProcessorService

pause