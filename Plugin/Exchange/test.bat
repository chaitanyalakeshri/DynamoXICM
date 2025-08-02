@echo off
setlocal

set "currentFolder=%~dp0"
set "rubyScriptPath=%currentFolder%test.rb"
"C:\Program Files\Autodesk\InfoWorks ICM Ultimate 2025\ICMExchange.exe" --version -- "%rubyScriptPath%" /ICM
rem "C:/Program Files/Innovyze Workgroup Client 2025.3/IExchange.exe" --version -- "%rubyScriptPath%" /ICM
pause