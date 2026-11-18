Directory.CreateDirectory("../Fileok");
File.AppendAllText("../Fileok/regisztraciok.txt", "");

void exit()
{
    Environment.Exit(0);
}

bool startup_check(string ertek)
{
    if (ertek != "1" && ertek != "2" && ertek != "3")
    {
        return false;
    }
    return true;
}

void startup()
{
    Console.WriteLine("        ----Üdvözöljük!----");
    Console.WriteLine("Bejelentkezés (1) | Regisztrálás (2) | Kilépés (3)");
    string visszaadas = Console.ReadLine();

    while (!startup_check(visszaadas))
    {
        Console.WriteLine("Helytelen! Válaszd ki az egyik opciót ( Bejelentkezés (1) | Regisztrálás (2) | Kilépés (3) )");
        visszaadas = Console.ReadLine();
    }


    if (visszaadas == "2")
    {
        register();
    }

    if (visszaadas == "3")
    {
        exit();
    }

}

void register()
{
    while (true)
    {
        Console.WriteLine("Felhasználónév: ");
        string user = Console.ReadLine();
        Console.WriteLine("Jelszó: ");
        string password = Console.ReadLine();
        try
        {
            bool sikeres = true;
            Regisztracio reg = new Regisztracio(user, password);

            foreach (string sor in File.ReadLines("../Fileok/regisztraciok.txt"))
            {
                if (sor.Split(":")[0] == user)
                {
                    throw new ArgumentException("Már létezik ilyen nevű felhasználó!");
                }
            }

            JelszoKoder koder = new();
            File.AppendAllText("../Fileok/regisztraciok.txt", $"{reg.Nev}:{koder.Kodolas(reg.Jelszo)}\n");
            break;
        }
        catch (ArgumentException ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            break;
        }
    }
}

startup();


//string felh = Console.ReadLine();
//string jelsz = Console.ReadLine();

//JelszoKoder koder = new();
//foreach (string sor in File.ReadLines("../Fileok/regisztraciok.txt"))
//{
//    string[] x = sor.Trim().Split(":");
//    if (x[0] == felh)
//    {
//        if (koder.Ellenorzes(jelsz, x[1]))
//        {
//            Console.WriteLine("bent vagy");
//        }
//        else
//        {
//            Console.WriteLine("rossz jelsz");
//        }
//    }
//}


