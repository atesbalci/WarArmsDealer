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
            foreach (var research in ActiveResearches)
            {
                research.Tick();
            }
        }

        public void AddResearch(Research research)
        {

        }
    }
}
