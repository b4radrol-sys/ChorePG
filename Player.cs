using System.Xml.XPath;

public class Player
{
    public int xp { get; set; }
    public int gold { get; set; }
    public int felvett_fegyver { get; set; }
    public int felvett_pancel { get; set; }
    public int megcsinalt_taskok { get; set; }

    public Player(int xp,int gold, int felvett_fegyver, int felvett_pancel, int megcsinalt_taskok)
    {
        this.xp = xp;
        this.gold = gold;
        this.felvett_fegyver = felvett_fegyver;
        this.felvett_pancel = felvett_pancel;
        this.megcsinalt_taskok = megcsinalt_taskok;
    }

    public static Player Peldanyositas(string felhasznalo)
    {
        string[] sorok = File.ReadAllLines($"../Fileok/{felhasznalo}/player_stats.csv");
        string[] adatsor = sorok[1].Trim().Split(';');

        return new Player(int.Parse(adatsor[0]), int.Parse(adatsor[1]), int.Parse(adatsor[2]),int.Parse(adatsor[3]), int.Parse(adatsor[4]));
    }

    public void Mentes(string felhasznalo)
    {
        string[] sorok = File.ReadAllLines($"../Fileok/{felhasznalo}/player_stats.csv");
        sorok[1] = $"{xp};{gold};{felvett_fegyver};{felvett_pancel};{megcsinalt_taskok}";

        File.WriteAllText($"../Fileok/{felhasznalo}/player_stats.csv", sorok[0]+"\n");
        File.AppendAllText($"../Fileok/{felhasznalo}/player_stats.csv", sorok[1]);
    }

    public void xp_gold_adding(Task t)
    {
        xp += t.xp;
        gold += t.gold;
        megcsinalt_taskok++;
    }

    public string fegyver_kiszamitas(string path)
    {
        foreach (string x in File.ReadLines(path).Skip(1))
        {
            string[] adatok = x.Trim().Split(";");
            if (int.Parse(adatok[0]) == felvett_fegyver)
            {
                return adatok[1];
            }
        }
        return "Nincs felvéve fegyver!";
    }

    public string armor_kiszamitas(string path)
    {
        foreach (string x in File.ReadLines(path).Skip(1))
        {
            string[] adatok = x.Trim().Split(";");
            if (int.Parse(adatok[0]) == felvett_pancel)
            {
                return adatok[1];
            }
        }
        return "Nincs felvéve páncél!";
    }
    public int felvett_fegyver_atk(string path)
    {
        foreach (string x in File.ReadLines(path).Skip(1))
        {
            string[] adatok = x.Trim().Split(";");
            if (int.Parse(adatok[0]) == felvett_fegyver)
            {
                return int.Parse(adatok[3]);
            }
        }
        return 0;
    }

    public int felvett_armor_def(string path)
    {
        foreach (string x in File.ReadLines(path).Skip(1))
        {
            string[] adatok = x.Trim().Split(";");
            if (int.Parse(adatok[0]) == felvett_pancel)
            {
                return int.Parse(adatok[3]);
            }
        }
        return 0;
    }

    public int szint_kiszamitas()
    {
        return Math.Min(30, xp / 100);
    }

    public int harciero_kiszamitas()
    {
        return 12 + szint_kiszamitas() + 2 * (felvett_fegyver_atk("weapons.csv") + felvett_armor_def("armor.csv"));
    }
    public void player_stat_kiiratas()
    {
        Console.WriteLine($"xp: {xp} ({szint_kiszamitas()}-as szint)");
        Console.WriteLine($"gold: {gold}");
        Console.WriteLine($"Felvett fegyver: {fegyver_kiszamitas("weapons.csv")}");
        Console.WriteLine($"Felvett páncél: {armor_kiszamitas("armor.csv")}");
        Console.WriteLine($"Megcsinált taskok: {megcsinalt_taskok}");
    }
}