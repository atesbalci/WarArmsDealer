using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class LightInfantry : Trait
    {

        public LightInfantry()
        {
            Name = "Light Infantry:";
            Description = "Increases the support of infantry by 50% but decreases health by 25%";
            Complexity = 1;
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
            traitModifiers[1] *= 0.75f;
            traitModifiers[0] *= 1.5f; 
            return traitModifiers;
        }

    }
}


