using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class CastEngine : Trait
    {
        public CastEngine()
        {
            Name = "Cast Engine Manifold:";
            Description = " Increases attack by the 10% of the health";
            Complexity = 1;
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
            traitModifiers[0] += traitModifiers[1] * 0.1f; //10% increase



            return traitModifiers;
        }

    }
}


