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
            if (value.Length > 12 || value.Length < 3)
            {
                throw new ArgumentException("A felhasználónév csak 3-12 karakter hosszűságő lehet.");
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
            if (value.Length < 8 || value.Length > 16)
            {
                throw new ArgumentException("A jelszó csak 8-16 karakter hosszűságő lehet.");
            }
            if (!value.Any(char.IsUpper) || !value.Any(char.IsDigit))
            {
                throw new ArgumentException("A jelszónak tartalmaznia kell számot és nagy betűt.");
            }
            jelszo = value;
        }
    }

}
