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

        public string ReturnTechName(ResearchType rType, WeaponType wType, int level)
        {
            string name = "";
            if(rType == ResearchType.Stat)
            {
                switch (wType)
                {
                    case WeaponType.Infantry:

                        if (level == 4)
                            name = "Weapon Sights";
                        if (level == 5)
                            name = "Multi-purpose Backpacks";
                        if (level == 6)
                            name = "Steel Helmets";
                        if (level == 7)
                            name = "Rainproof Materials";
                        if (level == 8)
                            name = "Adv. Trigger Mechanisms";
                        if (level == 9)
                            name = "Rifling";
                        if (level == 10)
                            name = "Preloaded Magazines";
                        if (level == 11)
                            name = "Machine Guns";
                        if (level == 12)
                            name = "Scopes";

                        break;
                    case WeaponType.Tank:
                        if (level == 4)
                            name = "Armored Tractors";
                        if (level == 5)
                            name = "Adv. Combustion Engines";
                        if (level == 6)
                            name = "Steel Casting";
                        if (level == 7)
                            name = "Suspension Tracks";
                        if (level == 8)
                            name = "Aluminium-Steel Alloys";
                        if (level == 9)
                            name = "Adv. Welding Process";
                        if (level == 10)
                            name = "Automatic Reloaders";
                        if (level == 11)
                            name = "Superheated Alloys";
                        if (level == 12)
                            name = "Replacable Parts";
                        break;
                    case WeaponType.Artillery:
                        if (level == 4)
                            name = "Artillery Shells";
                        if (level == 5)
                            name = "Pre-heated Ammunition";
                        if (level == 6)
                            name = "High-explosive Chemicals";
                        if (level == 7)
                            name = "Diamond Rifling Mechanisms";
                        if (level == 8)
                            name = "Longer Barrels";
                        if (level == 9)
                            name = "Alloy-cored Shells";
                        if (level == 10)
                            name = "Mobile Platforms";
                        if (level == 11)
                            name = "Aiming Mechanisms";
                        if (level == 12)
                            name = "Industrial Forging";
                        if (level == 13)
                            name = "Ballistic Computation";
                        break;
                    default:
                        break;
                }
            }
            else if (rType == ResearchType.Design)
            {

                name = "Design Techniques";
            }
            else if (rType == ResearchType.Tech)
            {
                name = "Computation Tools";
            }

            return name;
        }
        public string ReturnTechDesc(ResearchType rType, WeaponType wType, int level)
        {
            string desc = "";
            if (rType == ResearchType.Stat)
            {
                switch (wType)
                {
                    case WeaponType.Tank:

                        if (level == 4)
                            desc = "With armored tractors infantry is better protected against fire.";
                        
                        if (level == 5)
                            desc = "These newer combustuion engines can support the heavier loads brought by tanks. Unlocks Cast Engine trait.";
                        if (level == 6)
                            desc = "Steel can be cast in large sheets making a durable armor. Unlocks Steel Plating trait.";
                        if (level == 7)
                            desc = "Suspension tracks makes it possible to traverse through harsh terrain conditions.";
                        if (level == 8)
                            desc = "Inclusion of aluminum in alloys creates light and durable materials. Unlocks Light Chassis trait.";
                        if (level == 9)
                            desc = "Larger parts can be combined to form a single structure with welding. Unlocks Armor Skirts trait.";
                        if (level == 10)
                            desc = "Automatic reloaders increase the rate of fire significantly and more relaible than manual loading.";
                        if (level == 11)
                            desc = "Superheated alloys can bent at will to create more efficient shapes. Unlocks Rounded Armor trait";
                        if (level == 12)
                            desc = "Replaceable parts makes the on-field repairs possible. Unlocks the Repair Corps trait.";

                        break;
                    case WeaponType.Infantry:
                        if (level == 4)
                            desc = "Weapon sights improve the accuracy of  a given weapon considerably. Unlocks Light Infantry trait.";
                        if (level == 5)
                            desc = "With multi-purpose backpacks, supplying other troops on the battlefield is much easier. Unlocks Survival Kits trait.";
                        if (level == 6)
                            desc = "Steel helmets reduce the causalities by protecting infantry from shell explosions and sharapnels";
                        if (level == 7)
                            desc = "With rainproof materials it is easier to traverse in harsh conditions, giving infantry a edge. Unlocks Rainproof Gear trait.";
                        if (level == 8)
                            desc = "With advanced trigger mechanisms it is possible to fire at a faster rate. Unlocks Semi-automatic Rifles trait.";
                        if (level == 9)
                            desc = "Rifling increases the accuracy using rotational inertia.";
                        if (level == 10)
                            desc = "Preloaded magazines makes easier to resupply the troops and increases the fire rate. Unlocks Engineers trait.";
                        if (level == 11)
                            desc = "Machine guns dominate the infantry battles with incredible firing rates. Unlocks Full-automatic rifles trait.";
                        if (level == 12)
                            desc = "With precisely engineered scopes range of the rifles can be extended beyond the normal sight range";
                        break;
                    case WeaponType.Artillery:
                        if (level == 4)
                            desc = "Switching from cannonbals to shells increase the range and accuracy dramatically.";
                        if (level == 5)
                            desc = "Preheated ammunition is more reliable in terms of impact response and more effective against soft targets";
                        if (level == 6)
                            desc = "Highly explosive chemicals enables sharapnel storms across the battlefield. Unlocks Canister Shot trait.";
                        if (level == 7)
                            desc = "Diamond tipped drills can create large rifled barrels. Unlocks Rifled Cannons trait.";
                        if (level == 8)
                            desc = "Longer barrels increases penetration power and range significantly";
                        if (level == 9)
                            desc = "Replacing shell cores with advanced alloys increases pierce capability. Unlocks Tungsten Rounds trait.";
                        if (level == 10)
                            desc = "Self-propelled artillery platforms provide much needed manuverability. Unlocks Self-Propelled trait.";
                        if (level == 11)
                            desc = "Dynamic aiming mechanisms increase accuracy and aiming time.";
                        if (level == 12)
                            desc = "Huge forges can create smoothbored large barrels which are powerful. Unlocks Smoothbore trait.";
                        if (level == 13)
                            desc = "Mechanisms for simple calculations are actually very complex. Unlocks Ballistic Calculator trait.";
                        break;
                        
                    default:
                        break;
                }
            }
            else if(rType == ResearchType.Design)
            {

                desc = "Increased efficiency in designing and prototyping";
            }
            else if(rType == ResearchType.Tech)
            {
                desc = "Creating tools for more efficient research.";
            }
            return desc;
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
