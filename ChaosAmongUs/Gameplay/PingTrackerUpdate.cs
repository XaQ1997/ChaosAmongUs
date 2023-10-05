using HarmonyLib;
using UnityEngine;

namespace ChaosAmongUs
{
    //[HarmonyPriority(Priority.VeryHigh)] // to show this message first, or be overrided if any plugins do
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {

        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            var position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(3.6f, 0.1f, 0);
            position.AdjustPosition();

            __instance.text.text =
                (!MeetingHud.Instance?"<color=#44FF00FF>Chaos Among Us v" + ChaosAmongUs.VersionString + "</color>\n":null) +
                $"Ping: {AmongUsClient.Instance.Ping}ms\n" +
                (!MeetingHud.Instance?"<color=#00FF00FF>Modded By: Xaviush</color>\n":null);
        }
    }
}