using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Models
{
    public class WarSim : ModelBase
    {

        Nation[] _nations = new Nation[2];

        public WarSim(Nation east, Nation west)
        {
            _nations[0] = east;
            _nations[1] = west;
        }
        
        public void SimulateBattle()
        {
            float leftHealth = 0f;
            float leftAttack = 0f;
            float rightHealth = 0f;
            float rightAttack = 0f;


            for (int i=0;i<3;i++)
            {
                leftHealth += _nations[0].Weapons[i].HealthStat.Value;
                rightHealth += _nations[1].Weapons[i].HealthStat.Value;
                leftAttack += _nations[0].Weapons[i].HealthStat.Value;
                rightAttack += _nations[1].Weapons[i].HealthStat.Value;

            }

 


            float leftArmorWest=Mathf.Max(0f,_nations[0].CurrentTank.ArmorStat.Value - _nations[1].CurrentArtillery.PiercingStat.Value);
            float leftArmorEast = Mathf.Max(0f, _nations[1].CurrentTank.ArmorStat.Value - _nations[0].CurrentArtillery.PiercingStat.Value);
            while (leftHealth > 0f || rightHealth > 0f)
            {
                leftHealth -= rightAttack + leftArmorWest;
                rightHealth -= leftAttack + leftArmorEast;
            }
            Debug.Log("Battle is over");
            if(leftHealth>rightHealth)
            {
                Debug.Log("Left has won");
            }
            else
            {
                Debug.Log("Right has won");
            }

        }


    }
}


