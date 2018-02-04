namespace Game.Models {
    public class InfantryWeapon : Weapon {
        [Stat("Support")]
        public SupportStat SupportStat { get; set; }

        public InfantryWeapon() {
            this.SupportStat = new SupportStat();
        }
    }
}
