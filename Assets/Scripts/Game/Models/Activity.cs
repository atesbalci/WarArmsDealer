namespace Game.Models
{
    public abstract class Activity : ModelBase
    {
        public bool Cancelled { get; set; }

        public int RemainingDuration { get; private set; }
        public int TotalDuration { get; private set; }
        public float RefundAmount { get; set; }

        protected Activity(int duration, float refund)
        {
            RemainingDuration = duration;
            TotalDuration = duration;
            Cancelled = false;
            RefundAmount = refund;
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
