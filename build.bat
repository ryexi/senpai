@echo off
setlocal
set "Configuration=%~dp0build.json"
goto :Main

::
:: Summary:
::      The debug build.
::
:1 Debug
    call dotnet build "%~dp0src/Senpai/Senpai.csproj" "/property:GenerateFullPaths=true" "/p:AssemblyVersion=0.0.0" "/property:Version=0.0.0" --configuration Debug
    exit /b

::
:: Summary:
::      The production-ready build.
::
:2 Release
    call :WriteLine "Todo." 91m
    exit /b

::
:: Summary:
::      Build and pack the current project into a nuget.pkg.
:: 
:3 Publish
    setlocal
    set "Src=%~dp0src\Senpai"
    set "Project=%Src%\Senpai.csproj"
    set "NugetDir=%Src%\bin\Release\net6.0\publish"
    
    call :ManageVersion
    call :GetVersionPrefix Prefix
    call :GetVersionSuffix Suffix

    :: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-pack
    :: https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli
    dotnet pack "%Project%" "/property:VersionPrefix=%Prefix%" "/property:VersionSuffix=%Suffix%" -c Release -o "%NugetDir%"
    :: dotnet nuget push 
    exit /b

::
:: Summary:
::      Displays an error text and terminates the script/process.
:: 
:: Parameters:
::    %1:
::      A string representing the error text.
:: 
:Throw (Text)
    call :WriteLine "Exception thrown: %~1" 91m
    exit 1

::
:: Summary:
::      Display a normal or color'd text.
:: 
:: Parameters:
::    %1:
::      A string representing the text.
:: 
::    %2:
::      https://ss64.com/nt/syntax-ansi.html
::
:WriteLine (Text, ANSIColor)
    setlocal
    if [%1] == [] (
        echo.
        exit /b
    )

    if [%2] == [] (
        call cmd /c "echo %~1"
        exit /b
    )

    :: LRed         91m
    :: LGreen       92m
    :: LYellow      93m
    :: LCyan        96m
    :: LMagenta     95m
    :: Reset         0m
    set "color=[%~2"
    call cmd /c "echo %color%%~1[0m"
    exit /b

::
:: Summary:
::      Displays a red text.
:: 
:: Parameters:
::    %1:
::      The text.
:: 
:WriteError (Text)
    call :WriteLine "%~1" 91m
    exit /b

::
:: Summary:
::      Display a text with some padding.
:: 
:: Parameters:
::    %1:
::      A string representing the text.
::
::    %2:
::      https://stackoverflow.com/a/41954792
:: 
:WritePadded (Text, ANSIColor)
    if not defined WhiteSpace (
        call :GetWhiteSpace
    )

    if [%2] == [] (
        call :WriteLine "%WhiteSpace%%~1"
    ) else (
        call :WriteLine "%WhiteSpace%%~1" %2
    )
    exit /b

::
:: Summary:
::      Returns whitespaces that can be inserted at the beginning of a user-input.
::
:: Returns:
::    %WhiteSpace%
::      A var containing the spaces.
::
:GetWhiteSpace
    :: https://stackoverflow.com/a/9865960
    for /f %%A in ('"prompt $H &echo on &for %%B in (1) do rem"') do set "WhiteSpace=%%A  "
    exit /b

::
:: Summary:
::      null
::
:: Parameters:
::    %1:
::      A variable to store the input.
::
:: Returns:
::    %VarName%:
::      The input from the cin.
::
:UserSelect (VarName)
    if [%1] == [] (
        call :throw "A variable name wasn't provided. (UserSelect)"
    )

    if not defined WhiteSpace (
        call :GetWhiteSpace
    )

    echo.
    set /p "%~1=%WhiteSpace%Select: "
    echo.
    exit /b

::
:: Summary:
::      -
:: 
:: Parameters:
::    %1:
::      The path to the 'build.json' file.
::
::    %2:
::      The property to search and its variable name which will contain the result.
::
:: Returns:
::    %VarName%:
::       The result of the property %2.
::
:GetVersion (FileName, Property)
    if "[%1]" == "[]" (
        call :throw "Path not defined."
    )

    if [%2] == [] (
        call :throw "Specify a value to get."
    )

    for /f "tokens=*" %%a in ('pwsh -Command "(Get-Content "%~1" -Raw | ConvertFrom-Json).Version | Select -ExpandProperty '%~2'"') do set "%~2=%%a"
    exit /b

::
:: Summary:
::      -
::
:: Parameters:
::    %1:
::      The path to the 'build.json' file.
::
::    %2:
::      The value to set to the 'current' property.
::
::    %3:
::      The value to set to the 'previous' property.
::
:SetVersion (FileName, Current, Previous)
    if [%1] == [] (
        call :throw "Undefined path. (SetVersion)"
    )

    if [%2] == [] (
        call :throw "Undefined parameter. (SetVersion)"
    )

    if [%3] == [] (
        call :throw "Undefined parameter. (SetVersion)"
    )
    
    pwsh -Command "ConvertTo-Json -InputObject @{Version=[ordered]@{Current='%~2'; Previous='%~3';}} | Out-File %1 -NoNewLine"
    exit /b

