using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class BalisticCalculator : Trait
    {

        public BalisticCalculator()
        {
            Name = "Ballistic Calculator:";
            Description = "Increases piercing by 100% with %50 chance";
            Complexity = 4;
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
            if(Random.value >= 0.5f)
                traitModifiers[2] *= 1.75f; //25% increase
            return traitModifiers;
        }

    }
}


