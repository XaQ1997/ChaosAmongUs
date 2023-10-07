using HarmonyLib;
using ChaosAmongUs.Roles;
using ChaosAmongUs.Gameplay;
using UnityEngine;

namespace ChaosAmongUs.CrewmateRoles.ReviverMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.SetTarget))]
    public class KillButtonTarget
    {
        public static byte DontRevive = byte.MaxValue;

        public static void SetTarget(KillButton __instance, DeadBody target, Reviver role)
        {
            if (target != null && target.ParentId == DontRevive) target = null;
            role.CurrentTarget = target;

            __instance.graphic.color = Palette.DisabledClear;
            __instance.graphic.material.SetFloat("_Desat", 1f);
        }
    }
}