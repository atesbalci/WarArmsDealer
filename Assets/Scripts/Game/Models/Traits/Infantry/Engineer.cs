using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class Engineer : Trait
    {
        
        public Engineer()
        {
            Name = "Engineers:";
            Description = "Increases the support of the weapon by 25%";
            Complexity = 2;
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
            traitModifiers[2] *= 1.25f; //25% increase
            return traitModifiers;
        }

    }
}


