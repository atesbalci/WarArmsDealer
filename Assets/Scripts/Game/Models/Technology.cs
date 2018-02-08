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

        public List<Trait> InfantryTraits;
        public List<Trait> TankTraits;
        public List<Trait> ArtilleryTraits;

        public int TechLevel, DesignLevel;

        public Technology() {

            TechLevel = 1;
            DesignLevel = 1;

            //LoadTraits();
            InfantryTraits = new List<Trait>();
            TankTraits = new List<Trait>();
            ArtilleryTraits = new List<Trait>();
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

        public string ReturnTechName(ResearchType type, int level)
        {
            return "Ultimate Tech";
        }
        public string ReturnTechDesc(ResearchType type, int level)
        {
            return "Ulitmate Desc";
        }

        void LoadTraits()
        {
           

            //Infantry
            InfantryTraits.Add(new Engineer());

            //Tank
            TankTraits.Add(new RoundedArmor());
            TankTraits.Add(new TungstenRounds());
        }


        public void UnlockTrait(WeaponType type, int level)
        {
            var levelOffset = 2; //If levels start at 3 it has to be 2 etc.
            switch (type)
            {
                case WeaponType.Infantry:

                    if(level==levelOffset+2)
                        InfantryTraits.Add(new LightInfantry());
                    if(level==levelOffset+3)
                        InfantryTraits.Add(new SurvivalKits());
                    if (level==levelOffset+ 5)
                        InfantryTraits.Add(new RainnproofGear());
                    if (level==levelOffset+ 6)
                        InfantryTraits.Add(new SemiAutomatic());
                    if (level==levelOffset+ 8)
                        InfantryTraits.Add(new Engineer());
                    if (level==levelOffset+ 9)
                        InfantryTraits.Add(new FullAutomatic());
                    break;
                case WeaponType.Tank:
                    if (level==levelOffset+ 3)
                        TankTraits.Add(new CastEngine());
                    if (level==levelOffset+ 4)
                        TankTraits.Add(new SteelPlating());
                    if (level==levelOffset+ 6)
                        TankTraits.Add(new LightChasis());
                    if (level==levelOffset+ 7)
                        TankTraits.Add(new ArmorSkirts());
                    if (level==levelOffset+ 9)
                        TankTraits.Add(new RoundedArmor());
                    if (level==levelOffset+ 10)
                        TankTraits.Add(new RepairCorps());
                    break;
                case WeaponType.Artillery:

                    if (level == levelOffset + 4)
                        ArtilleryTraits.Add(new CanisterShot());
                    if (level == levelOffset + 5)
                        ArtilleryTraits.Add(new RifledCannons());
                    if (level == levelOffset + 7)
                        ArtilleryTraits.Add(new TungstenRounds());
                    if (level == levelOffset + 8)
                        ArtilleryTraits.Add(new SelfPropelled());
                    if (level == levelOffset + 10)
                        ArtilleryTraits.Add(new Smoothbore());
                    if (level == levelOffset + 11)
                        ArtilleryTraits.Add(new BalisticCalculator());
                    break;
                default:
                    break;
            }
        }
    }
}
