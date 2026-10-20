# hax-boilerplate

A boilerplate for developing mods for Unity applications.

## Rationale

Most open-source Unity mod distributions use a plugin framework like BepInEx. These frameworks rely on both developers and end-users to modify the game's files. Not only is this not a good user experience, but it also pollutes the game's files with unnecessary modifications. This boilerplate uses the local `launcher/` project to perform runtime injection of the mod's code into the game's Mono process. The compiled payload is embedded in `launcher.exe`, so no separate injector or submodules are required.

## Setup

Use Windows 10 or later, [Git](https://git-scm.com/download/win), and the [Microsoft .NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0).

Fill in the payload under `hax/` and supply your game's reference assemblies in `hax/References/`. Set the empty `ProcessName` constant in `launcher/Program.cs` to the game's process name without `.exe`.

The launcher targets a 64-bit Mono runtime and loads `hax.dll` through `Hax.Loader.Load`. Update `launcher/launcher.csproj` and the constants in `launcher/MonoInjector.cs` if you change the payload assembly or entry point.

## Usage

Once you have configured the boilerplate, run `launch.bat`. It updates the repository, publishes the launcher, waits for the game, and loads the embedded payload.
