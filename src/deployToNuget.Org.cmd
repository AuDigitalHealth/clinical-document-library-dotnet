del *.nupkg

msbuild CDA.sln /p:Configuration=Release

REM Use dotnet for packaging now
REM NuGet.exe pack CDA.Generator/CDA.Generator.csproj -Properties Configuration=Release -IncludeReferencedProjects
REM NuGet.exe pack DigitalHealth.Hl7ToCdaTransformer/DigitalHealth.Hl7ToCdaTransformer.csproj -Properties Configuration=Release -IncludeReferencedProjects
dotnet pack .\CDA.Generator\CDA.Generator.csproj -c Release -o .
dotnet pack .\DigitalHealth.Hl7ToCdaTransformer\DigitalHealth.Hl7ToCdaTransformer.csproj -c Release -o .

forfiles /m *.nupkg /c "cmd /c NuGet.exe push @FILE -Source https://www.nuget.org/api/v2/package"
