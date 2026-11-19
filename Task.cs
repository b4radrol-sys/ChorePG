class Task
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



}
