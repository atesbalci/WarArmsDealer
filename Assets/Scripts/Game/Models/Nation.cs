namespace Game.Models
{
    public class Nation : ModelBase
    {
        public string Name { get; set; }
        public float Manpower { get; set; }

        public InfantryWeapon CurrentInfantry;
        public TankWeapon CurrentTank;
        public ArtilleryWeapon CurrentArtillery;

        public Weapon[] Weapons;

        public Nation() {
            CurrentInfantry = new InfantryWeapon();
            CurrentTank = new TankWeapon();
            CurrentArtillery = new ArtilleryWeapon();

            Weapons = new Weapon[3];

            Weapons[0] = CurrentInfantry;
            Weapons[1] = CurrentTank;
            Weapons[2] = CurrentArtillery;

            Manpower = 10000;
        }

        public Nation(string name) {
            CurrentInfantry = new InfantryWeapon();
            CurrentTank = new TankWeapon();
            CurrentArtillery = new ArtilleryWeapon();

            Weapons = new Weapon[3];

            Weapons[0] = CurrentInfantry;
            Weapons[1] = CurrentTank;
            Weapons[2] = CurrentArtillery;

            Manpower = 10000;

            Name = name;
        }
    }
}
