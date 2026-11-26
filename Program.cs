
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

List<Item> Items = new List<Item>();

foreach (string sor in File.ReadLines("weapons.csv").Skip(1))
{
    string[] adatok = sor.Trim().Split(";");
    Items.Add(new Weapon(int.Parse(adatok[0]), adatok[1], adatok[2], int.Parse(adatok[3])));
}
foreach (string sor in File.ReadLines("armor.csv").Skip(1))
{
    string[] adatok = sor.Trim().Split(";");
    Items.Add(new Armor(int.Parse(adatok[0]), adatok[1], adatok[2], int.Parse(adatok[3])));
}

List<Boss> Bosses = new List<Boss>();

foreach (string sor in File.ReadLines("bosses.csv").Skip(1))
{
    string[] adatok = sor.Trim().Split(";");
    Bosses.Add(new Boss(int.Parse(adatok[0]), adatok[1] , int.Parse(adatok[2]), int.Parse(adatok[3]), int.Parse(adatok[4]), int.Parse(adatok[5]), int.Parse(adatok[6])));
}

//---------------------------------------------------------------
void startup()
{
    Console.WriteLine("        ----Üdvözöljük!----");
    Console.Write("Bejelentkezés (1) | Regisztrálás (2) | Kilépés (3): ");
    string visszaadas = Console.ReadLine();
    Console.WriteLine();


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
                throw new ArgumentException("---Már létezik ilyen nevű felhasználó!---");
                Console.WriteLine();
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
    if (ertek != "1" && ertek != "2" && ertek != "3" && ertek != "4" && ertek != "5" && ertek != "6" && ertek != "7" && ertek != "8" && ertek != "9" && ertek != "10")
    {
        return false;
    }
    return true;
}


bool kilepes_check_main_menu(string ertek, string felhasznalo)
{
    if (ertek == "0")
    {
        Console.Clear();
        return true;
    }
    return false;
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
    Console.Write("Nyomj Entert a kilepeshez: ");
    Console.ReadLine();
    Console.Clear();
}
//---------------------------------------------------------------

