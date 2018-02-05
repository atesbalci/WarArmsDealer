namespace Game.Models {
    [Stat("Health")]
    public class HealthStat : Stat {
        public HealthStat() {
            Value = 1;
            Type = StatType.Health;
        }
    }
}