public class Task
{
    public int id;
    private string nev;
    private int nehezseg;
    public int xp;
    public int gold;

    public Task(int id, string nev, int nehezseg, int xp, int gold)
    {
        this.id = id;
        Nev = nev;
        Nehezseg = nehezseg;
        this.xp = xp;
        this.gold = gold;

    }

    public string Nev
    {
        get { return nev; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("A nevet nem szabad üresen hagyni!");
            }

            if (value.Contains(";"))
            {
                throw new ArgumentException("A név nem tartalmazhat ';'-t");
            }
            nev = value;
        }
    }

    public int Nehezseg
    {
        get { return nehezseg; }
        set
        {
            if (value > 9 || value < 1)
            {
                throw new ArgumentException("A nehézség csak 1 és 9 közötti szám lehet! (1-legkönnyebb | 9-legnehezebb)");
            }

            nehezseg = value;
        }

    }

    public static List<Task> Peldanyositas(string felhasznalo)
    {
        List<Task> taskok = new List<Task>();
        foreach (string sor in File.ReadLines($"../Fileok/{felhasznalo}/player_tasks.csv").Skip(1))
        {
            string[] sorok = sor.Trim().Split(";");
            taskok.Add(new Task(int.Parse(sorok[0]), sorok[1], int.Parse(sorok[2]), int.Parse(sorok[3]), int.Parse(sorok[4])));
        }


        return taskok;
    }
    public static void Listazas(List<Task> taskok)
    {
        Console.WriteLine(" =========================================================================");
        Console.WriteLine("---Task sorszáma - Neve - Nehézségi fokozata - XP jutalom - Gold jutalom---");
        Console.WriteLine(" =========================================================================");
        foreach (Task s in taskok)
        {
            if (s.Nev == "-")
            {
                Console.WriteLine($"{s.id} - üres task slot");
            }
            else
            {
                Console.WriteLine($"{s.id} - {s.Nev} - {s.Nehezseg} - {s.xp} - {s.gold}");
            }
        }
        Console.WriteLine(" =========================================================================");
    }

    public static void Mentes(string felhasznalo, List<Task> taskok)
    {
        File.WriteAllText($"../Fileok/{felhasznalo}/player_tasks.csv", "id;nev;nehezseg;xp;gold\n");
        foreach (Task s in taskok)
        {
            File.AppendAllText($"../Fileok/{felhasznalo}/player_tasks.csv",
            $"{s.id};{s.Nev};{s.Nehezseg};{s.xp};{s.gold}\n");
        }
    }

    public void Felvetel(string nev, int nehezseg)
    {
        Nev = nev;
        Nehezseg = nehezseg;
        gold = task_gold_szamitas(nehezseg);
        xp = task_xp_szamitas(nehezseg);
    }

    public void Torles()
    {
        Nev = "-";
        Nehezseg = 1;
        xp = 0;
        gold = 0;
    }
    
    public void Teljesites(Player p)
    {
        p.xp_gold_adding(this);
        Torles();
    }

    public static int task_gold_szamitas(int nehezseg)
    {
        if (nehezseg >= 1 && nehezseg <= 3)
        {
            return Random.Shared.Next(1, 6);
        }
        else if (nehezseg >= 4 && nehezseg <= 6)
        {
            return Random.Shared.Next(6, 13);
        }
        else
        {
            return Random.Shared.Next(20, 31);
        }
    }

    public static int task_xp_szamitas(int nehezseg)
    {
        if (nehezseg >= 1 && nehezseg <= 3)
        {
            return Random.Shared.Next(5, 11);
        }
        else if (nehezseg >= 4 && nehezseg <= 6)
        {
            return Random.Shared.Next(15, 26);
        }
        else
        {
            return Random.Shared.Next(35, 51);
        }
    }
}


