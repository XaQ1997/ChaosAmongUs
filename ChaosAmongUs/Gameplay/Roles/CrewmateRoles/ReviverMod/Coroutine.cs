using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities.Extensions;
using ChaosAmongUs.Roles;
using ChaosAmongUs.Gameplay;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ChaosAmongUs.CrewmateRoles.ReviverMod
{
    public class Coroutine
    {
        public static PlayerControl Target;

        public static IEnumerator ReviverRevive(DeadBody target, Reviver role)
        {
            var parentId = target.ParentId;
            var position = target.TruePosition;

            var revived = new List<PlayerControl>();

            if (target != null)
            {
                foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
                {
                    if (deadBody.ParentId == target.ParentId) deadBody.gameObject.Destroy();
                }
            }

            var startTime = DateTime.UtcNow;
            while (true)
            {
                var now = DateTime.UtcNow;
                var seconds = (now - startTime).TotalSeconds;
                if (seconds < CustomGameOptions.ReviveDuration)
                    yield return null;
                else break;

                if (MeetingHud.Instance) yield break;
            }

            foreach (DeadBody deadBody in GameObject.FindObjectsOfType<DeadBody>())
            {
                if (deadBody.ParentId == role.Player().PlayerId) deadBody.gameObject.Destroy();
            }

            var player = Utils.PlayerById(parentId);

            player.Revive();

            revived.Add(player);

            if (target != null) Object.Destroy(target.gameObject);

            if (revived.Any(x => x.AmOwner))
                try
                {
                    Minigame.Instance.Close();
                    Minigame.Instance.Close();
                }
                catch
                {
                }
        }
    }
}