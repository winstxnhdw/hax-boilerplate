@echo off

set project_name=hax
dotnet build %project_name%
start /wait /b ./submodules/SharpMonoInjectorCore/dist/SharpMonoInjector.exe inject -p " " -a bin/%project_name%.dll -n Hax -c Loader -m Load

pause
