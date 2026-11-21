public class Armor : Item
{
    public int defbonusz { get; set; }

    public Armor(int id, string nev, string ritkasag, int defbonusz) : base(id, nev, ritkasag)
    {
        this.defbonusz = defbonusz;
    }
}
