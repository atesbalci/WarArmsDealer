using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models {
    public class Weapon : ModelBase {
        public HealthStat HealthStat { get; set; }
        public AttackStat AttackStat { get; set; }
    }
}
