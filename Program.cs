using System.Diagnostics;

if (args.Length == 0 || args[0] != "Task.java")
{
    Console.WriteLine("Usage: dotnet run Task.java");
    return;
}

string javaFile = args[0];
string directory = Directory.GetCurrentDirectory();
string javaPath = Path.Combine(directory, javaFile);
string className = Path.GetFileNameWithoutExtension(javaFile);

ProcessStartInfo javacInfo = new ProcessStartInfo
{
    FileName = "javac",
    Arguments = javaPath,
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    UseShellExecute = false,
    CreateNoWindow = true,
    WorkingDirectory = directory
};

Process javacProcess = Process.Start(javacInfo) ?? throw new InvalidOperationException("Failed to start javac process.");
javacProcess.WaitForExit();

if (javacProcess.ExitCode != 0)
{
    Console.WriteLine("Error compiling Task.java:");
    Console.WriteLine(javacProcess.StandardError.ReadToEnd());
    return;
}

ProcessStartInfo javaInfo = new ProcessStartInfo
{
    FileName = "java",
    Arguments = className,
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    UseShellExecute = false,
    CreateNoWindow = true,
    WorkingDirectory = directory
};

Process javaProcess = Process.Start(javaInfo) ?? throw new InvalidOperationException("Failed to start java process.");
javaProcess.WaitForExit();

Console.WriteLine(javaProcess.StandardOutput.ReadToEnd());