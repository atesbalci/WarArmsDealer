using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class RepairCorps : Trait
    {
        public RepairCorps()
        {
            Name = "Repair Corps:";
            Description = "Increases armor by 33%";
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

                traitModifiers[2] *= 1.33f;



            return traitModifiers;
        }

    }
}


