using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public abstract class Trait : ModelBase
    {
        
        public abstract float[] ApplyTrait(Nation weaponNation, Nation oppositeNation);

    }
}


