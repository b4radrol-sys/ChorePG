using System.Runtime.InteropServices;

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

void kilepes_check_startup(string ertek)
{
    if (ertek == "0")
    {
        Console.Clear();
        startup();
    }
}

//---------------------------------------------------------------
void register()
{
    Console.WriteLine("Kilépés a menübe (0)");

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
    Console.WriteLine("Kilépés a menübe (0)");
    Console.WriteLine("Felhasználónév: ");
    string felh = Console.ReadLine();
    kilepes_check_startup(felh);
    felh = felh.ToUpper();

    Console.WriteLine("Jelszó:");
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
                Console.WriteLine("siker");
                main_menu(felh);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Rossz jelszó");
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
    if (ertek != "1" && ertek != "2" && ertek != "3" && ertek != "4")
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

void main_menu(string felhasznalo)
{
    while (true)
    {
        Console.WriteLine($"--- Üdvözöllek, {felhasznalo} ---");
        Console.WriteLine("1. Taskok megtekintése");
        Console.WriteLine("2. Új task hozzáadása");
        Console.WriteLine("3. Profil statok megnézése (XP, Level)");
        Console.WriteLine("4. Kijelentkezés");

        string valasztas = Console.ReadLine();

        while (!main_valasztas_check(valasztas))
        {
            Console.WriteLine("Helytelen! Válaszd ki az egyik opciót ( 1. Taskok megtekintése | 2. Új teendő hozzáadása | 3. Profil (XP, Level) | 4. Kijelentkezés )");
            valasztas = Console.ReadLine();
        }

        if (valasztas == "1")
        {
            Console.Clear();
            task_listazas(task_peldanyositas(felhasznalo));
        }
        if (valasztas == "2")
        {
            Console.Clear();
            task_felvetele(felhasznalo, task_peldanyositas(felhasznalo));

        }

        //if (valasztas == "1") { teendoklistazasa(); }
        //else if (valasztas == "2") { ujfeladathozzadas(); }
        //else if (valasztas == "3") { profilmegtekintes(); }
        //else if (valasztas == "4") { break; }
    }
}

List<Task> task_peldanyositas(string felhasznalo)
{
    List<Task> taskok = new List<Task>();
    foreach (string sor in File.ReadLines($"../Fileok/{felhasznalo}/player_tasks.csv").Skip(1))
    {
        string[] sorok = sor.Trim().Split(";");
        taskok.Add(new Task(int.Parse(sorok[0]), sorok[1], int.Parse(sorok[2]), int.Parse(sorok[3]), int.Parse(sorok[4])));
    }

    return taskok;
}

int task_gold_szamitas(int nehezseg)
{
    int gold;
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

int task_xp_szamitas(int nehezseg)
{
    int xp;
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

void task_felvetele_write(string felhasznalo,List<Task> taskok)
{
    File.WriteAllText($"../Fileok/{felhasznalo}/player_tasks.csv", "id;nev;nehezseg;xp;gold\n");

    foreach (Task s in taskok)
    {
        File.AppendAllText($"../Fileok/{felhasznalo}/player_tasks.csv",
        $"{s.id};{s.Nev};{s.Nehezseg};{s.xp};{s.gold}\n");
    }

}

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

        foreach (Task s in taskok)
        {
            if (s.id == v)
            {
                if (!s.Nev.Contains("-"))
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
                            Console.WriteLine("A task neve nem tartalmazhat '-'t (vesszőt)");
                            task_nev = Console.ReadLine();
                        }

                        Console.Clear();
                        Console.WriteLine("Add meg a task nehézségét! (1-legkönnyebb | 9-legnehezebb)");
                        string task_nehezseg = Console.ReadLine();
                        int int_task_nehezseg;
                        while(!int.TryParse(task_nehezseg, out int_task_nehezseg))
                        {
                            Console.WriteLine("A nehézség csak 1 és 9 közötti szám lehet! (1-legkönnyebb | 9-legnehezebb)");
                            task_nehezseg = Console.ReadLine();
                        }
 // itt még le kell ellenőrizni hogy 1 és 9 között van e -------------------------------------------------------!!!!!!!!
                        try
                        {
                            s.Nev = task_nev;
                            s.Nehezseg = int_task_nehezseg;
                            s.gold = task_gold_szamitas(int_task_nehezseg);
                            s.xp = task_xp_szamitas(int_task_nehezseg);
                            vegigment = true;
                            task_felvetele_write(felhasznalo,taskok);
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
void task_listazas(List<Task> taskok)
{
    Console.WriteLine("Task sorszáma - Neve - Nehézségi fokozata - XP jutalom - Gold jutalom");
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
}

//---------------------------------------------------------------
void xp_adding(int pluszXp)
{
    // szint += xp 
    //Console.WriteLine($"Gratulálok! Kaptál {xp} XP-t!");
}
//---------------------------------------------------------------


//---------------------------------------------------------------
while (true)
{
    startup();
}
