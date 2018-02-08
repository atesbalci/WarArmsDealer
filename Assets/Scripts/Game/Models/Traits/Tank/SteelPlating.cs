using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class SteelPlating : Trait
    {
        public SteelPlating()
        {
            Name = "Steel Plating:";
            Description = "Increases armor by 25% but decreases attack by 10%";
            Complexity = 2;
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
            traitModifiers[0] *= 0.9f; //10% increase
            traitModifiers[2] *= 1.25f;


            return traitModifiers;
        }

    }
}


