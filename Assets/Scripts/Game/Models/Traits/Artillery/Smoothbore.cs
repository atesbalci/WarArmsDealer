using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class Smoothbore : Trait
    {

        public Smoothbore()
        {
            Name = "Smoothbore Cannons:";
            Description = "Better penetration overall. Increases the pierce and attack by 25%";
            Complexity = 3;
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
            traitModifiers[0] *= 1.25f;
            traitModifiers[1] *= 1.25f; //25% increase
            return traitModifiers;
        }

    }
}


