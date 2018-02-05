namespace Game.Models {
    [Stat("Piercing")]
    public class PiercingStat : Stat {
        public PiercingStat() {
            Value = 1;
            Type = StatType.Piercing;
        }
    }
}
