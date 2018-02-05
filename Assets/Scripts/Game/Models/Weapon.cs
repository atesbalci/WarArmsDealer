﻿using System;

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
    }
}
