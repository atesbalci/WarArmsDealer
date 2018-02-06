using System.Collections.Generic;

namespace  Game.Models {
    public class ArtilleryWeapon : Weapon {
        public ArtilleryWeapon() : base() {
            this[StatType.Piercing].Value = 1;
            Type = WeaponType.Artillery;
        }

        /// <summary>
        /// Pass any amount of stat as KeyValuePair
        /// </summary>
        /// <param name="keyValuePair"></param>
        public ArtilleryWeapon(KeyValuePair<StatType,int>[] keyValuePair) {
            foreach (var t in keyValuePair) {
                this[t.Key].Value = t.Value;
            }

            Type = WeaponType.Artillery;
        }
    }
}