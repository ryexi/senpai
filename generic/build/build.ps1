$ErrorActionPreference = "Stop";

# Reserved variables.
$BuildTag       ??= '0.0.0';
$BuildNumber    ??= 0;
$BuildVersion   ??= '0.0.0';
$BuildVersionId ??= 'gold';

# Vars
$Project = 'Senpai.sln';

# Build info.
Echo @("Date    $(Get-Date)"
       "Build   $BuildNumber"
       "Version $BuildVersion`n"
       "Building...`n");

# Content of the build script:
 dotnet pack @("$Project"
                "/p:Version=$BuildVersion"
                '/p:Configuration=Release'
                '--output'
                '.\artifacts\NuGet');

# dotnet nuget push