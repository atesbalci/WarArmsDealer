namespace Game.Models {
    public class TankWeapon : Weapon {
        [Stat("Armor")]
        public ArmorStat ArmorStat { get; set; }

        public TankWeapon() {
            this.ArmorStat = new ArmorStat();
        }
    }
}

