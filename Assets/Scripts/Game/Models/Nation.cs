using System;

namespace Game.Models
{
    public class Nation : ModelBase
    {
        public string Name { get; set; }

        public float Manpower {
            get { return _manpower; }
            set { _manpower = value >= 0 ? value : 0; }
        }
        private float _manpower;

        public InfantryWeapon CurrentInfantry;
        public TankWeapon CurrentTank;
        public ArtilleryWeapon CurrentArtillery;

        public Weapon[] Weapons;
        public int Casualities;

        public Nation() {
            CurrentInfantry = new InfantryWeapon();
            CurrentTank = new TankWeapon();
            CurrentArtillery = new ArtilleryWeapon();

            Weapons = new Weapon[3];

            Weapons[0] = CurrentInfantry;
            Weapons[1] = CurrentTank;
            Weapons[2] = CurrentArtillery;

            Manpower = 10000;
        }

        public Nation(string name) {
            CurrentInfantry = new InfantryWeapon();
            CurrentTank = new TankWeapon();
            CurrentArtillery = new ArtilleryWeapon();

            Weapons = new Weapon[3];

            Weapons[0] = CurrentInfantry;
            Weapons[1] = CurrentTank;
            Weapons[2] = CurrentArtillery;

            Manpower = 10000;

            Name = name;
        }

        /// <summary>
        /// TODO : Handle trust and other mechanics
        /// </summary>
        /// <param name="p_Weapon"></param>
        /// <param name="p_CallbackAction">Handle callback with it</param>
        public void BuyWeapon(Weapon p_Weapon, Action p_CallbackAction) {
            int curStatSum = 0;
            foreach (var stat in Weapons[(int)p_Weapon.Type].Stats) {
                curStatSum += stat.Value;
            }

            int newStatSum = 0;
            foreach (var stat in p_Weapon.Stats) {
                newStatSum += stat.Value;
            }

            if (newStatSum >= curStatSum) {
                Weapons[(int)p_Weapon.Type] = p_Weapon.Copy();

                if (p_CallbackAction != null)
                    p_CallbackAction.Invoke();
            }
        }
    }
}
