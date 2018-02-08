using UnityEngine;

namespace Game.Models
{
    public enum ResearchType {
        Stat,
        Design,
        Tech
    }

    public class Research : ModelBase
    {
        public Weapon Weapon { get; set; }
        public StatType StatType { get; set; }
        public ResearchType ResearchType { get; set; }

        public Research(Weapon weapon) {
            this.Weapon = weapon;
            this.ResearchType = ResearchType.Stat;
        }

        public Research(Weapon weapon, StatType p_StatType) : this(weapon){
            this.StatType = p_StatType;
        }

        public Research(Weapon weapon, ResearchType p_ResearchType) : this(weapon) {
            this.ResearchType = p_ResearchType;
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
                    duration += Weapon[StatType.Attack].Value + Weapon[StatType.Health].Value + Weapon[StatType.Support].Value;
                    break;
                case WeaponType.Tank:
                    duration += Weapon[StatType.Attack].Value + Weapon[StatType.Health].Value + Weapon[StatType.Armor].Value;
                    break;
                case WeaponType.Artillery:
                    duration += Weapon[StatType.Attack].Value + Weapon[StatType.Health].Value + Weapon[StatType.Piercing].Value;
                    break;
                default:
                    break;
            }

            return Mathf.RoundToInt(Mathf.Pow(2f, duration / 3.0f) * 1f);
        }
        public float GetCost()
        {
            return (Weapon[StatType.Attack].Value + 1) * 250f;
        }
    }
    
}
