using UnityEngine;

namespace Game.Models
{
    public class Research : ModelBase
    {
        public Weapon Weapon { get; set; }
        public StatType StatType { get; set; }

        public Research(Weapon weapon) {
            this.Weapon = weapon;
        }

        public Research(Weapon weapon, StatType p_StatType) {
            this.Weapon = weapon;
            this.StatType = p_StatType;
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the design duration of a research
        /// </summary>
        public int CalculateDuration()
        {
            float duration = 0f;
            switch (Weapon.Type) {
                case WeaponType.Infantry:
                    duration += Weapon.Stats[(int)StatType.Attack].Value + Weapon.Stats[(int)StatType.Health].Value + Weapon.Stats[(int)StatType.Support].Value;
                    break;
                case WeaponType.Tank:
                    duration += Weapon.Stats[(int)StatType.Attack].Value + Weapon.Stats[(int)StatType.Health].Value + Weapon.Stats[(int)StatType.Armor].Value;
                    break;
                case WeaponType.Artillery:
                    duration += Weapon.Stats[(int)StatType.Attack].Value + Weapon.Stats[(int)StatType.Health].Value + Weapon.Stats[(int)StatType.Piercing].Value;
                    break;
                default:
                    break;
            }

            return Mathf.RoundToInt(Mathf.Pow(2f, duration / 3.0f) * 1f);
        }
    }
}
