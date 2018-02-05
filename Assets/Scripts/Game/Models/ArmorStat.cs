namespace Game.Models {
    [Stat("Armor")]
    public class ArmorStat : Stat {
        public ArmorStat() {
            Value = 1;
            Type = StatType.Armor;
        }
    }
}