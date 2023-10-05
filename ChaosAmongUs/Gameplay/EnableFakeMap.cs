using Discord;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace ChaosAmongUs.Gameplay
{
    [HarmonyPatch(typeof(GameSettingMenu), nameof(GameSettingMenu.InitializeOptions))]
    public class EnableFakeMap
    {
        private static void Prefix(ref GameSettingMenu __instance)
        {
            __instance.HideForOnline = new Il2CppReferenceArray<Transform>(0);
        }
    }

    [HarmonyPatch(typeof(ImpostorRole), nameof(ImpostorRole.CanUse))]
    public class FakeTasks
    {
        private static bool Prefix(ImpostorRole __instance, ref IUsable usable, ref bool __result)
        {
            __result = true;
            return false;
        }
    }
}