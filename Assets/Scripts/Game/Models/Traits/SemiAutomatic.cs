using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class SemiAutomatic : Trait
    {

        public SemiAutomatic()
        {
            Name = "Semi-automatic Rifles:";
            Description = "Increases the attack of the infantry by 25%";
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

            traitModifiers[0] *= 1.25f;


            return traitModifiers;
        }

    }
}


