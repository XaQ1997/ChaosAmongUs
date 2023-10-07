namespace ChaosAmongUs.Gameplay
{
    class Utils
    {
        public static PlayerControl PlayerById(byte id)
        {
            foreach (var player in PlayerControl.AllPlayerControls)
                if (player.PlayerId == id)
                    return player;

            return null;
        }
    }
}
