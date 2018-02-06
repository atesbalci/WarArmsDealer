using System.Collections.Generic;

namespace Game.Models {
    public class TankWeapon : Weapon {
        public TankWeapon() {
            this[StatType.Armor].Value = 1;

            Type = WeaponType.Tank;
        }

        public TankWeapon(KeyValuePair<StatType, int>[] keyValuePair) {
            foreach (var t in keyValuePair) {
                this[t.Key].Value = t.Value;
            }

            Type = WeaponType.Tank;
        }
    }
}

