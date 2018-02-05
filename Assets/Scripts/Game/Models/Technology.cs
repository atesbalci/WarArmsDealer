using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class Technology : ModelBase
    {
        private const int DefaultTechLevel = 3;

        public List<Research> Researches;
        public Weapon[] Weapons;

        public Technology()
        {
            var statCnt = Enum.GetValues(typeof(StatType)).Length;

            Weapons = new Weapon[System.Enum.GetNames(typeof(WeaponType)).Length];

            Weapons[0] = new InfantryWeapon(new KeyValuePair<StatType, int>[3] {
                new KeyValuePair<StatType, int>(StatType.Attack, DefaultTechLevel)
                ,new KeyValuePair<StatType, int>(StatType.Health, DefaultTechLevel)
                ,new KeyValuePair<StatType, int>(StatType.Support, DefaultTechLevel)});

            Weapons[1] = new TankWeapon(new KeyValuePair<StatType, int>[3] {
                new KeyValuePair<StatType, int>(StatType.Attack, DefaultTechLevel)
                ,new KeyValuePair<StatType, int>(StatType.Health, DefaultTechLevel)
                ,new KeyValuePair<StatType, int>(StatType.Armor, DefaultTechLevel)});

            Weapons[2] = new ArtilleryWeapon(new KeyValuePair<StatType, int>[3] {
                new KeyValuePair<StatType, int>(StatType.Attack, DefaultTechLevel)
                ,new KeyValuePair<StatType, int>(StatType.Health, DefaultTechLevel)
                ,new KeyValuePair<StatType, int>(StatType.Piercing, DefaultTechLevel)});

            Researches = new List<Research>();
        }

        public void AddResearch(Research research) {
            Researches.Add(research);

            for (int i = 0; i < research.Weapon.Stats.Length; i++) {
                Weapons[(int) research.Weapon.Type].Stats[i].Value
                    = Mathf.Max(Weapons[(int) research.Weapon.Type].Stats[i].Value, research.Weapon.Stats[i].Value);
            }
        }
    }
}
