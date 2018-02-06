using System.Collections.Generic;

namespace Game.Models {
    public class InfantryWeapon : Weapon {
        public InfantryWeapon() : base() {
            this[StatType.Support].Value = 1;

            Type = WeaponType.Infantry;
        }

        public InfantryWeapon(KeyValuePair<StatType, int>[] keyValuePair) {
            foreach (var t in keyValuePair) {
                this[t.Key].Value = t.Value;
            }

            Type = WeaponType.Infantry;
        }
    }
}
