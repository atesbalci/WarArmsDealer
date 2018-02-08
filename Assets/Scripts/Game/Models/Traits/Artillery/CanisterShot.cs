using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class CanisterShot : Trait
    {

        public CanisterShot()
        {
            Name = "Canister Shot:";
            Description = "Increases the attack by 50% but decreases the piercing by 50%";
            Complexity = 1;
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
            traitModifiers[0] *= 1.5f;
            traitModifiers[2] *= 0.5f; //25% increase
            return traitModifiers;
        }

    }
}


