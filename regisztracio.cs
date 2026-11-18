class Regisztracio
{
    private string nev;
    private string jelszo;


    public Regisztracio(string nev, string jelszo)
    {
        Nev = nev;
        Jelszo = jelszo;
    }

    public string Nev
    {
        get { return nev; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A felhasználónevet nem lehet üresen hagyni!");
            }
            nev = value;
        }
    }
    public string Jelszo
    {
        get { return jelszo; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A jelszót nem lehet üresen hagyni!");
            }

            jelszo = value;
        }
    }

}
