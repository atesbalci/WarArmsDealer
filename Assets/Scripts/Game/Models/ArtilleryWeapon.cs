using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Game.Models {
    public class ArtilleryWeapon : Weapon {
        public PiercingStat PiercingStat { get; set; }

        public ArtilleryWeapon() {
            this.PiercingStat = new PiercingStat();
        }
    }
}