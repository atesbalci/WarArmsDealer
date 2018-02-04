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
        
        public void SimulateBattle(float combatWidth)
        {
            float leftHealth = 0f;
            float leftAttack = 0f;
            float rightHealth = 0f;
            float rightAttack = 0f;

            float leftStartHealth = 0f, rightStartHealth = 0f; 

            for (int i=0;i<3;i++)
            {

                leftHealth += _nations[0].Weapons[i].HealthStat.Value;
                rightHealth += _nations[1].Weapons[i].HealthStat.Value;
                leftAttack += _nations[0].Weapons[i].AttackStat.Value;
                rightAttack += _nations[1].Weapons[i].AttackStat.Value;

                leftStartHealth = leftHealth;
                rightStartHealth = rightHealth;
            }

            float leftArmorWest = Mathf.Max(0f,_nations[0].CurrentTank.ArmorStat.Value - _nations[1].CurrentArtillery.PiercingStat.Value);
            float leftArmorEast = Mathf.Max(0f, _nations[1].CurrentTank.ArmorStat.Value - _nations[0].CurrentArtillery.PiercingStat.Value);

            while (leftHealth > 0f && rightHealth > 0f)
            {
                leftHealth -= (rightAttack + Random.Range(0f, 2f) - leftArmorWest) * 0.1f;
                rightHealth -= (leftAttack + Random.Range(0f, 2f) - leftArmorEast) * 0.1f;
                
            }

            leftHealth = Mathf.Max(0f, leftHealth);
            rightHealth = Mathf.Max(0f, rightHealth);

            Debug.Log("Battle is over");

            if(leftHealth>rightHealth)
            {
                Debug.Log("<color=red>"+ _nations[0].Name + "</color> has won");
               
            }
            else
            {
                Debug.Log("<color=red>" + _nations[1].Name + "</color> has won");
            }
            _nations[0].Manpower -= (1 - leftHealth / leftStartHealth) * combatWidth;
            _nations[1].Manpower -= (1 - rightHealth / rightStartHealth) * combatWidth;
            Debug.Log(_nations[0].Name + " Remaining manpower: " + _nations[0].Manpower);
            Debug.Log(_nations[1].Name + " Remaining manpower: " + _nations[1].Manpower);
        }
    }
}


