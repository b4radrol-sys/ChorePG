Directory.CreateDirectory("../Fileok");
File.AppendAllText("../Fileok/regisztraciok.txt", "");

//---------------------------------------------------------------
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
//---------------------------------------------------------------


//---------------------------------------------------------------
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

    if (visszaadas == "1")
    {
        login();
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
//---------------------------------------------------------------


//---------------------------------------------------------------
void create_datafiles(string username)
{
    Directory.CreateDirectory($"../Fileok/{username}");
    File.Copy("template_player_stats.csv", $"../Fileok/{username}/player_stats.csv");
    File.Copy("template_tasks.csv", $"../Fileok/{username}/player_tasks.csv");
    File.Copy("template_task_archivum.csv", $"../Fileok/{username}/player_task_archivum.csv");
    File.Copy("template_player_inventory.csv", $"../Fileok/{username}/player_inventory.csv");
    File.Copy("template_legyozott_bossok.csv", $"../Fileok/{username}/legyozott_bossok.csv");
}
//---------------------------------------------------------------


//---------------------------------------------------------------
void register()
{
        Console.Write("Felhasználónév: ");
        string user = Console.ReadLine();
        Console.WriteLine();
        user = user.ToUpper();
        Console.Write("Jelszó: ");
        string password = Console.ReadLine();
        try
        {
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

            create_datafiles(user);
            Console.Clear();
        }
        catch (ArgumentException ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
        }
}
//---------------------------------------------------------------


//---------------------------------------------------------------
void login()
{
    Console.WriteLine("Felhasználónév: ");
    string felh = Console.ReadLine();
    felh = felh.ToUpper();
    Console.WriteLine("Jelszó:");
    string jelsz = Console.ReadLine();

    bool sikeres = false;

    JelszoKoder koder = new();
    foreach (string sor in File.ReadLines("../Fileok/regisztraciok.txt"))
    {
        string[] x = sor.Trim().Split(":");
        if (x[0] == felh)
        {
            sikeres = true;

            if (koder.Ellenorzes(jelsz, x[1]))
            {
                Console.WriteLine("siker");
                main_menu(felh);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Rossz jelszó");
            }
        }
    }
    if (!sikeres)
    {
        Console.WriteLine("Nincs ilyen felhasználónevű fiók");
    }
}
//---------------------------------------------------------------

while (true)
{
    startup();
}

//---------------------------------------------------------------
void main_menu(string felhasznalo)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine($"--- Üdvözöllek, {felhasznalo} ---");
        Console.WriteLine("1. Teendők megtekintése / Teljesítése");
        Console.WriteLine("2. Új teendő hozzáadása");
        Console.WriteLine("3. Profil (XP, Level)");
        Console.WriteLine("4. Kijelentkezés");

        string valasztas = Console.ReadLine();

        //if (valasztas == "1") { teendoklistazasa(); }
        //else if (valasztas == "2") { ujfeladathozzadas(); }
        //else if (valasztas == "3") { profilmegtekintes(); }
        //else if (valasztas == "4") { break; }
    }
}
//---------------------------------------------------------------
void teendoklistazasa()
{

}

//---------------------------------------------------------------
void xp_adding(int pluszXp)
{
    // szint += xp 
    //Console.WriteLine($"Gratulálok! Kaptál {xp} XP-t!");
}
//---------------------------------------------------------------


//---------------------------------------------------------------
