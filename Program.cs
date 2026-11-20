using System.Runtime.InteropServices;


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




//---------------------------------------------------------------
void main_menu(string felhasznalo)
{
    while (true)
    {
        Console.WriteLine($"--- Üdvözöllek, {felhasznalo} ---");
        Console.WriteLine("1. Taskok megtekintése");
        Console.WriteLine("2. Új task hozzáadása");
        Console.WriteLine("3. Profil statok megnézése (XP, Level)");
        Console.WriteLine("4. Task teljesítése");
        Console.WriteLine("5. Kijelentkezés");


        string valasztas = Console.ReadLine();


        while (!main_valasztas_check(valasztas))
        {
            Console.WriteLine("Helytelen! Válaszd ki az egyik opciót (1-5)");
            valasztas = Console.ReadLine();
        }


        if (valasztas == "1")
        {
            Console.Clear();
            task_listazas(task_peldanyositas(felhasznalo));
        }
        else if (valasztas == "2")
        {
            Console.Clear();
            task_felvetele(felhasznalo, task_peldanyositas(felhasznalo));
        }
        else if (valasztas == "3")
        {
            Console.Clear();
            Console.WriteLine("--- Profil Statisztikak ---");
            Console.WriteLine();


            foreach (string sor in File.ReadLines($"../Fileok/{felhasznalo}/player_stats.csv"))
            {
                Console.WriteLine(sor);
            }


            Console.WriteLine();
            Console.WriteLine("Nyomj Entert a kilepeshez: ");
            Console.ReadLine();
            Console.Clear();
        }
        else if (valasztas == "4")
        {
            Console.Clear();
            task_teljesitese(felhasznalo, task_peldanyositas(felhasznalo));
        }
        else if (valasztas == "5")
        {
            Console.Clear();
            Console.WriteLine("Sikeres kijelentkezes!");
            startup();
            break;
        }
    }
}
//---------------------------------------------------------------




//---------------------------------------------------------------
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
//---------------------------------------------------------------




//---------------------------------------------------------------
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
//---------------------------------------------------------------




//---------------------------------------------------------------
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
//---------------------------------------------------------------




//---------------------------------------------------------------
void task_felvetele_write(string felhasznalo, List<Task> taskok)
{
    File.WriteAllText($"../Fileok/{felhasznalo}/player_tasks.csv", "id;nev;nehezseg;xp;gold\n");


    foreach (Task s in taskok)
    {
        File.AppendAllText($"../Fileok/{felhasznalo}/player_tasks.csv",
        $"{s.id};{s.Nev};{s.Nehezseg};{s.xp};{s.gold}\n");
    }


}
//---------------------------------------------------------------




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
                        while (!int.TryParse(task_nehezseg, out int_task_nehezseg) || int_task_nehezseg < 1 || int_task_nehezseg > 9)
                        {
                            Console.WriteLine("A nehézség csak 1 és 9 közötti szám lehet! (1-legkönnyebb | 9-legnehezebb)");
                            task_nehezseg = Console.ReadLine();
                        }
                        try
                        {
                            s.Nev = task_nev;
                            s.Nehezseg = int_task_nehezseg;
                            s.gold = task_gold_szamitas(int_task_nehezseg);
                            s.xp = task_xp_szamitas(int_task_nehezseg);
                            vegigment = true;
                            task_felvetele_write(felhasznalo, taskok);
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




//---------------------------------------------------------------
void xp_gold_adding(string felhasznalo, int pluszXp, int pluszGold)
{
    string statPath = $"../Fileok/{felhasznalo}/player_stats.csv";
    string[] sorok = File.ReadAllLines(statPath);


    if (sorok.Length > 1)
    {
        string[] adatok = sorok[1].Split(';');


        // xp;gold;felvett_fegyver;felvett_pancel;megcsinalt_taskok


        int jelenlegixp = int.Parse(adatok[0]) + pluszXp;
        int jelenlegigold = int.Parse(adatok[1]) + pluszGold;
        int jelenlegitaskok = int.Parse(adatok[4]) + 1;


        adatok[0] = jelenlegixp.ToString();
        adatok[1] = jelenlegigold.ToString();
        adatok[4] = jelenlegitaskok.ToString();


        string osszerakottSor = "";
        for (int i = 0; i < adatok.Length; i++)
        {
            osszerakottSor += adatok[i];

            if (i < adatok.Length - 1)
            {
                osszerakottSor += ";";
            }
        }


        sorok[1] = osszerakottSor;
        File.WriteAllLines(statPath, sorok);
    }
}
//---------------------------------------------------------------




//---------------------------------------------------------------
void task_teljesitese(string felhasznalo, List<Task> taskok)
{
    Console.WriteLine("Kilépés a menübe (0)");
    Console.WriteLine();
    task_listazas(taskok);
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


    foreach (Task s in taskok)
    {
        if (s.id == v)
        {
            if (s.Nev == "-")
            {
                Console.Clear();
                Console.WriteLine("Ez üres");
                return;
            }



            xp_gold_adding(felhasznalo, s.xp, s.gold);


            s.Nev = "-";
            s.Nehezseg = 0;
            s.xp = 0;
            s.gold = 0;


            task_felvetele_write(felhasznalo, taskok);


            Console.Clear();
            Console.WriteLine($"Gratulalok, hogy megcsinaltad!");
            Console.WriteLine($"Kaptál {s.xp} XP-t és {s.gold} Aranyat!");
            Console.WriteLine();
            Console.WriteLine("Nyomj Entert a kilepeshez: ");
            Console.ReadLine();
            Console.Clear();
            return;
        }
    }
}




//---------------------------------------------------------------




//---------------------------------------------------------------
while (true)
{
    startup();
}
//---------------------------------------------------------------

