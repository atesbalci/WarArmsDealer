using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class Nation : ModelBase
    {
        public string Name { get; set; }
        public float Manpower { get; set; }

        public InfantryWeapon CurrentInfantry;
        public TankWeapon CurrentTank;
        public ArtilleryWeapon CurrentArtillery;

        public WeaponBase[] Weapons;



    }
}
