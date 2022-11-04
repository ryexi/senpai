# Tested on PowerShell 7.2
# For more information, refer to https://github.com/imdying/kio

ImportDependencies;

function Publish {
    $Project = 'Senpai.sln';
    $Version = PromptVersionControl;

    dotnet pack @("$Project"
                "/p:Version=$($Version.Absolute)"
                "/p:VersionPrefix=$($Version.Prefix)"
                "/p:VersionSuffix=$($Version.Suffix)"
                '/p:Configuration=Release'
                '--output'
                '.\artifacts\NuGet');
    # dotnet nuget push
}