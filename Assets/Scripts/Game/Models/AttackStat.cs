namespace Game.Models {
    [Stat("Attack")]
    public class AttackStat : Stat {
        public AttackStat() {
            Value = 1;
            Type = StatType.Attack;
        }
    }
}
