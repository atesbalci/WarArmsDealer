using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class SelfPropelled : Trait
    {

        public SelfPropelled()
        {
            Name = "Self Propelled Artillery:";
            Description = "Increases the maneuverability of the artillery, increases health by 50% but decreases attack by %10";
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
            traitModifiers[0] *= 0.9f;
            traitModifiers[1] *= 1.5f; //25% increase
            return traitModifiers;
        }

    }
}


