msbuild Keystroke.API.csproj /t:Build /p:Configuration="Release 3.5"
msbuild Keystroke.API.csproj /t:Build /p:Configuration="Release 4.0"
msbuild Keystroke.API.csproj /t:Build /p:Configuration="Release 4.5"
msbuild Keystroke.API.csproj /t:Build;Package;Publish /p:Configuration="Release 4.6"