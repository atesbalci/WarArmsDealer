using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class TungstenRounds : Trait
    {



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
            traitModifiers[(int)StatType.Attack] *= 1.1; //10% increase



            return traitModifiers;
        }

    }
}


