namespace Game.Models {
    public class Weapon : ModelBase {
        public HealthStat HealthStat { get; set; }
        public AttackStat AttackStat { get; set; }

        public Weapon() {
            this.HealthStat = new HealthStat();
            this.AttackStat = new AttackStat();
        }
    }
}
