del *.nupkg

nuget restore

msbuild CDA.sln /p:Configuration=Release

NuGet.exe pack CDA.Generator/CDA.Generator.csproj -Properties Configuration=Release -IncludeReferencedProjects
NuGet.exe pack DigitalHealth.Hl7ToCdaTransformer/DigitalHealth.Hl7ToCdaTransformer.csproj -Properties Configuration=Release -IncludeReferencedProjects

pause

forfiles /m *.nupkg /c "cmd /c NuGet.exe push @FILE -Source https://www.nuget.org/api/v2/package"
