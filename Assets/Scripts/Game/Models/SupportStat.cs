namespace Game.Models {
    [Stat("Support")]
    public class SupportStat : Stat {
        public SupportStat() {
            Value = 1;
            Type = StatType.Support;
        }
    }
}
