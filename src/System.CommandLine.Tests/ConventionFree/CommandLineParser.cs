namespace System.CommandLine.Tests.ConventionFree
{
    public class CommandLineParser
    {
        internal static void Invoke<TRootCommand>(string[] args)
            where TRootCommand : Core.ICommand
        {
            // Populate the command hierarchy.
            // Configure parser/Invokations
            // Call Invoke with parseresult
        }
    }
}
