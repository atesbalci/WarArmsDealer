using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class RainnproofGear : Trait
    {

        public RainnproofGear()
        {
            Name = "Rainproof Gear:";
            Description = "Increases all stats of the infantry by 25% with 50% chance";
            Complexity = 2;
        }

        public override float[] ApplyTrait(Weapon weapon, Nation weaponNation, Nation oppositeNation)
        {

            Debug.Assert(weapon.Type == WeaponType.Infantry);
            float[] traitModifiers = new float[3];
            int count = 0;
            foreach (var val in weapon[weapon.GetStatTypes()])
            {
                traitModifiers[count] = val.Value;
                count++;
            }
            if(Random.value >= 0.5f)
            {
                traitModifiers[2] *= 1.25f;
                traitModifiers[1] *= 1.25f;
                traitModifiers[0] *= 1.25f;
            }
            
            return traitModifiers;
        }

    }
}


