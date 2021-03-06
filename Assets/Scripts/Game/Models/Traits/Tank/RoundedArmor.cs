﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class RoundedArmor : Trait
    {
        public RoundedArmor()
        {
            Name = "Rounded Armor:";
            Description = "Rounded armor increases the armor by %10";
            Complexity = 2;
        }

        public override float[] ApplyTrait(Weapon weapon, Nation weaponNation, Nation oppositeNation)
        {
            
            Debug.Assert(weapon.Type == WeaponType.Tank);
            float[] traitModifiers = new float[3];
            int count = 0;
            foreach (var val in weapon[weapon.GetStatTypes()])
            {
                traitModifiers[count] = val.Value;
                count++;
            }
            traitModifiers[2] *= 1.25f; //10% increase



            return traitModifiers;
        }

    }
}


