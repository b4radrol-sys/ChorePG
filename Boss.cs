using System.Data.SqlTypes;

public class Boss
{
    public int id {  get; set; }
    public string nev { get; set; }
    public int atk { get; set; }
    public int def { get; set; }
    public int szukseges_xp { get; set; }
    public int arany_jutalom { get; set; }
    public int xp_jutalom { get; set; }

    public Boss(int id, string nev,int atk, int def, int szukseges_xp, int arany_jutalom, int xp_jutalom)
    {
        this.id = id;
        this.nev = nev;
        this.atk = atk;
        this.def = def;
        this.szukseges_xp = szukseges_xp;
        this.arany_jutalom = arany_jutalom;
        this.xp_jutalom = xp_jutalom;
    }


    public int harciero_kiszamitas()
    {
        return atk + def;
    }
}
