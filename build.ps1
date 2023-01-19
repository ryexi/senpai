try {
    vortex build;
} catch [System.Management.Automation.CommandNotFoundException] {
    # Fall-back in case Vortex isn't installed on the local machine.
    dotnet publish "Senpai.sln" "/p:Configuration=Release";
}