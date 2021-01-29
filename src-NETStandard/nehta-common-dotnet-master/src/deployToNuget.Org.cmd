del *.nupkg

msbuild /p:Configuration=Release

NuGet.exe pack Common\Common.csproj  -Properties Configuration=Release

pause

forfiles /m *.nupkg /c "cmd /c NuGet.exe push @FILE -Source https://www.nuget.org/api/v2/package"