::
:: Summary:
::      Get the version suffix. (Format: Alphanumberic (+ hyphen) string: [0-9A-Za-z-]*)
:: 
:: Parameters:
::    %1:
::      The variable to declare and store the suffix.
::
:GetVersionSuffix
    if [%1] == [] (
        call :throw "A variable name is required. (GetVersionSuffix)"
    )

    call :SemVerParser %Configuration%

    if not [%PreRelease%] == [] (
        set "%~1=%PreRelease%.%Build%.%Revision%"
    )
    exit /b

::
:: Summary:
::     Display the current version number.
:: 
:DisplayVersion
    setlocal
    call :GetVersion %Configuration% Current
    call :WriteLine "The current version is '%Current%'" 93m
    exit /b

::
:: Summary:
::      Get the version prefix. (Format: major.minor.patch[.build])
:: 
:: Parameters:
::    %1:
::      The variable to declare and store the prefix.
::
:GetVersionPrefix
    if [%1] == [] (
        call :throw "A variable name is required. (GetVersionPrefix)"
    )

    call :SemVerParser %Configuration%
    set "%~1=%Major%.%Minor%.%Patch%"
    exit /b
::
:: Summary:
::      Parse a semantic version number.
:: 
:: Parameters:
::    %1:
::      The pathname of the 'version.json' file.
::
:: Returns:
::    %Major%:
::       The major integer of the sem-ver number.
::
::    %Minor%:
::       The minor integer of the sem-ver number.
::
::    %Patch%:
::       The patch integer of the sem-ver number.
::
::    %PreRelease%:
::       The pre-release value, it is either one of these value. (alpha, beta or rc)
::
::    %Build%:
::       The current build of the pre-release version.
::
::    %Revision%:
::       The revision int of the current %Build%.
::
::    %Current%:
::       The current version number.
::
::    %Previous%:
::       The previous version number.
::
:SemVerParser (FileName)
    if "[%1]" == "[]" (
        call :throw "FileName is not defined."
    )

    call :GetVersion %1 Current
    ::call :GetVersion %1 Previous

    :: Parsing X.Y.Z
    for /F "tokens=1,2,3 delims=." %%a in ("%Current%") do (
        set Major=%%a
        set Minor=%%b
        set Patch=%%c
    )

    :: Parsing pre-release
    set PreRelease=

    :: https://stackoverflow.com/a/7006016
    if not "x%Patch%" == "x%Patch:-alpha=%" (
        set "PreRelease=alpha"
        set "Patch=%Patch:-alpha=%"
    )

    if not "x%Patch%" == "x%Patch:-beta=%" (
        set "PreRelease=beta"
        set "Patch=%Patch:-beta=%"
    )

    if not "x%Patch%" == "x%Patch:-rc=%" (
        set "PreRelease=rc"
        set "Patch=%Patch:-rc=%"
    )

    :: Parsing build and revision
    if not [%PreRelease%] == [] (
        for /F "tokens=4,5 delims=." %%a in ("%Current%") do (
            set "build=%%a"
            set "revision=%%b"
        )
    )
    exit /b

:RefreshVersion
    :: Todo, add check
    set "Current=%Major%.%Minor%.%Patch%"
    if not [%PreRelease%] == [] (
        set "Current=%Current%-%PreRelease%.%Build%.%Revision%"
    )
    exit /b

:IncrementMajor
    set /a "Major = %Major% + 1"
    set "Minor=0"
    set "Patch=0"
    exit /b

:IncrementMinor
    set /a "Minor = %Minor% + 1"
    set "Patch=0"
    exit /b

:IncrementPatch
    set /a "Patch = %Patch% + 1"
    exit /b

:IncrementBuild
    set /a "Build=%Build%+1"
    set "Revision=0"
    exit /b

:IncrementRevision
    set /a "Revision=%Revision% + 1"
    exit /b

:IncrementPreRelease
    if [%1] == [] (
        call :throw "No can do (IncrementPreRelease)"
    )

    set "PreRelease=%~1"
    set "Build=1"
    set "Revision=0"
    exit /b

