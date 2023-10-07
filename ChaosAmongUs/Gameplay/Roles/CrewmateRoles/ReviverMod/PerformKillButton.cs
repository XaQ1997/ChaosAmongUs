using HarmonyLib;
using Hazel;
using Reactor.Utilities;
using ChaosAmongUs.Roles;
using ChaosAmongUs.Gameplay;
using UnityEngine;

namespace ChaosAmongUs.CrewmateRoles.ReviverMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKillButton
    {
        public static bool Prefix(KillButton __instance)
        {
            if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var role = Role.GetRole<Reviver>(PlayerControl.LocalPlayer);

            var flag2 = __instance.isCoolingDown;
            if (flag2) return false;
            if (!__instance.enabled) return false;
            if (role == null)
                return false;
            if (role.CurrentTarget == null)
                return false;
            var playerId = role.CurrentTarget.ParentId;
            var player = Utils.PlayerById(playerId);

            Coroutines.Start(Coroutine.ReviverRevive(role.CurrentTarget, role));
            return false;
        }
    }
}