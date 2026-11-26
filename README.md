<div align="center">

#  ChorePG

### A feladatkezelő játék

Egy C# alkalmazás, ami a teendőidet játékká alakítja: felveszed a feladataidat, és ha teljesíted őket, XP-t és aranyat kapsz. Minden felhasználónak saját fiókja és saját mentése van.

<br>

---


<div align="center">

#  Felhasználóknak

</div>

##  Mi ez?

Ahelyett, hogy csak kipipálnád a teendőidet, a ChorePG **megjutalmaz érte**.

Felveszed a feladataidat, és amikor megcsinálod őket, **XP-t** és **aranyat** kapsz. Az aranyból lootboxokat nyithatsz, amikből fegyverek és páncélok esnek. Ezekkel erősebb leszel, és bossokkal is harcolhatsz. Mindenkinek saját fiókja és mentése van.

---

##  Indítás

Indítás után ezt látod:

```
        ----Üdvözöljük!----
Bejelentkezés (1) | Regisztrálás (2) | Kilépés (3):
```

| Szám | Művelet |
|:----:|---------|
| `1` | 🔑 Bejelentkezés  fiókba |
| `2` | 📝 Új fiók létrehozása |
| `3` | 🚪 Kilépés a programból |

> ⌨️ Bárhol a `0` beírásával visszaléphetsz az előző menübe.

---

##  Fiók létrehozása és belépés

### Regisztráció

1. Válaszd a **2**-es opciót.
2. Add meg a neved és a jelszavad.
3. Ha a név szabad, a fiók létrejön, és visszakerülsz a kezdőmenübe, ahol be tudsz jelentkezni.

Ha már van ilyen nevű felhasználó, a program hibaüzenetet ír ki.

| | Szabály |
|---|---|
| **Felhasználónév** | 3–12 karakter, nem tartalmazhat `:`  `/`  |
|  **Jelszó** | 8–16 karakter, legalább 1 nagybetű és 1 szám, nem tartalmazhat `:`  `/`  |

> A felhasználónevet a program automatikusan nagybetűssé alakítja, tehát a `Peti` és a `peti` ugyanaz.
>
>  A jelszavad titkosítva tárolódik

### Bejelentkezés

Add meg a nevedet és a jelszavadat. Rossz jelszó vagy ismeretlen név esetén újra próbálkozhatsz.

---

##  Főmenü

Sikeres belépés után ez jelenik meg:

```
1. Taskok megtekintése
2. Új task hozzáadása
3. Profil statok megnézése (XP, Level)
4. Task teljesítése
5. Task törlése
6. Inventory kezelése
7. Lootbox nyitása
8. Boss harcolása
9. Kijelentkezés
```

A **9**-es opció kijelentkeztet, és visszavisz.

---

##  Taskok

Összesen **9 slotod** van, ezekbe veheted fel a feladataidat.

### 1. Taskok megtekintése

Kilistázza mind a 9 slotot:

```
 =========================================================================
---Task sorszáma - Neve - Nehézségi fokozata - XP jutalom - Gold jutalom---
 =========================================================================
1 - Matekházi - 5 - 20 - 9
2 - üres task slot
3 - üres task slot
...
```

### 2. Új task hozzáadása

1. Válaszd ki a slotot (**1–9**). Ha foglalt, másikat kell választani.
2. Add meg a task **nevét**.  A név nem tartalmazhat kötőjelet (`-`) (hibaüzenetet kapsz).
3. Add meg a **nehézséget** (1 = legkönnyebb, 9 = legnehezebb).

A jutalom a felvételkor **random** a nehézséghez tartozó sávon belül:

| Nehézség | ⭐ XP | 🪙 Gold |
|:--------:|:-----:|:-------:|
| 1–3 | 5–10 | 1–5 |
| 4–6 | 15–25 | 6–12 |
| 7–9 | 35–50 | 20–30 |

### 4. Task teljesítése

Kilistázza a taskjaidat, majd megkérdezi, melyiket csináltad meg és:

- megkapod az XP-t és a goldot,
- a slot kiürül,
- a feladat lementődik.

>  **Napi limit:** legfeljebb **10-et** teljesíthetsz egy nap.

Üres slotot nem lehet teljesíteni.

### 5. Task törlése

Kitörli a kiválasztott taskot. Jutalmat  **nem** kapsz.

---

##  Profil és szintek

A **3**-as menüpont megmutatja:

