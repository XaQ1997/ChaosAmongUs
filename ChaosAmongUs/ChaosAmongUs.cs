using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChaosAmongUs
{
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public partial class ChaosAmongUs : BasePlugin
    {
        public const string Id = "pl.xaviush.chaosamongus";
        public const string VersionString = "0.0.3";
        public static System.Version Version = System.Version.Parse(VersionString);

        public static Vector3 ButtonPosition { get; private set; } = new Vector3(2.6f, 0.7f, -9f);

        public Harmony _harmony;

        public ConfigEntry<string> Ip { get; set; }

        public ConfigEntry<ushort> Port { get; set; }

        public override void Load()
        {
            _harmony = new Harmony("pl.xaviush.chaosamongus");

            Ip = Config.Bind("Custom", "Ipv4 or Hostname", "127.0.0.1");
            Port = Config.Bind("Custom", "Port", (ushort) 22023);
            var ip = Ip.Value;
        }
    }
}