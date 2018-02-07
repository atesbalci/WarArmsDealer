using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class LightChasis : Trait
    {
        public LightChasis()
        {
            Name = "Light Chassis:";
            Description = "If base health is below 10, it gives %5 increase per health below 10";
            Complexity = 3;
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
            traitModifiers[0] *= 1f+(10-traitModifiers[1])*0.05f; //10% increase



            return traitModifiers;
        }

    }
}


