namespace Game.Models {
    public class InfantryWeapon : Weapon {
        public SupportStat SupportStat { get; set; }

        public InfantryWeapon() {
            this.SupportStat = new SupportStat();
        }
    }
}
