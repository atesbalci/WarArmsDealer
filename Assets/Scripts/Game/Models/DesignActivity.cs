using Utils;

namespace Game.Models
{
    public class DesignCompleteEvent : WEvent
    {
        public DesignActivity DesignActivity { get; set; }
    }

    public class DesignActivity : ModelBase
    {
        public int RemainingTicks { get; set; }
        public Weapon CreatedWeapon { get; private set; }

        public DesignActivity(Weapon design)
        {
            CreatedWeapon = design;
        }

        public override void Tick()
        {
            base.Tick();
            RemainingTicks--;
            if (RemainingTicks <= 0)
                MessageManager.Send(new DesignCompleteEvent { DesignActivity = this });
        }
    }
}
