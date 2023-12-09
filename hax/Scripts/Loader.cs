using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace Hax;

public class Loader : MonoBehaviour {
    static GameObject HaxGameObjects { get; } = new();
    static GameObject HaxModules { get; } = new();
    static Harmony Harmony { get; } = new("winstxnhdw.hax");

    static void AddHaxModules<T>() where T : Component => Loader.HaxModules.AddComponent<T>();
    static void AddHaxGameObjects<T>() where T : Component => Loader.HaxGameObjects.AddComponent<T>();

    static Assembly OnResolveAssembly(object _, ResolveEventArgs args) {
        Assembly assembly = Assembly.GetExecutingAssembly();

        using Stream stream = assembly.GetManifestResourceStream(
            assembly.GetManifestResourceNames()
                    .First(name => name.EndsWith($"{new AssemblyName(args.Name).Name}.dll"))
        );

        using MemoryStream memoryStream = new();
        stream.CopyTo(memoryStream);
        return Assembly.Load(memoryStream.ToArray());
    }

    public static void Load() {
        AppDomain.CurrentDomain.AssemblyResolve += Loader.OnResolveAssembly;

        Loader.LoadHarmonyPatches();
        Loader.LoadHaxGameObjectss();
        Loader.LoadHaxModules();

        AppDomain.CurrentDomain.AssemblyResolve -= Loader.OnResolveAssembly;
    }

    static void LoadHarmonyPatches() {
        try {
            Loader.Harmony.PatchAll();
        }

        catch (HarmonyException exception) {
            Logger.Write(exception.ToString());
        }
    }

    static void LoadHaxGameObjectss() {
        DontDestroyOnLoad(Loader.HaxGameObjects);
    }

    static void LoadHaxModules() {
        DontDestroyOnLoad(Loader.HaxModules);
    }

    public static void Unload() {
        Destroy(Loader.HaxModules);
        Destroy(Loader.HaxGameObjects);
        Loader.Harmony.UnpatchAll();
    }
}
