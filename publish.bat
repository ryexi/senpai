:: This script is only for versioning and publishing to https://nuget.org
:: To build the lib, just 'cd senpai' and call 'dotnet build'.

@echo off

if [%1] == [] (
    call :throw "No parameter were provided."
)

:: https://semver.org
if not %1 == Major (
    if not %1 == Minor (
        if not %1 == Patch (
            call :throw "The parameter `'%~1`' is either in lowercase or doesn`'t match the SemVer format."
        )
    )
)

set "file=%~dp0.version"
set "directory=%~dp0Senpai/"
for /F "tokens=1,2,3 delims=." %%a in (%file%) do (
    set major=%%a
    set minor=%%b
    set patch=%%c
)

set "version=%major%.%minor%.%patch%"
set "previous=%version%"

call :%1 %major% %minor% %patch%
call :echo "Upgrading from `'%previous%`' to `'%version%`'" "Magenta"

:: https://stackoverflow.com/a/11336754
< NUL set /p="%version%" > %file%

:: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-pack
:: https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli
dotnet pack "%directory%Senpai.csproj" -c "Release" -o "%directory%bin/Nuget"
:: dotnet nuget push 
exit


:Major
    set /a var = %1 + 1
    set /a %2 = 0
    set /a %3 = 0
    set "version=%var%.%2.%3"
    exit /b

:Minor
    set /a var = %2 + 1
    set /a %3 = 0
    set "version=%1.%var%.%3"
    exit /b

:Patch
    set /a var = %3 + 1
    set "version=%1.%2.%var%"
    exit /b

:throw
    call :echo "Exception thrown: %~1" "DarkRed"
    exit 1

:echo
    if [%2] == [] (
        powershell -command write-host %1
        exit /b
    )
    if [%3] == [] (
        powershell -command write-host %1 -foreground %2
        exit /b
    )
    if not %~3 == True (
        if not %~3 == False (
            call :throw "`'%~3`' isn`'t a valid paramater on :echo."
        )
    )
    if not %~3 == False (
        powershell -command write-host %1 -foreground %2 -NoNewline
    ) else (
        powershell -command write-host %1 -foreground %2
    )
    exit /b