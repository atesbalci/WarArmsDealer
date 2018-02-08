using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class RifledCannons : Trait
    {

        public RifledCannons()
        {
            Name = "Rifled Cannons:";
            Description = "Increases the piercing by 25% but decreases the attack by 10%";
            Complexity = 2;
        }

        public override float[] ApplyTrait(Weapon weapon, Nation weaponNation, Nation oppositeNation)
        {

            Debug.Assert(weapon.Type == WeaponType.Artillery);
            float[] traitModifiers = new float[3];
            int count = 0;
            foreach (var val in weapon[weapon.GetStatTypes()])
            {
                traitModifiers[count] = val.Value;
                count++;
            }
            traitModifiers[0] *= 0.9f;
            traitModifiers[2] *= 1.25f; //25% increase
            return traitModifiers;
        }

    }
}


