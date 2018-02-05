namespace Game.Models
{
    public class Research : ModelBase
    {
        public Weapon Weapon { get; set; }

        public Research(Weapon weapon) {
            this.Weapon = weapon;
        }

        //TODO Anyone: Implement this (please) (I couldn't decide)
        public int CalculateDuration()
        {
            return 0;
        }
    }
}
