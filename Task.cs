class Feladat
{
    public string feladat_id
    private string feladat_nev
    private int feladat_nehezseg
    public int feladat_xp
    public int feladat_gold

    private Feladat(string nev, int xpErtek)
    {
        
        
    }

    public string nev
    {
        get { return feladat_nev; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A nevet nem szabad üresen hagyni!")
            }

            if (value.Contains(";"))
            {
                throw new ArgumentException("A név nem tartalmazhat ';' -t")
            }

        }
    }

    public string nehezseg
    {
        get { return feladat_nehezseg; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A nehézséget nem szabad üresen hagyni!")
            }

            if (value.Contains(";"))
            {
                throw new ArgumentException("A nehézség nem tartalmazhat ';' -t")
            }

        }
    }



}
