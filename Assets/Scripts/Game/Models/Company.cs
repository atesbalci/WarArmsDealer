using UnityEngine;
using System.Collections.Generic;

namespace Game.Models
{
    public class Company : ModelBase
    {
        public List<ResearchActivity> ActiveResearches { get; private set; }
        public List<DesignActivity> ActiveDesigns { get; private set; }
        public List<Weapon> DesignedWeapons;
        public Technology Tech { get; set; }
        public Design CompanyDesigns;
        public string Name;
        public float Money;


        public Company()
        {
            CompanyDesigns = new Design(this);
            ActiveResearches = new List<ResearchActivity>();
            ActiveDesigns = new List<DesignActivity>();
            DesignedWeapons = new List<Weapon>();
            Tech = new Technology();
            Money = 1000f;
        }

        public Company(string p_Name) {
            CompanyDesigns = new Design(this);
            ActiveResearches = new List<ResearchActivity>();
            ActiveDesigns = new List<DesignActivity>();
            DesignedWeapons = new List<Weapon>();
            Tech = new Technology();
            Money = 1000f;


            Name = p_Name;
        }

        public override void Tick()
        {
            base.Tick();
            for (var i = 0; i < ActiveResearches.Count; i++)
            {
                var research = ActiveResearches[i];
                research.Tick();
                if (research.RemainingTicks <= 0)
                {
                    ActiveResearches.RemoveAt(i);
                    i--;
                }
            }
            for(var i = 0; i < ActiveDesigns.Count; i++)
            {
                
                var design = ActiveDesigns[i];
                design.Tick();
                Debug.Log("Designing: " + design.RemainingTicks);
                if (design.RemainingTicks <= 0)
                {
                    ActiveDesigns.RemoveAt(i);
                    i--;
                }
            }
        }


        public void AddResearch(Research research)
        {
            ActiveResearches.Add(new ResearchActivity(research));
        }
        [System.Obsolete("This is an obsolete method")]
        public void AddDesign(Weapon design)
        {
            ActiveDesigns.Add(new DesignActivity(design, CompanyDesigns.BaseDesignTime));
        }
    }
}
