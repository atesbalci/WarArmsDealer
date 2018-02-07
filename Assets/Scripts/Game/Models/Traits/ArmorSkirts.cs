using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class ArmorSkirts : Trait
    {
        public ArmorSkirts()
        {
            Name = "Armor skirts:";
            Description = "Increases armor by 50% with %25 of chance each battle";
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
            if (Random.value >= 0.75f)
            {
                traitModifiers[2] *= 1.5f;

            }


            return traitModifiers;
        }

    }
}


