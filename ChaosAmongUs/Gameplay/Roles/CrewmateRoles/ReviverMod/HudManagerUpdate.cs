using HarmonyLib;
using ChaosAmongUs.Roles;
using ChaosAmongUs.Gameplay;
using UnityEngine;

namespace ChaosAmongUs.CrewmateRoles.ReviverMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (PlayerControl.LocalPlayer.Data.IsDead) return;

            var role = Role.GetRole<Reviver>(PlayerControl.LocalPlayer);

            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var truePosition = PlayerControl.LocalPlayer.GetTruePosition();
            var allocs = Physics2D.OverlapCircleAll(truePosition, 1.0f,
                LayerMask.GetMask(new[] { "Players", "Ghost" }));

            var killButton = __instance.KillButton;
            DeadBody closestBody = null;
            var closestDistance = float.MaxValue;

            foreach (var collider2D in allocs)
            {
                if (isDead || collider2D.tag != "DeadBody") continue;
                var component = collider2D.GetComponent<DeadBody>();

                var distance = Vector2.Distance(truePosition, component.TruePosition);
                if (!(distance < closestDistance)) continue;
                closestBody = component;
                closestDistance = distance;
            }

            killButton.gameObject.SetActive((__instance.UseButton.isActiveAndEnabled)
                    && !MeetingHud.Instance && !PlayerControl.LocalPlayer.Data.IsDead
                    && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started);

            KillButtonTarget.SetTarget(killButton, closestBody, role);
            __instance.KillButton.SetCoolDown(0f, 1f);
        }
    }
}