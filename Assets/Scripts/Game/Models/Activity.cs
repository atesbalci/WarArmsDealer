namespace Game.Models
{
    public abstract class Activity : ModelBase
    {
        public int RemainingDuration { get; private set; }
        public int TotalDuration { get; private set; }

        protected Activity(int duration)
        {
            RemainingDuration = duration;
            TotalDuration = duration;
        }

        public override void Tick()
        {
            base.Tick();
            RemainingDuration--;
            if (RemainingDuration <= 0)
            {
                ActivityComplete();
            }
        }

        protected abstract void ActivityComplete();
    }
}
