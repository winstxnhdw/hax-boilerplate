using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

const string ProcessName = "";

Console.WriteLine("Waiting for the game to launch...");
Process? process;

while ((process = Process.GetProcessesByName(ProcessName).SingleOrDefault()) is null) {
    Thread.Sleep(1000);
}

using Process game = process;
_ = game.WaitForInputIdle();
using Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Launcher.Payload.hax.dll")
    ?? throw new InvalidOperationException("The launcher does not contain hax.dll. Rebuild the launcher.");
byte[] payload = new byte[resource.Length];
resource.ReadExactly(payload);
Console.WriteLine("Loading hax...");
MonoInjector.Inject(game, payload);
Console.WriteLine("hax loaded.");

if (!Console.IsInputRedirected && !Console.IsOutputRedirected) {
    Console.WriteLine("Press any key to close.");
    _ = Console.ReadKey(intercept: true);
}
