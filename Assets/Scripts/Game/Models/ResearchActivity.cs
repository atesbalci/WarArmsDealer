using Utils;

namespace Game.Models
{
    public class ResearchCompleteEvent : WEvent
    {
        public ResearchActivity ResearchActivity { get; set; }
    }

    public class ResearchActivity : ModelBase
    {
        public int RemainingTicks { get; set; }
        public Research Research { get; private set; }

        public ResearchActivity(Research research)
        {
            Research = research;
        }

        public override void Tick()
        {
            base.Tick();
            RemainingTicks--;
            MessageManager.Send(new ResearchCompleteEvent { ResearchActivity = this });
        }
    }
}
