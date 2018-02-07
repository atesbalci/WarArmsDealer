using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class TungstenRounds : Trait
    {
        public TungstenRounds()
        {
            Name = "Tungsten Rounds:";
            Description = "Tungsten rounds are better for piercing armor. Increases piercing by 10%";
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
            traitModifiers[0] *= 1.1f; //10% increase



            return traitModifiers;
        }

    }
}