void inventory_kezelese(string felhasznalo, List<Item> items)
{
    Console.Clear();
    Console.WriteLine("---Kilépés a menübe (0)---");
    Console.WriteLine();
    Console.WriteLine("Az inventoryd: ");

    foreach (string s in File.ReadLines($"../Fileok/{felhasznalo}/player_inventory.csv").Skip(1))
    {
        int item_id = int.Parse(s.Trim());

        foreach (Item item in items)
        {
            if (item_id == item.id)
            {
                if (item is Weapon weapon)
                {
                    Console.WriteLine($"{weapon.nev} ({weapon.ritkasag}) - Sebzés bónusz: {weapon.atkbonusz}");
                }

                if (item is Armor armor)
                {
                    Console.WriteLine($"{armor.nev} ({armor.ritkasag}) - Védelem bónusz: {armor.defbonusz}");
                }
            }
        }
    }

    Console.WriteLine();
    Console.Write("Fegyver felvétele (1) | Páncél felvétele (2) | Fegyver levétele (3) | Páncél levétele (4) |: ");
    string valasztas = Console.ReadLine();
    if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; };
    while (valasztas != "1" && valasztas != "2" && valasztas != "3" && valasztas != "4")
    {
        Console.Write("Az egyik opciót válaszd ki! Fegyver felvétele (1) | Páncél felvétele (2) | Fegyver levétele (3) | Páncél levétele (4) | Kilépés a menübe (0): ");
        valasztas = Console.ReadLine();
        if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; };
    }

    if (valasztas == "1")
    {
        Console.Clear();
        Console.WriteLine("---Kilépés a menübe (0)---");
        Console.WriteLine();

        List<string> fegyvereid = new List<string>();
        foreach (string weaponid in File.ReadLines($"../Fileok/{felhasznalo}/player_inventory.csv").Skip(1))
        {
            int itemid_weapon = int.Parse(weaponid.Trim());

            foreach (Item item in Items)
            {
                if (item is Weapon weapon && itemid_weapon == item.id)
                {
                    fegyvereid.Add(weapon.id.ToString());
                    Console.WriteLine($"{weapon.id} - {weapon.nev} ({weapon.ritkasag}) - Sebzés bónusz: {weapon.atkbonusz}");
                }
            }
        }
        Console.WriteLine();

        Console.Write("Melyik sorszámú fegyvert szeretnéd felvenni?: ");
        string valasztas_weapon = Console.ReadLine();
        if (kilepes_check_main_menu(valasztas_weapon, felhasznalo)) { return; }

        while (!fegyvereid.Contains(valasztas_weapon))
        {
            Console.Write("Nincs ilyen sorszámú fegyvered!: ");
            valasztas_weapon = Console.ReadLine();
            if (kilepes_check_main_menu(valasztas_weapon, felhasznalo)) { return; }
        }

        int valasztas_weapon_int = int.Parse(valasztas_weapon);

        Player p = Player.Peldanyositas(felhasznalo);
        p.felvett_fegyver = valasztas_weapon_int;
        p.Mentes(felhasznalo);

        Console.Clear();
        Console.WriteLine("Sikeresen felvetted!");
    }

    if (valasztas == "2")
    {
        Console.Clear();
        Console.WriteLine("Kilépés a menübe (0)");
        Console.WriteLine();

        List<string> panceljaid = new List<string>();
        foreach (string armorid in File.ReadLines($"../Fileok/{felhasznalo}/player_inventory.csv").Skip(1))
        {
            int itemid_armor = int.Parse(armorid.Trim());

            foreach (Item item in Items)
            {
                if (item is Armor armor && itemid_armor == item.id)
                {
                    panceljaid.Add(armor.id.ToString());
                    Console.WriteLine($"{armor.id} - {armor.nev} ({armor.ritkasag}) - Védelem bónusz: {armor.defbonusz}");
                }
            }
        }
        Console.WriteLine();

        Console.Write("Melyik sorszámú a armort szeretnéd felvenni?: ");
        string valasztas_armor = Console.ReadLine();
        if (kilepes_check_main_menu(valasztas_armor, felhasznalo)) { return; }

        while (!panceljaid.Contains(valasztas_armor))
        {
            Console.WriteLine("Nincs ilyen sorszámú armorod!");
            valasztas_armor = Console.ReadLine();
            if (kilepes_check_main_menu(valasztas_armor, felhasznalo)) { return; }
        }

        int valasztas_armor_int = int.Parse(valasztas_armor);

        Player p = Player.Peldanyositas(felhasznalo);
        p.felvett_pancel = valasztas_armor_int;
        p.Mentes(felhasznalo);

        Console.Clear();
        Console.WriteLine("Sikeresen felvetted!");
    }

    if (valasztas == "3")
    {
        Player p = Player.Peldanyositas(felhasznalo);
        p.felvett_fegyver = 0;
        p.Mentes(felhasznalo);

        Console.Clear();
        Console.WriteLine("Sikeresen levetted!");
    }

    if (valasztas == "4")
    {
        Player p = Player.Peldanyositas(felhasznalo);
        p.felvett_pancel = 0;
        p.Mentes(felhasznalo);

        Console.Clear();
        Console.WriteLine("Sikeresen levetted!");
    }
}
//---------------------------------------------------------------

