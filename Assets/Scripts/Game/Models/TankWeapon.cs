using System.Collections.Generic;

namespace Game.Models {
    public class TankWeapon : Weapon {
        public TankWeapon() {
            Stats[(int) StatType.Armor].Value = 1;

            Type = WeaponType.Tank;
        }

        public TankWeapon(KeyValuePair<StatType, int>[] keyValuePair) {
            foreach (var t in keyValuePair) {
                Stats[(int)t.Key].Value = t.Value;
            }

            Type = WeaponType.Tank;
        }
    }
}

