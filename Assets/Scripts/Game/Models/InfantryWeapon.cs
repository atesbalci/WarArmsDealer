using System.Collections.Generic;

namespace Game.Models {
    public class InfantryWeapon : Weapon {
        public InfantryWeapon() : base() {
            Stats[(int) StatType.Support].Value = 1;

            Type = WeaponType.Infantry;
        }

        public InfantryWeapon(KeyValuePair<StatType, int>[] keyValuePair) {
            foreach (var t in keyValuePair) {
                Stats[(int)t.Key].Value = t.Value;
            }

            Type = WeaponType.Infantry;
        }
    }
}
