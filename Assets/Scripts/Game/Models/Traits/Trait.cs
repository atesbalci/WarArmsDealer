using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public abstract class Trait : ModelBase
    {
        
        public abstract float[] ApplyTrait(Weapon weapon, Nation weaponNation, Nation oppositeNation);
        protected float[] GetNationPowers(Nation nation)
        {
            float[] statValues = new float[5];
            for (int i = 0; i < 3; i++)
            {

                statValues[0] += nation.Weapons[i][StatType.Attack].Value;
                statValues[1] += nation.Weapons[i][StatType.Health].Value;

            }
            statValues[2] += nation.CurrentInfantry[StatType.Support].Value;
            statValues[3] += nation.CurrentTank[StatType.Armor].Value;
            statValues[4] += nation.CurrentArtillery[StatType.Piercing].Value;

            return statValues;
        }
    }
}


