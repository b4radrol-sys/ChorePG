
//---------------------------------------------------------------
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
    Console.Write("Bejelentkezés (1) | Regisztrálás (2) | Kilépés (3): ");
    string visszaadas = Console.ReadLine();


    while (!startup_check(visszaadas))
    {
        Console.Write("Helytelen! Válaszd ki az egyik opciót ( Bejelentkezés (1) | Regisztrálás (2) | Kilépés (3) ): ");
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
void kilepes_check_startup(string ertek)
{
    if (ertek == "0")
    {
        Console.Clear();
        startup();
    }
}
//---------------------------------------------------------------




//---------------------------------------------------------------
void register()
{
    Console.WriteLine("---Kilépés a menübe (0)---");


    Console.Write("Felhasználónév: ");
    string user = Console.ReadLine();
    kilepes_check_startup(user);
    user = user.ToUpper();


    Console.WriteLine();


    Console.Write("Jelszó: ");
    string password = Console.ReadLine();
    kilepes_check_startup(password);




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
    Console.WriteLine("---Kilépés a menübe (0)---");
    Console.Write("Felhasználónév: ");
    string felh = Console.ReadLine();
    kilepes_check_startup(felh);
    felh = felh.ToUpper();


    Console.Write("Jelszó: ");
    string jelsz = Console.ReadLine();
    kilepes_check_startup(jelsz);


    bool sikeres = false;


    if (felh == "0" || jelsz == "0")
    {
        Console.Clear();
        startup();
    }


    JelszoKoder koder = new();
    foreach (string sor in File.ReadLines("../Fileok/regisztraciok.txt"))
    {
        string[] x = sor.Trim().Split(":");
        if (x[0] == felh)
        {
            sikeres = true;


            if (koder.Ellenorzes(jelsz, x[1]))
            {
                Console.Clear();
                Console.WriteLine("     ---Sikeres---");
                main_menu(felh);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("---Rossz jelszó---");
                login();
            }
        }
    }
    if (!sikeres)
    {
        Console.Clear();
        Console.WriteLine("Nincs ilyen felhasználónevű fiók!");
        Console.WriteLine();
        login();
    }
}
//---------------------------------------------------------------




//---------------------------------------------------------------


bool main_valasztas_check(string ertek)
{
    if (ertek != "1" && ertek != "2" && ertek != "3" && ertek != "4" && ertek != "5" && ertek != "6")
    {
        return false;
    }
    return true;
}


void kilepes_check_main_menu(string ertek, string felhasznalo)
{
    if (ertek == "0")
    {
        Console.Clear();
        main_menu(felhasznalo);
    }
}
//---------------------------------------------------------------


void player_stat_kiiratas(string felhasznalo)
{
    Console.Clear();
    Console.WriteLine("--- Profil Statisztikak ---");
    Console.WriteLine();

    Player player = Player.Peldanyositas(felhasznalo);

    player.player_stat_kiiratas();

    Console.WriteLine();
    Console.WriteLine("Nyomj Entert a kilepeshez: ");
    Console.ReadLine();
    Console.Clear();
}


//---------------------------------------------------------------
void main_menu(string felhasznalo)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine($"--- Üdvözöllek, {felhasznalo} ---");
        Console.WriteLine();
        Console.WriteLine("1. Taskok megtekintése");
        Console.WriteLine("2. Új task hozzáadása");
        Console.WriteLine("3. Profil statok megnézése (XP, Level)");
        Console.WriteLine("4. Task teljesítése");
        Console.WriteLine("5. Task törlése");
        Console.WriteLine("6. Kijelentkezés");


        string valasztas = Console.ReadLine();


        while (!main_valasztas_check(valasztas))
        {
            Console.WriteLine("Helytelen! Válaszd ki az egyik opciót (1-6)");
            valasztas = Console.ReadLine();
        }


        if (valasztas == "1")
        {
            Console.Clear();
            Task.Listazas(Task.Peldanyositas(felhasznalo));
        }
        else if (valasztas == "2")
        {
            Console.Clear();
            task_felvetele(felhasznalo, Task.Peldanyositas(felhasznalo));
        }
        else if (valasztas == "3")
        {
            Console.Clear();
            player_stat_kiiratas(felhasznalo);
        }
        else if (valasztas == "4")
        {
            Console.Clear();
            task_teljesitese(felhasznalo, Task.Peldanyositas(felhasznalo));
        }
        else if (valasztas == "5")
        {
            Console.Clear();
            task_torlese(felhasznalo, Task.Peldanyositas(felhasznalo));
        }
        else if (valasztas == "6")
        {
            Console.Clear();
            Console.WriteLine("Sikeres kijelentkezes!");
            startup();
            break;
        }
    }
}
//---------------------------------------------------------------

