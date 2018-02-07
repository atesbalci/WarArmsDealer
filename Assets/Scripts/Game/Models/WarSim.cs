using UnityEngine;
using System.Linq;
namespace Game.Models
{
    public class WarSim : ModelBase
    {

        public Nation[] _nations = new Nation[2];

        static public WarSim Instance;

        public WarSim(Nation east, Nation west)
        {
            _nations[0] = east;
            _nations[1] = west;
            Instance = this;
        }



        float[] eastModifier = new float[5];
        float[] westModifier = new float[5];

        public float[] CalculateDesign(Weapon wep)
        {

            float[] result = wep[wep.GetStatTypes()].Select(r => ((float) r.Value)).ToArray();
            
            float[] modifier = new float[3];
            foreach(var trait in wep.WeaponTraits)
            {
                var temp = trait.ApplyTrait(wep, new Nation(), new Nation());
                Debug.Log("Length is :" + temp.Length);
                modifier = modifier.Select((x, index) => x + temp[index]).ToArray();
            }
            switch (wep.Type)
            {
                case WeaponType.Infantry:
                    result[0] = modifier[0];
                    result[1] = modifier[1];
                    result[2] = modifier[2];
                    break;
                case WeaponType.Tank:
                    result[0] = modifier[0];
                    result[1] = modifier[1];
                    result[2] = modifier[2];
                    break;
                case WeaponType.Artillery:
                    result[0] = modifier[0];
                    result[1] = modifier[1];
                    result[2] = modifier[2];
                    break;
                default:
                    break;
            }
            return result;
        }

        void HandleTraits()
        {
            eastModifier = new float[5];
            westModifier = new float[5];
            for (int nat = 0; nat < _nations.Length; nat++)
            {
                for (int i = 0; i < _nations[nat].CurrentInfantry.WeaponTraits.Count; i++)
                {
                    var trait = _nations[nat].CurrentInfantry.WeaponTraits[i];
                    var modifier = trait.ApplyTrait(_nations[nat].CurrentInfantry, _nations[nat], _nations[(nat - 1) % 2]);

                    if (nat == 0)
                    {
                        eastModifier[0] += modifier[0];
                        eastModifier[1] += modifier[1];
                        eastModifier[2] += modifier[2];
                    }
                    else
                    {
                        westModifier[0] += modifier[0];
                        westModifier[1] += modifier[1];
                        westModifier[2] += modifier[2];
                    }
                }

                for (int i = 0; i < _nations[nat].CurrentTank.WeaponTraits.Count; i++)
                {
                    var trait = _nations[nat].CurrentTank.WeaponTraits[i];
                    var modifier = trait.ApplyTrait(_nations[nat].CurrentTank, _nations[nat], _nations[(nat + 1) % 2]);

                    if (nat == 0)
                    {
                        eastModifier[0] += modifier[0];
                        eastModifier[1] += modifier[1];
                        eastModifier[3] += modifier[2];
                    }
                    else
                    {
                        westModifier[0] += modifier[0];
                        westModifier[1] += modifier[1];
                        westModifier[3] += modifier[2];
                    }
                }

                for (int i = 0; i < _nations[nat].CurrentArtillery.WeaponTraits.Count; i++)
                {
                    var trait = _nations[nat].CurrentArtillery.WeaponTraits[i];
                    var modifier = trait.ApplyTrait(_nations[nat].CurrentArtillery, _nations[nat], _nations[(nat - 1) % 2]);

                    if (nat == 0)
                    {
                        eastModifier[0] += modifier[0];
                        eastModifier[1] += modifier[1];
                        eastModifier[4] += modifier[2];
                    }
                    else
                    {
                        westModifier[0] += modifier[0];
                        westModifier[1] += modifier[1];
                        westModifier[4] += modifier[2];
                    }
                }
            }
            

        }
        
        public void SimulateBattle(float combatWidth)
        {
            float healthEast = 0f;
            float attackEast = 0f;
            float healthWest = 0f;
            float attackWest = 0f;

            float startHealthEast = 0f, startHealthWest = 0f;

            HandleTraits();

            for (int i=0;i<3;i++)
            {

                healthEast += _nations[0].Weapons[i][StatType.Health].Value;
                healthWest += _nations[1].Weapons[i][StatType.Health].Value;
                attackEast += _nations[0].Weapons[i][StatType.Attack].Value;
                attackWest += _nations[1].Weapons[i][StatType.Attack].Value;

                startHealthEast = healthEast;
                startHealthWest = healthWest;
            }

            float leftArmorWest = Mathf.Max(0f, _nations[0].CurrentTank[StatType.Armor].Value + eastModifier[3] - _nations[1].CurrentArtillery[StatType.Piercing].Value);
            float leftArmorEast = Mathf.Max(0f, _nations[1].CurrentTank[StatType.Armor].Value + westModifier[3] - _nations[0].CurrentArtillery[StatType.Piercing].Value);

            float supportEast = _nations[0].CurrentInfantry[StatType.Support].Value;
            float supportWest = _nations[1].CurrentInfantry[StatType.Support].Value;

            attackEast += eastModifier[0];
            attackWest += westModifier[0];

            healthEast += eastModifier[1];
            healthWest += westModifier[1];

            supportEast += eastModifier[2];
            supportWest += westModifier[2];

            float supportRandEast = (attackEast + healthEast) / Mathf.Max((supportEast * 6f), 1);
            float supportRandWest = (attackWest + healthWest) / Mathf.Max((supportWest * 6f), 1);

            while (healthEast > 0f && healthWest > 0f)
            {
                healthEast -= Mathf.Max((attackWest - Random.Range(0f, supportRandEast * 2f) - leftArmorWest) * 0.1f, Random.Range(0f, supportRandEast * 0.1f));
                healthWest -= Mathf.Max((attackEast - Random.Range(0f, supportRandWest * 2f) - leftArmorEast) * 0.1f, Random.Range(0f, supportRandWest * 0.1f));
            }

            healthEast = Mathf.Max(0f, healthEast);
            healthWest = Mathf.Max(0f, healthWest);

            Debug.Log(_nations[0].Name + leftArmorEast);
            Debug.Log(_nations[1].Name + leftArmorWest);

            /*if(leftHealth>rightHealth)
            {
                Debug.Log("<color=red>"+ _nations[0].Name + "</color> has won");
            }
            else
            {
                Debug.Log("<color=red>" + _nations[1].Name + "</color> has won");
            }*/

            _nations[0].Manpower -= (1 - healthEast / startHealthEast) * combatWidth * 10;
            _nations[1].Manpower -= (1 - healthWest / startHealthWest) * combatWidth * 10;

            //Debug.Log(_nations[0].Name + " Remaining manpower: " + _nations[0].Manpower);
            //Debug.Log(_nations[1].Name + " Remaining manpower: " + _nations[1].Manpower);
        }
    }
}