void lootbox_start(string felhasznalo, List<Item> Items)
{
    Console.Clear();

    Player p = Player.Peldanyositas(felhasznalo);
    Console.WriteLine($"Egyenleged: {p.gold} gold");

    Console.WriteLine("---Kilépés a menübe (0)---");
    Console.WriteLine("Elérhető lootboxok:");
    Console.WriteLine();

    if (p.xp >= 150 && p.xp < 400)
    {
        Console.WriteLine("(1) Bronz lootbox - 20 gold");
        Console.WriteLine("(2) Ezüst lootbox - 60 gold");
    }
    else if (p.xp >= 400)
    {
        Console.WriteLine("(1) Bronz lootbox - 20 gold");
        Console.WriteLine("(2) Ezüst lootbox - 60 gold");
        Console.WriteLine("(3) Arany lootbox - 150 gold");
    }
    else
    {
        Console.WriteLine("(1) Bronz lootbox - 20 gold");
    }

    string valasztas = Console.ReadLine();
    if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; };

    if (p.xp >= 150 && p.xp < 400)
    {
        while (valasztas != "1" && valasztas != "2")
        {
            Console.WriteLine("Csak az elérhető lootbox(ok) sorszámát választhatod!");
            valasztas = Console.ReadLine();
            if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }
        }
    }
    else if (p.xp >= 400)
    {
        while (valasztas != "1" && valasztas != "2" && valasztas != "3")
        {
            Console.WriteLine("Csak az elérhető lootbox(ok) sorszámát választhatod!");
            valasztas = Console.ReadLine();
            if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }
        }
    }
    else
    {
        while (valasztas != "1")
        {
            Console.WriteLine("Csak az elérhető lootbox(ok) sorszámát választhatod!");
            valasztas = Console.ReadLine();
            if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }
        }
    }

    List<Item> elerhetoitemek = new(Items);

    foreach (string s in File.ReadLines($"../Fileok/{felhasznalo}/player_inventory.csv").Skip(1))
    {
        int item_id = int.Parse(s.Trim());
        elerhetoitemek.RemoveAll((Item i) => i.id == item_id);

    }

    List<int> common_items = new List<int>();
    List<int> uncommon_items = new List<int>();
    List<int> rare_items = new List<int>();
    List<int> epic_items = new List<int>();
    List<int> legendary_items = new List<int>();

    foreach (Item i in elerhetoitemek)
    {
        if (i.ritkasag == "common")
        {
            common_items.Add(i.id);
        }
        else if (i.ritkasag == "uncommon")
        {
            uncommon_items.Add(i.id);
        }
        else if (i.ritkasag == "rare")
        {
            rare_items.Add(i.id);
        }
        else if (i.ritkasag == "epic")
        {
            epic_items.Add(i.id);
        }
        else if (i.ritkasag == "legendary")
        {
            legendary_items.Add(i.id);
        }
    }

    if (valasztas == "1")
    {
        if (p.gold < 20)
        {
            Console.Clear();
            Console.WriteLine("Nincs elég büdzséd");
            return;
        }

        if (Random.Shared.Next(1, 100) <= 30)
        {
            if (uncommon_items.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Már megszerezted az összes uncommon itemet! Nem került levonásra az egyenleged.");
                return;
            }
            int pick_uncommon = uncommon_items[Random.Shared.Next(uncommon_items.Count)];
            lootbox_mentes(felhasznalo, Items, pick_uncommon);
        }
        else
        {
            if (common_items.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Már megszerezted az összes common itemet! Nem került levonásra az egyenleged.");
                return;
            }
            int pick_common = common_items[Random.Shared.Next(common_items.Count)];
            lootbox_mentes(felhasznalo, Items, pick_common);

        }
        p.gold = p.gold - 20;
        p.Mentes(felhasznalo);
    }

    if (valasztas == "2")
    {
        if (p.gold < 60)
        {
            Console.Clear();
            Console.WriteLine("Nincs elég büdzséd");
            return;
        }

        if (Random.Shared.Next(1, 100) <= 20)
        {
            if (rare_items.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Már megszerezted az összes rare itemet! Nem került levonásra az egyenleged.");
                return;
            }
            int pick_rare = rare_items[Random.Shared.Next(rare_items.Count)];
            lootbox_mentes(felhasznalo, Items, pick_rare);
        }
        else
        {
            if (uncommon_items.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Már megszerezted az összes uncommon itemet! Nem került levonásra az egyenleged.");
                return;
            }
            int pick_uncommon = uncommon_items[Random.Shared.Next(uncommon_items.Count)];
            lootbox_mentes(felhasznalo, Items, pick_uncommon);
        }
        p.gold = p.gold - 60;
        p.Mentes(felhasznalo);
    }

    if (valasztas == "3")
    {
        if (p.gold < 150)
        {
            Console.Clear();
            Console.WriteLine("Nincs elég büdzséd");
            return;
        }
        int rnd = Random.Shared.Next(1, 100);
        if (rnd >= 1 && rnd <= 5)
        {
            if (legendary_items.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Már megszerezted az összes legendary itemet! Nem került levonásra az egyenleged.");
                return;
            }
            int pick_legendary = legendary_items[Random.Shared.Next(legendary_items.Count)];
            lootbox_mentes(felhasznalo, Items, pick_legendary);
        }
        else if (rnd >= 6 && rnd <= 26)
        {
            if (epic_items.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Már megszerezted az összes epic itemet! Nem került levonásra az egyenleged.");
                return;
            }
            int pick_epic = epic_items[Random.Shared.Next(epic_items.Count)];
            lootbox_mentes(felhasznalo, Items, pick_epic);
        }
        else
        {
            if (rare_items.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Már megszerezted az összes rare itemet! Nem került levonásra az egyenleged.");
                return;
            }
            int pick_rare = rare_items[Random.Shared.Next(rare_items.Count)];
            lootbox_mentes(felhasznalo, Items, pick_rare);
        }
        p.gold = p.gold - 150;
        p.Mentes(felhasznalo);
    }
}

