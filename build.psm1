# Tested on PowerShell 7.2
# For more information, refer to https://github.com/imdying/kio
ImportDependencies;

$test = './test/Test.csproj';
$project = './src/Senpai.csproj';

function Debug {
    dotnet build $test
}

function Publish {
    $Version = PromptVersionControl;

    # https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-pack
    # https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli
    dotnet pack "$project" "/property:VersionPrefix=$($Version.Prefix)" "/property:VersionSuffix=$($Version.Suffix)" --configuration 'Release' --output './nuget'
    # dotnet nuget push 
}