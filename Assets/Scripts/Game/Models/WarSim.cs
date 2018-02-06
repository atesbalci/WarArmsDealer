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

                leftHealth += _nations[0].Weapons[i].Stats[(int)StatType.Health].Value;
                rightHealth += _nations[1].Weapons[i].Stats[(int)StatType.Health].Value;
                leftAttack += _nations[0].Weapons[i].Stats[(int)StatType.Attack].Value;
                rightAttack += _nations[1].Weapons[i].Stats[(int)StatType.Attack].Value;

                leftStartHealth = leftHealth;
                rightStartHealth = rightHealth;
            }

            float leftArmorWest = Mathf.Max(0f, _nations[0].CurrentTank.Stats[(int)StatType.Armor].Value - _nations[1].CurrentArtillery.Stats[(int)StatType.Piercing].Value);
            float leftArmorEast = Mathf.Max(0f, _nations[1].CurrentTank.Stats[(int)StatType.Armor].Value - _nations[0].CurrentArtillery.Stats[(int)StatType.Piercing].Value);

            while (leftHealth > 0f && rightHealth > 0f)
            {
                leftHealth -= (rightAttack + Random.Range(0f, 5f) - leftArmorWest) * 0.1f;
                rightHealth -= (leftAttack + Random.Range(0f, 5f) - leftArmorEast) * 0.1f;
            }

            leftHealth = Mathf.Max(0f, leftHealth);
            rightHealth = Mathf.Max(0f, rightHealth);


            /*if(leftHealth>rightHealth)
            {
                Debug.Log("<color=red>"+ _nations[0].Name + "</color> has won");
            }
            else
            {
                Debug.Log("<color=red>" + _nations[1].Name + "</color> has won");
            }*/

            _nations[0].Manpower -= (1 - leftHealth / leftStartHealth) * combatWidth * 10;
            _nations[1].Manpower -= (1 - rightHealth / rightStartHealth) * combatWidth * 10;

            //Debug.Log(_nations[0].Name + " Remaining manpower: " + _nations[0].Manpower);
            //Debug.Log(_nations[1].Name + " Remaining manpower: " + _nations[1].Manpower);
        }
    }
}


