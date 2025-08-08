using System;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace Hax;

sealed class Loader : MonoBehaviour {
    const string HarmonyID = "winstxnhdw.valheim-hax";
    static GameObject HaxGameObjects { get; } = new GameObject();
    public static GameObject HaxModules { get; } = new GameObject();
    static bool HasLoaded => Harmony.HasAnyPatches(Loader.HarmonyID);
    static void AddHaxModules<T>() where T : Component => Loader.HaxModules.AddComponent<T>();
    static void AddHaxGameObject<T>() where T : Component => Loader.HaxGameObjects.AddComponent<T>();

    static void LoadLibraries() {
        Assembly assembly = Assembly.GetExecutingAssembly();

        foreach (string resourceName in assembly.GetManifestResourceNames().Where(name => name.EndsWith(".dll"))) {
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            _ = AppDomain.CurrentDomain.Load(memoryStream.ToArray());
        }
    }

    internal static void Load() {
        Loader.LoadLibraries();

        if (Loader.HasLoaded) {
            Logger.Write("hax has already loaded!");
            return;
        }

        Loader.LoadHarmonyPatches();
    }

    static void LoadHarmonyPatches() {
        try {
            new Harmony(Loader.HarmonyID).PatchAll();
        }

        catch (Exception exception) {
            Logger.Write(exception.ToString());
            throw;
        }
    }

    internal static void Unload() {
        Destroy(Loader.HaxModules);
        Destroy(Loader.HaxGameObjects);
        new Harmony(Loader.HarmonyID).UnpatchAll();
    }
}
