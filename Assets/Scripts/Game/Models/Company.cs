using System.Collections.Generic;

namespace Game.Models
{
    public class Company : ModelBase
    {
        public List<ResearchActivity> ActiveResearches { get; private set; }
        public List<DesignActivity> ActiveDesigns { get; private set; }
        public List<Weapon> DesignedWeapons;


        public Company()
        {
            ActiveResearches = new List<ResearchActivity>();
            ActiveDesigns = new List<DesignActivity>();
            DesignedWeapons = new List<Weapon>();
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
        public void AddDesign(Weapon design)
        {
            ActiveDesigns.Add(new DesignActivity(design));
        }
    }
}