void lootbox_mentes(string felhasznalo, List<Item> Items, int pick)
{
        File.AppendAllText($"../Fileok/{felhasznalo}/player_inventory.csv", pick.ToString() + "\n");

        foreach (Item i in Items)
        {
            if (i.id == pick)
            {
                Console.Clear();
                Console.WriteLine($"Nyitottál egy {i.nev}-t ({i.ritkasag})");
            }
        }
}
//---------------------------------------------------------------



void bossfight(string felhasznalo)
{

    Console.Clear();
    Console.WriteLine("!!! A BOSSFIGHT SZERENCSE ALAPON MŰKÖDIK! VESZTÉSNÉL JELENTŐS LOOT VESZTÉS !!!");
    Console.WriteLine("Kilépés a menübe (0)");
    Console.WriteLine();
    Console.WriteLine("Elérhető bossok:");
    Console.WriteLine();
    Console.WriteLine("Sorszám - Név - Gold jutalom - XP jutalom");
    Player p = Player.Peldanyositas(felhasznalo);

    List<Boss> elerhetobossok_pre = new(Bosses);

    foreach (string s in File.ReadLines($"../Fileok/{felhasznalo}/legyozott_bossok.csv").Skip(1))
    {
        int item_id = int.Parse(s.Trim());
        elerhetobossok_pre.RemoveAll((Boss i) => i.id == item_id);

    }

    List<Boss> elerhetobossok = new();

    foreach (Boss boss in elerhetobossok_pre)
    {
        if (boss.szukseges_xp <= p.xp)
        {
            Console.WriteLine($"{boss.id} - {boss.nev} - {boss.arany_jutalom} - {boss.xp_jutalom}");
            elerhetobossok.Add(boss);
        }
    }

    if (elerhetobossok.Count == 0)
    {
        Console.Clear();
        Console.WriteLine("Jelenleg nincs elérhető boss. Szerezz több XP-t!");
        return;
    }

    Console.WriteLine();
    Console.WriteLine("Válassz egy bosst: ");
    string valasztas =  Console.ReadLine();
    if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }

    while (!int.TryParse(valasztas, out int ID) || !elerhetobossok.Any(b => b.id == ID))
    {
        Console.WriteLine("Érvénytelen! az elérhető listából adj meg egy sorszámot!");
        valasztas = Console.ReadLine();
        if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }
    }

    Boss kivalasztott_boss = null;
    foreach (Boss boss in elerhetobossok)
    {
        if (boss.id == int.Parse(valasztas))
        {
            kivalasztott_boss = boss;
            break;
        }
    }

    double player_harciero = p.harciero_kiszamitas();
    double boss_harciero = kivalasztott_boss.harciero_kiszamitas();

    double gyozelmi_esely = player_harciero * player_harciero / ((player_harciero * player_harciero) + (boss_harciero * boss_harciero));

    bool gyozott = Random.Shared.NextDouble() < gyozelmi_esely;

    Console.Clear();
    Console.WriteLine($"!! Ennél a bossnál a győzelmi esélyed ~{Math.Round(gyozelmi_esely*100)}%. Biztosan szeretnél vele harcolni? (0-kilépés | 1-igen | 2-nem)");
    string valasztas_harc = Console.ReadLine();
    if (kilepes_check_main_menu(valasztas_harc, felhasznalo)) { return; }

    while (valasztas_harc != "1" && valasztas_harc != "2")
    {
        Console.WriteLine("Az egyik opciót válaszd ki!");
        valasztas_harc = Console.ReadLine();
        if (kilepes_check_main_menu(valasztas_harc, felhasznalo)) { return; }
    }

    if (valasztas_harc == "2")
    {
        Console.Clear();
        return;
    }

    if (valasztas_harc == "1" && gyozott)
    {
        Console.Clear();
        Console.WriteLine("Gratulálok! Győztél!");
        Console.WriteLine($"Kaptál {kivalasztott_boss.arany_jutalom} aranyat és {kivalasztott_boss.xp_jutalom} XP-t");
        
        p.xp += kivalasztott_boss.xp_jutalom;
        p.gold += kivalasztott_boss.arany_jutalom;
        p.Mentes(felhasznalo);

        File.AppendAllText($"../Fileok/{felhasznalo}/legyozott_bossok.csv",kivalasztott_boss.id.ToString()+"\n");
    }



    if (valasztas_harc == "1" && !gyozott)
    {
        Console.Clear();
        Console.WriteLine("Vesztettél!");
        Console.WriteLine($"Elvesztettél {Math.Round((p.gold)*0.2)} aranyat és {Math.Round((p.xp)*0.1)} xp-t");

        p.xp = (int)Math.Round(p.xp * 0.9);
        p.gold = (int)Math.Round(p.gold * 0.8);
        p.Mentes(felhasznalo);
    }
}
void titkos(string felhasznalo)
{
    Console.Clear();
    Thread.Sleep(1000);
    Console.WriteLine("Legyőzted az összes bosst.");
    Console.WriteLine();
    Thread.Sleep(3500);
    Console.WriteLine("A játékot kivitted.");
    Console.WriteLine();
    Thread.Sleep(3500);
    Console.WriteLine("Ha most visszamehetnél az időben, talán most jobban tudnád, mennyit számít egyetlen kis lépés.");
    Console.WriteLine();
    Thread.Sleep(3500);
    Console.WriteLine("Emlékszel, hogyan kezdődött? Egy üres lista, egy halogatott feladat.");
    Console.WriteLine();
    Thread.Sleep(3500);
    Console.WriteLine("Emlékszel az elsőre?");
    Console.WriteLine();
    Thread.Sleep(3500);
    Console.WriteLine("Arra, amit végre nem holnapra halogattál?");
    Console.WriteLine();
    Thread.Sleep(3500);
    Console.WriteLine("Ez az app sosem a bossokról szólt.");
    Console.WriteLine();
    Thread.Sleep(3500);
    Console.WriteLine("Azért született, hogy a 'majd később' helyett a 'most' győzzön.");
    Console.WriteLine();
    Thread.Sleep(3500);
    Console.WriteLine("Nem a bossok miatt értél ide, hanem azért, mert minden nap elkezdted, amit korábban elhalogattál.");
    Console.WriteLine();
    Thread.Sleep(4500);
    Console.WriteLine("Minden egyes elvégzett task egy apró győzelem volt a halogatás felett.");
    Console.WriteLine();
    Thread.Sleep(3500);
    Console.WriteLine("Hiszen az igazi ellenfél mindig ott lesz egy új feladat képében.");
    Console.WriteLine();
    Thread.Sleep(3000);
    Console.WriteLine();
    Console.Write("Visszaállítod a progressed? (igen/nem):");
    string valasztas = Console.ReadLine().ToLower();
    while (valasztas != "igen" && valasztas != "nem")
    {  
        Console.WriteLine("rossz válasz.");
        valasztas = Console.ReadLine().ToLower();
    }
    if (valasztas == "nem")
    {
        Console.Clear();
        return;
    }
    if (valasztas == "igen")
    {
        Directory.Delete($"../Fileok/{felhasznalo}", true);
        create_datafiles(felhasznalo);
        Console.Clear();
        return;
    }
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
        Console.WriteLine("6. Inventory kezelése");
        Console.WriteLine("7. Lootbox nyitása");
        Console.WriteLine("8. Boss harcolása");
        Console.WriteLine("9. Kijelentkezés");
        int cnt = 0;
        bool beenged = false;
        foreach (string sor in File.ReadLines($"../Fileok/{felhasznalo}/legyozott_bossok.csv").Skip(1))
        {
            cnt += 1;
        }
        if (cnt == 30)
        {
            Console.WriteLine("10. ???");
            beenged = true;
        }
        Console.Write("Opció: ");


        string valasztas = Console.ReadLine();


        if (!main_valasztas_check(valasztas))
        {
            Console.Clear();
            Console.WriteLine("Helytelen! Válaszd ki az egyik opciót (1-9)");
            continue;
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
            inventory_kezelese(felhasznalo, Items);
        }
        else if (valasztas == "7")
        {
            lootbox_start(felhasznalo, Items);
        }
        else if (valasztas == "8")
        {
            bossfight(felhasznalo);
        }

        else if (valasztas == "9")
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" ---Sikeres kijelentkezes!---");
            Console.WriteLine();
            startup();
            break;
        }
        else if (valasztas == "10")
        {
            if (beenged)
            {
                titkos(felhasznalo);
            }
            else
            {
                Console.Clear();
                Console.Write("Helytelen! Válaszd ki az egyik opciót (1-9) ");
            }
        }
    }
}
//---------------------------------------------------------------
void task_torlese(string felhasznalo, List<Task> taskok)
{
    Console.WriteLine();
    Console.WriteLine("---Kilépés a menübe (0)---");
    Console.WriteLine();
    Task.Listazas(taskok);
    Console.WriteLine();
    Console.Write("Melyik sorszamu taskot szeretnéd kitörölni? (1-9): ");
    string valasztas = Console.ReadLine();


    if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }


    int v;
    while (!int.TryParse(valasztas, out v) || v < 1 || v > 9)
    {
        Console.Write("Helytelen, irj be egy szamot 1-9-ig: ");
        valasztas = Console.ReadLine();
        if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }
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
            Console.WriteLine();
            Console.WriteLine("---Sikeres törlés!---");
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
        Console.WriteLine();
        Console.WriteLine("---Kilépés a menübe(0)---");
        Console.WriteLine();
        Console.Write("Melyik task slotba szeretnéd felvenni a taskot (1-9): ");
        string valasztas = Console.ReadLine();


        if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }


        int v;
        while (!int.TryParse(valasztas, out v) || v < 1 || v > 9)
        {
            Console.WriteLine("Számot írj be 1-9-ig: ");
            valasztas = Console.ReadLine();
            if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }
        }


        foreach (Task task in taskok)
        {
            if (task.id == v)
            {
                if (!task.Nev.Contains("-"))
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("---A slot már foglalt. Válassz egy másikat---");
                    break;
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.Write("Add meg a task nevét: ");
                        string task_nev = Console.ReadLine();
                        while (task_nev.Contains("-"))
                        {
                            Console.Write("A task neve nem tartalmazhat '-'t (kötőjelet): ");
                            task_nev = Console.ReadLine();
                        }

                        Console.WriteLine();
                        Console.Clear();
                        Console.Write("Add meg a task nehézségét! (1-legkönnyebb | 9-legnehezebb): ");
                        string task_nehezseg = Console.ReadLine();
                        int int_task_nehezseg;
                        while (!int.TryParse(task_nehezseg, out int_task_nehezseg) || int_task_nehezseg < 1 || int_task_nehezseg > 9)
                        {
                            Console.Write("A nehézség csak 1 és 9 közötti szám lehet! (1-legkönnyebb | 9-legnehezebb): ");
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

    Console.WriteLine();
    Console.WriteLine("---Kilépés a menübe (0)---");
    Console.WriteLine();

    Task.Listazas(taskok);

    Console.WriteLine();
    Console.Write("Melyik sorszamu taskot csinaltad meg? (1-9): ");
    string valasztas = Console.ReadLine();


    if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }


    int v;
    while (!int.TryParse(valasztas, out v) || v < 1 || v > 9)
    {
        Console.WriteLine("Helytelen, irj be egy szamot 1-9-ig: ");
        valasztas = Console.ReadLine();
        if (kilepes_check_main_menu(valasztas, felhasznalo)) { return; }
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
            Console.Write("Nyomj Entert a kilepeshez: ");
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