::
:: Summary:
::      Manage the version number of the project.
:: 
:: Parameters:
::    %1:
::      -
::
:ManageVersion
    setlocal

    :: Parses the version
    call :SemVerParser %Configuration%
    call :WriteLine "Current version: %Current%" 95m
    call :WriteLine

    :: Options
    :OptionMenu
    set "Option="
    set "Options=1,2,3,4"
    set "Previous=%Current%"
    Echo Select a version to increment. (Leave blank for auto-incrementing)

    if [%PreRelease%] == [] (
        call :WritePadded "1. Major"
        call :WritePadded "2. Minor"
        call :WritePadded "3. Patch"
        call :WritePadded "4. Pre-Release"
    ) else (
        set "Options=1,2"
        call :WritePadded "1. Major (Release)"
        call :WritePadded "2. Pre-Release"
    )
    call :UserSelect Option

    :: Auto-incrementing
    if [%Option%] == [] (
        :: If it's not a pre-release, increment the patch #, otherwise the revision #
        if [%PreRelease%] == [] (
            call :IncrementPatch
        ) else (
            call :IncrementRevision
        )
        goto :Eol
    )

    for %%a in (%Options%) do (
        if [%Option%] == [%%a] (
            goto :V%%a
        )
    )

    call :WriteError "Not a valid option."
    goto :OptionMenu
    
    :V1
        if [%PreRelease%] == [] (
            call :IncrementMajor
        ) else (
            set "PreRelease="
        )
        goto :Eol
    :V2
        if [%PreRelease%] == [] (
            call :IncrementMinor
        ) else (
            goto :V4
        )
        goto :Eol
    :V3
        call :IncrementPatch
        goto :Eol
    :V4
        rem Lazy coding.
        
        Echo Select the pre-release version to increment to.

        if [%PreRelease%] == [] (
            call :WritePadded "1. Alpha"
            call :WritePadded "2. Beta"
            call :WritePadded "3. Release Candidate"
        ) else (
            if /i "%PreRelease%" == "alpha" (
                call :WritePadded "1. Alpha (Current)"
                call :WritePadded "2. Beta"
                call :WritePadded "3. Release Candidate"
            )
            if /i "%PreRelease%" == "beta" (
                call :WritePadded "1. Alpha"
                call :WritePadded "2. Beta (Current)"
                call :WritePadded "3. Release Candidate"
            )
            if /i "%PreRelease%" == "rc" (
                call :WritePadded "1. Alpha"
                call :WritePadded "2. Beta"
                call :WritePadded "3. Release Candidate (Current)"
            )
        )

        set "Option="
        call :UserSelect Option

        if [%Option%] == [] (
            call :WriteError "Not a valid option."
            goto :V4
        )

        if %Option% == 1 (
            set "Identifier=alpha"
        )

        if %Option% == 2 (
            set "Identifier=beta"
        )

        if %Option% == 3 (
            set "Identifier=rc"
        )

        rem No pre-release identifier, incrementing to next major 
        rem and applying the pre-release identifier
        if [%PreRelease%] == [] (
            call :IncrementMajor
            call :IncrementPreRelease %Identifier%
            goto :Eol
        )

        rem No need to change anything, proceed to build n rev.
        if /i "%PreRelease%" == "%Identifier%" (
            goto :V5
        )

        rem Is a pre-release but the identifier doesn't match up to the option selected.
        rem Nothing can upgrade to alpha
        if %Option% == 1 (
            call :WriteError "Cannot increment to an inferior version."
            goto :V4
        )

        rem Only alpha can be upgraded to beta.
        if %Option% == 2 (
            if /i "%PreRelease%" == "alpha" (
                call :IncrementPreRelease %Identifier%
                goto :Eol
            ) else (
                call :WriteError "Cannot increment to an inferior version."
                goto :V4
            )
        )

        rem Anything else is below it
        if %Option% == 3 (
            call :IncrementPreRelease %Identifier%
            goto :Eol
        )
        goto :Eol

    :V5
        Echo Select a version to increment.
        call :WritePadded "1. Build"
        call :WritePadded "2. Revision (Default)"
        
        set "Option="
        call :UserSelect Option

        if [%Option%] == [] (
            call :IncrementRevision
            goto :Eol
        )

        if %Option% == 1 (
            call :IncrementBuild
        )

        if %Option% == 2 (
            call :IncrementRevision
        )
        goto :Eol

    :Eol
        call :RefreshVersion

        if [%Previous%] == [] (
            call :WriteLine "Upgrading to '%Current%'" 95m
        ) else (
            call :WriteLine "Upgrading from '%Previous%' to '%Current%'" 95m
        )
        
        :: Save config
        call :SetVersion %Configuration% %Current% %Previous%

        endlocal
        exit /b

::
:: Summary:
::      Roll-back to a previous version number, if possible.
::
:RollBack
    setlocal
    call :GetVersion %Configuration% Current
    call :GetVersion %Configuration% Previous

    if [%Previous%] == [] (
        call :WriteLine "No prior version found to rollback." 93m
        exit /b
    )

    call :WriteLine "Rolling back from '%Current%' to '%Previous%'" 93m
    call :SetVersion %Configuration% %Previous% ""
    exit /b

::
:: Summary:
::      The script's main entry.
::
:Main
    set "Option=%~1"
    set "Options=1,2,3"

    if not [%1] == [] (
        if /i [%1] == [rollback] (
            call :RollBack & exit /b
        )

        if /i [%1] == [v] (
            call :DisplayVersion & exit /b
        )

        if /i [%1] == [version] (
            call :DisplayVersion & exit /b
        )
    )

    Echo Target a build by specifying the numeric value of the options below.
    call :WritePadded "1. Debug"
    call :WritePadded "2. Release"
    call :WritePadded "3. Publish"

    if [%1] == [] (
        call :UserSelect Option
    ) else (
        Echo.
    )

    for %%a in (%Options%) do (
        if [%Option%] == [%%a] (
            set "Invoked=true"
            call :WriteLine "Targeting build: %%a" 95m
            goto :%%a
        )
    )

    if not defined Invoked (
        call :throw "Specified value doesn't correlate with the available options."
    )
    exit /b