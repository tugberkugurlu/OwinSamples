@echo off
%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
.nuget\NuGet.exe install .nuget\packages.config -o packages