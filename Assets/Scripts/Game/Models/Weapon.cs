using System.Collections.Generic;
using UnityEngine;

namespace Game.Models {

    /// <summary>
    /// Infantry is 1, Tank is 2, Artillery is 3
    /// </summary>
    public enum WeaponType {
        Infantry,
        Tank,
        Artillery
    }

    /// <inheritdoc />
    /// <summary>
    /// Weapons Stats are held in an array Stats.
    /// Every weapon have every stat, only exclusive stats are set to "0"
    /// Extending class will set the necessary exclusive stat to "1"
    /// </summary>
    public class Weapon : ModelBase {
        public Stat[] Stats;
        public WeaponType Type;

        public Stat this[StatType type] {
            get { return Stats[(int)type]; }
            set { Stats[(int) type] = value; }
        }

        public Stat[] this[StatType[] types] {
            get {
                Stat[] temp = new Stat[types.Length];

                for (int i = 0, j = 0; i < Stats.Length; i++) {
                    if (Stats[i].Type == types[j]) {
                        temp[j] = new Stat {
                            Value = Stats[i].Value,
                            Type = Stats[i].Type
                        };
                        j++;
                    }
                }

                return temp;
            }
        }

        public static Weapon CreateWeapon(WeaponType type, int v1, int v2, int v3)
        {
            Weapon w;
            switch (type)
            {
                case WeaponType.Infantry:
                    w = new InfantryWeapon();
                    w.Stats[(int)StatType.Attack].Value = v1;
                    w.Stats[(int)StatType.Health].Value = v2;
                    w.Stats[(int)StatType.Support].Value = v3;
                    break;
                case WeaponType.Tank:
                    w = new TankWeapon();
                    w.Stats[(int)StatType.Attack].Value = v1;
                    w.Stats[(int)StatType.Health].Value = v2;
                    w.Stats[(int)StatType.Armor].Value = v3;
                    break;
                case WeaponType.Artillery:
                    w = new ArtilleryWeapon();
                    w.Stats[(int)StatType.Attack].Value = v1;
                    w.Stats[(int)StatType.Health].Value = v2;
                    w.Stats[(int)StatType.Piercing].Value = v3;
                    break;
                default:
                    return null;
                    
            }
            return w;
        }

        public Weapon() {
            Stats = new Stat[System.Enum.GetNames(typeof(StatType)).Length];

            Stats[(int) StatType.Attack] = new AttackStat();
            Stats[(int) StatType.Health] = new HealthStat();
            Stats[(int) StatType.Armor] = new ArmorStat();
            Stats[(int) StatType.Piercing] = new PiercingStat();
            Stats[(int) StatType.Support] = new SupportStat();

            Stats[(int) StatType.Armor].Value = 0;
            Stats[(int) StatType.Piercing].Value = 0;
            Stats[(int) StatType.Support].Value = 0;

            Type = WeaponType.Infantry;

            
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the design cost of a weapon
        /// </summary>
        public float GetCost()
        {
            float cost = 0f;
            switch (Type)
            {
                case WeaponType.Infantry:
                    cost += Stats[(int)StatType.Attack].Value + Stats[(int)StatType.Health].Value + Stats[(int)StatType.Support].Value;
                    break;
                case WeaponType.Tank:
                    cost += Stats[(int)StatType.Attack].Value + Stats[(int)StatType.Health].Value + Stats[(int)StatType.Armor].Value;
                    break;
                case WeaponType.Artillery:
                    cost += Stats[(int)StatType.Attack].Value + Stats[(int)StatType.Health].Value + Stats[(int)StatType.Piercing].Value;
                    break;
                default:
                    break;
            }
            return cost * 250f;
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the design duration of a weapon
        /// </summary>
        public float GetDuration()
        {
            float duration = 0f;
            switch (Type)
            {
                case WeaponType.Infantry:
                    duration += Stats[(int)StatType.Attack].Value + Stats[(int)StatType.Health].Value + Stats[(int)StatType.Support].Value;
                    break;
                case WeaponType.Tank:
                    duration += Stats[(int)StatType.Attack].Value + Stats[(int)StatType.Health].Value + Stats[(int)StatType.Armor].Value;
                    break;
                case WeaponType.Artillery:
                    duration += Stats[(int)StatType.Attack].Value + Stats[(int)StatType.Health].Value + Stats[(int)StatType.Piercing].Value;
                    break;
                default:
                    break;
            }
            return Mathf.Pow(2f, duration/3.0f) * 5f;
        }

        public override string ToString()
        {
            string result = "";
            switch (Type)
            {
                case WeaponType.Infantry:
                    result = "Infantry: " + Stats[(int)StatType.Attack].Value + " " + Stats[(int)StatType.Health].Value + " " + Stats[(int)StatType.Support].Value;
                    break;
                case WeaponType.Tank:
                    result = "Tank: " + Stats[(int)StatType.Attack].Value + " " + Stats[(int)StatType.Health].Value + " " + Stats[(int)StatType.Armor].Value;
                    break;
                case WeaponType.Artillery:
                    result = "Artillery: " + Stats[(int)StatType.Attack].Value + " " + Stats[(int)StatType.Health].Value + " " + Stats[(int)StatType.Piercing].Value;
                    break;
                default:
                    break;
            }
            return result;
        }

        public void AddStat(StatType statType, int change) {
            for (int i = 0; i < Stats.Length; i++) {
                if (i == (int) statType) {
                    Stats[i].Value += change;
                }
            }
        }

        public StatType[] GetStatTypes() {
            List<StatType> output = new List<StatType>();

            foreach (var stat in Stats) {
                if (stat.Value != 0)
                    output.Add(stat.Type);
            }

            return output.ToArray();
        }

        public Weapon Copy() {
            Weapon temp = new Weapon();

            for (int i = 0; i < temp.Stats.Length; i++) {
                temp.Stats[i].Value = this.Stats[i].Value;
            }

            temp.Type = this.Type;

            return temp;
        }

    }
}