void task_torlese(string felhasznalo, List<Task> taskok)
{

    Console.WriteLine("Kilépés a menübe (0)");
    Console.WriteLine();
    Task.Listazas(taskok);
    Console.WriteLine();
    Console.WriteLine("Melyik sorszamu taskot szeretnéd kitörölni? (1-9)");
    string valasztas = Console.ReadLine();


    kilepes_check_main_menu(valasztas, felhasznalo);


    int v;
    while (!int.TryParse(valasztas, out v) || v < 1 || v > 9)
    {
        Console.WriteLine("Helytelen, irj be egy szamot 1-9-ig:");
        valasztas = Console.ReadLine();
    }

    foreach (Task s in taskok)
    {
        if (s.id == v)
        {
            if (s.Nev == "-")
            {
                Console.Clear();
                Console.WriteLine("Ez már üres.");
                return;
            }

            s.Torles();

            Console.Clear();
            Console.WriteLine("Sikeres törlés!");
            Task.Mentes(felhasznalo, taskok);

            return;
        }
    }
}


//---------------------------------------------------------------

//---------------------------------------------------------------

bool felvetel_limit_check(string felhasznalo)
{
    int cnt = 0;
    DateOnly ma = DateOnly.FromDateTime(DateTime.Today);
    foreach (string s in File.ReadAllLines($"../Fileok/{felhasznalo}/player_task_archivum.csv"))
    {
        if (s.Trim().Split(";")[0] == ma.ToString())
        {
            cnt++;
        }
    }
    if (cnt >= 10)
    {
        return true;
    }
    return false;
}


//---------------------------------------------------------------
void task_felvetele(string felhasznalo, List<Task> taskok)
{

    bool vegigment = false;
    while (!vegigment)
    {

        Console.WriteLine("Kilépés a menübe(0)");
        Console.WriteLine();
        Console.WriteLine("Melyik task slotba szeretnéd felvenni a taskot (1-9)");
        string valasztas = Console.ReadLine();


        kilepes_check_main_menu(valasztas, felhasznalo);


        int v;
        while (!int.TryParse(valasztas, out v) || v < 1 || v > 9)
        {
            Console.WriteLine("Számot írj be 1-9-ig");
            valasztas = Console.ReadLine();
        }


        foreach (Task task in taskok)
        {
            if (task.id == v)
            {
                if (!task.Nev.Contains("-"))
                {
                    Console.Clear();
                    Console.WriteLine("A slot már foglalt. Válassz egy másikat");
                    break;
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine("Add meg a task nevét: ");
                        string task_nev = Console.ReadLine();
                        while (task_nev.Contains("-"))
                        {
                            Console.WriteLine("A task neve nem tartalmazhat '-'t (kötőjelet)");
                            task_nev = Console.ReadLine();
                        }


                        Console.Clear();
                        Console.WriteLine("Add meg a task nehézségét! (1-legkönnyebb | 9-legnehezebb)");
                        string task_nehezseg = Console.ReadLine();
                        int int_task_nehezseg;
                        while (!int.TryParse(task_nehezseg, out int_task_nehezseg) || int_task_nehezseg < 1 || int_task_nehezseg > 9)
                        {
                            Console.WriteLine("A nehézség csak 1 és 9 közötti szám lehet! (1-legkönnyebb | 9-legnehezebb)");
                            task_nehezseg = Console.ReadLine();
                        }
                        try
                        {
                            task.Felvetel(task_nev, int_task_nehezseg);
                            vegigment = true;
                            Task.Mentes(felhasznalo, taskok);
                            break;
                        }
                        catch (ArgumentException ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                            continue;
                        }
                    }
                }
            }
        }
    }
}
//---------------------------------------------------------------


//---------------------------------------------------------------
void task_teljesitese(string felhasznalo, List<Task> taskok)
{

    if (felvetel_limit_check(felhasznalo))
    {
        Console.WriteLine("Elérted a napi task limitet! Gyere vissza holnap");
        return;
    }

    Console.WriteLine("Kilépés a menübe (0)");
    Console.WriteLine();

    Task.Listazas(taskok);

    Console.WriteLine();
    Console.WriteLine("Melyik sorszamu taskot csinaltad meg? (1-9)");
    string valasztas = Console.ReadLine();


    kilepes_check_main_menu(valasztas, felhasznalo);


    int v;
    while (!int.TryParse(valasztas, out v) || v < 1 || v > 9)
    {
        Console.WriteLine("Helytelen, irj be egy szamot 1-9-ig:");
        valasztas = Console.ReadLine();
    }


    foreach (Task task in taskok)
    {
        if (task.id == v)
        {
            if (task.Nev == "-")
            {
                Console.Clear();
                Console.WriteLine("Ez üres");
                return;
            }

            string nev = task.Nev;
            int nehezseg = task.Nehezseg;
            int xp = task.xp;
            int gold = task.gold;

            Player p = Player.Peldanyositas(felhasznalo);
            task.Teljesites(p);
            p.Mentes(felhasznalo);
            Task.Mentes(felhasznalo, taskok);

            File.AppendAllText($"../Fileok/{felhasznalo}/player_task_archivum.csv", $"{DateOnly.FromDateTime(DateTime.Today)};{nev};{nehezseg}\n");

            Console.Clear();
            Console.WriteLine($"Gratulalok, hogy megcsinaltad!");
            Console.WriteLine($"Kaptál {xp} XP-t és {gold} Aranyat!");
            Console.WriteLine();
            Console.WriteLine("Nyomj Entert a kilepeshez: ");
            Console.ReadLine();
            Console.Clear();
            return;
        }
    }
}
//---------------------------------------------------------------
while (true)
{
    startup();
}
//---------------------------------------------------------------

