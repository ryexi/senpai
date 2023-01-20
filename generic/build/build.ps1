$ErrorActionPreference = "Stop";

# Reserved variables.
$BuildTag       ??= '0.0.0';
$BuildNumber    ??= 0;
$BuildVersion   ??= '0.0.0';
$BuildVersionId ??= 'gold';

# Vars
$cwd = $pwd.Path;
$Project = 'Senpai.sln';
$NugetDir = Join-Path $cwd 'artifacts\NuGet';

# Build info.
Echo @("Date    $(Get-Date)"
       "Build   $BuildNumber"
       "Version $BuildVersion`n"
       "Building...`n");

# Content of the build script:

dotnet test @("$Project"
              '-s'
              './.runsettings');

if ($LASTEXITCODE -ne 0) {
    throw;
}

dotnet pack @("$Project"
              "/p:Version=$BuildVersion"
              '/p:Configuration=Release'
              "/p:PackageOutputPath=$NugetDir");

if ($LASTEXITCODE -ne 0) {
    throw;
}

# dotnet nuget push