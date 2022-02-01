using BenchmarkDotNet.Running;

using static System.Console;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();

WriteLine("\n\nPress any key ....");
ReadKey();