- ⭐ XP és jelenlegi szint
- 🪙 Gold
- 🗡️ Felvett fegyver
- 🛡️ Felvett páncél
- ✅ Megcsinált taskok száma

> **Szint = XP ÷ 100** (lefelé kerekítve), legfeljebb **30**.

Az **Enter** lenyomásával visszamész a főmenübe.

---

## 6. Inventory

Látod a megszerzett fegyvereidet és páncéljaidat, és itt tudod őket felvenni vagy levenni.

| Opció | Hatás |
|:-----:|-------|
| `1` | Fegyver felvétele |
| `2` | Páncél felvétele |
| `3` | Fegyver levétele |
| `4` | Páncél levétele |

- A **fegyver** sebzést ad, a **páncél** védelmet.
- Egyszerre **egy fegyvert és egy páncélt** használhatsz.

---

## 7. Lootboxok

Itt az aranyadat itemekre válthatod. Mindig olyan itemet kapsz, ami **nincs meg**.

| Lootbox | Ár | Feltétel | Esélyek |
|---------|:--:|:--------:|---------|
|  **Bronz** | 20  | – | ~70% common · ~30% uncommon |
|  **Ezüst** | 60  | 150 XP | ~80% uncommon · ~20% rare |
|  **Arany** | 150  | 400 XP | ~73% rare · ~21% epic · ~5% legendary |

>  Ha mind megvan egy kategóriából akkor jelzi, visszaadja a goldot

---

## 8. Bossfight

Megküzdhetsz a bossokkal. Egy boss csak akkor jelenik meg, ha van hozzá elég XP-d, és még nem győzted le.

A harc szerencsejáték alapú, de a jobb felszerelés javítja az esélyedet. A megerősítés előtt a program kiírja au   esélyedet:

```
!! Ennél a bossnál a győzelmi esélyed ~64%. Biztosan szeretnél vele harcolni?
```

| Kimenet | Következmény |
|---------|--------------|
|  **Győzelem** | Kapsz aranyat és XP-t, a boss pedig végleg legyőzött lesz |
|  **Vereség** | Elveszíted az aranyad **20%-át** és az XP-d **10%-át** |


---

## 10? 

Ha mind a **30 bosst** legyőzted, a főmenüben megjelenik egy rejtélyes **10. `???`** opció. 

Ha igazán tudni akarod akkor nézd meg. De megérné?

---

## Hol vannak a mentéseim?

A fájlaid a `../Fileok/<FELHASZNÁLÓNÉV>/` mappában találhatók.

> **Ezeket ne szerkeszd kézzel!** A program érzékeny, egy elírás elronthat egy mentést.

---

<div align="center">

#  Fejlesztőknek

</div>

## Technológia

| | |
|---|---|
| **Nyelv** | C# (.NET) |
| **Felület** | Konzol |
| **Adattárolás** | Szöveges és CSV fájlok |
| **Jelszókezelés** | PBKDF2 |
| **Nyelv (UI és a kód)** | Magyar |

---

## ▶Futtatás

**Feltétel:** kell .NET SDK

```bash
git clone <repo-url>
cd <projektmappa>
dotnet run
```

>
> - a `weapons.csv`, `armor.csv`, `bosses.csv` és a `template_*.csv` fájlok a **munkakönyvtárban** vannak,
> - a mentések a `../Fileok/` mappába kerülnek (a `Program.cs` induláskor létrehozza).
>

Ha bármelyik szükséges CSV hiányzik, a program indításkor  hibával leáll.

---


### Regisztrációkor másolt sablonok

| Sablon | → Létrejövő fájl |
|--------|------------------|
| `template_player_stats.csv` | `player_stats.csv` |
| `template_tasks.csv` | `player_tasks.csv` |
| `template_task_archivum.csv` | `player_task_archivum.csv` |
| `template_player_inventory.csv` | `player_inventory.csv` |
| `template_legyozott_bossok.csv` | `legyozott_bossok.csv` |

---

##  CSV formátumok

Minden fájl pontosvesszővel (`;`) tagolt, **fejlécsorral** kezdődik.

<br>

**`weapons.csv`**
```csv
id;nev;ritkasag;atkbonusz
1;Fa kard;common;2
```

**`armor.csv`**
```csv
id;nev;ritkasag;defbonusz
101;Bőrvért;common;2
```

**`bosses.csv`**
```csv
id;nev;atk;def;szukseges_xp;arany_jutalom;xp_jutalom
1;Goblin;10;8;100;40;60
```

 **Fontos szabályok:**

