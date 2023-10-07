using ChaosAmongUs.Gameplay;

namespace ChaosAmongUs.Roles
{
    public class Reviver : Role
    {
        public bool CurrentlyReviving;
        public DeadBody CurrentTarget;

        public bool ReviveUsed;

        public Reviver(PlayerControl player) : base(player)
        {
            Name = "Reviver";
            ImpostorText = () => "Sacrifice Yourself To Save Another";
            TaskText = () => "Revive a dead body at the cost of your own life";
            RoleType = RoleEnum.Reviver;
            AddToRoleHistory(RoleType);
        }
    }
}