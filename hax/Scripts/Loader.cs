using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace Hax;

public class Loader : MonoBehaviour {
    static GameObject HaxGameObject { get; } = new GameObject();
    public static GameObject HaxModules { get; } = new GameObject();

    static void AddHaxModules<T>() where T : Component => Loader.HaxModules.AddComponent<T>();
    static void AddHaxGameObject<T>() where T : Component => Loader.HaxGameObject.AddComponent<T>();

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
        Loader.LoadHaxGameObjects();
        Loader.LoadHaxModules();

        AppDomain.CurrentDomain.AssemblyResolve -= Loader.OnResolveAssembly;
    }

    static void LoadHarmonyPatches() {
        try {
            new Harmony("winstxnhdw.hax").PatchAll();
        }

        catch (HarmonyException exception) {
            Logger.Write(exception.ToString());
        }
    }

    static void LoadHaxGameObjects() {
        DontDestroyOnLoad(Loader.HaxGameObject);
    }

    static void LoadHaxModules() {
        DontDestroyOnLoad(Loader.HaxModules);
    }

    public static void Unload() {
        Destroy(Loader.HaxModules);
        Destroy(Loader.HaxGameObject);
    }
}
