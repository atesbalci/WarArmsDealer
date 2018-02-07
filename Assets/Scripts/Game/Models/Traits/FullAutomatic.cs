using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class FullAutomatic : Trait
    {

        public FullAutomatic()
        {
            Name = "Full-Automatic Rifles:";
            Description = "Increases the attack of the infantry by 50%";
            Complexity = 3;
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

            traitModifiers[0] *= 1.5f;


            return traitModifiers;
        }

    }
}