- A `ritkasag` ezek közül lehet (kisbetűvel): `common`, `uncommon`, `rare`, `epic`, `legendary`.
- Az item ID-k **egyediek** legyenek fegyverek és páncélok nál is, mert az inventory egy ID-listát használ.
- Az `id = 0` a „nincs felvéve" jelölése, ezért az itemek  **1-től** indulnak.


<br>

**`player_stats.csv`**
```csv
xp;gold;felvett_fegyver;felvett_pancel;megcsinalt_taskok
0;0;0;0;0
```

**`player_tasks.csv`** az üres slotok neve `-`
```csv
id;nev;nehezseg;xp;gold
1;-;1;0;0
2;Matekházi;5;20;9
...
9;-;1;0;0
```

**`player_task_archivum.csv`** —  minden teljesített task
```csv
datum;nev;nehezseg
2026-11-26;Matekházi;5
```

**`player_inventory.csv`** — a megszerzett itemek ID-i, soronként egy
```csv
item_id
1
101
```

**`legyozott_bossok.csv`** — a legyőzött bossok ID-i, soronként egy
```csv
boss_id
1
```


<br>

Soronként egy fiók, `NÉV:jelszo` formátumban (a salt és a hash Base64):

```
PETI:Zm9vYmFy....:YmF6cXV4....
```

A név nagybetűs. A `:` karakter tiltott a felhasználónévben és a jelszóban.



---

## Osztályok


| Osztály | Felelősség |
|---------|------------|
| `Item`, `Weapon`, `Armor` | A felszerelések modellje. A `Weapon` és az `Armor` az `Item` leszármazottja |
| `Boss` | Boss adatai és a harci erő számítása (`atk + def`) |
| `Player` | Játékos adatai, mentés és betöltés CSV-ből, szint és harci erő számítása, fegyver és páncél keresése |
| `Task` | Feladatok betöltése és mentése, listázás, jutalomsorsolás |
| `Regisztracio` | Felhasználónév és jelszó validáció  |
| `JelszoKoder` | `Kodolas` (hash + salt generálás) és `Ellenorzes` (jelszó ellenőrzése) |

---

## Program.cs függvényei

A `Program.cs` top-level statements stílusú: a lokális függvények egymást hívják, a végén egy `while (true)` ciklus indítja a `startup()`-ot.

| Függvény | Feladata |
|----------|----------|
| `startup()` | Kezdőmenü (bejelentkezés / regisztráció / kilépés) |
| `register()` | Regisztráció, duplikált név ellenőrzése, fájlok létrehozása |
| `login()` | Bejelentkezés a `regisztraciok.txt` alapján |
| `create_datafiles()` | Felhasználói mappa és CSV-k létrehozása a sablonok alapján |
| `main_menu()` | Főmenü, funkciók hívása |
| `task_felvetele()` | Új task felvétele slotba |
| `task_teljesitese()` | Task teljesítése, jutalom, archiválás, napi limit |
| `task_torlese()` | Task törlése jutalom nélkül |
| `felvetel_limit_check()` | Napi 10 teljesítés ellenőrzése |
| `player_stat_kiiratas()` | Profil megnézése |
| `inventory_kezelese()` | Itemek listázása, fel és levétel |
| `lootbox_start()` | Lootbox vásárlás és sorsolás |
| `lootbox_mentes()` | A kihúzott item mentése |
| `bossfight()` | Boss kiválasztása, esélyszámítás, harc |
| `kilepes_check_startup()` / `kilepes_check_main_menu()` | `0` beírásával visszalépés az adott menüből |

---

##  Képletek

**Szint**

```
szint = min(30, xp / 100)
```

**Játékos harci ereje**

```
harciero = 12 + szint + 2 × (fegyver_atk + páncél_def)
```

**Boss harci ereje**

```
boss_harciero = atk + def
```

**Győzelmi esély**

```
esely = P² / (P² + B²)        (P = játékos, B = boss harci ereje)
```



**Lootbox sorsolás** 
```
Random.Shared.Next(1, 100)
```   

| Doboz | Szabály |
|-------|---------|
|  Bronz | `≤ 30` → uncommon, különben common |
|  Ezüst | `≤ 20` → rare, különben uncommon |
|  Arany | `1–5` → legendary, `6–26` → epic, különben rare |

---

<div align="center">

