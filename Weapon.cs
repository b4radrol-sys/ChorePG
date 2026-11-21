public class Weapon : Item
{
    public int atkbonusz {  get; set; }
    
    public Weapon(int id, string nev, string ritkasag, int atkbonusz) : base(id, nev, ritkasag)
    {
        this.atkbonusz = atkbonusz;
    }
}
