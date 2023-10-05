namespace ChaosAmongUs.Gameplay
{
    public enum DisableSkipButtonMeetings
    {
        No,
        Emergency,
        Always
    }

    public static class CustomGameOptions
    {
        public static bool ColourblindComms = false;
        public static bool SeeTasksDuringRound = true;
        public static bool SeeTasksDuringMeeting = false;
        public static bool SeeTasksWhenDead = true;
    }
}
