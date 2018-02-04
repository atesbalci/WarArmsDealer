namespace Game.Models {
    public class TankWeapon : Weapon {
        public ArmorStat ArmorStat { get; set; }

        public TankWeapon() {
            this.ArmorStat = new ArmorStat();
        }
    }
}

