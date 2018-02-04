using System.Collections.Generic;

namespace Game.Models
{
    public class Company : ModelBase
    {
        public List<ResearchActivity> ActiveResearches { get; private set; }

        public Company()
        {
            ActiveResearches = new List<ResearchActivity>();
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
        }

        public void AddResearch(Research research)
        {
            ActiveResearches.Add(new ResearchActivity(research));
        }
    }
}
