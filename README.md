# ChorePG – feladatkezelő játék


Egy C# alkalmazás, ami a teendőidet játékká alakítja: felveszed a feladataidat, és ha teljesíted őket, **XP-t** és **aranyat** kapsz. Minden felhasználónak saját fiókja és saját mentése van.


---


# Felhasználóknak


## Mire jó a program?


Ahelyett, hogy kipipálnád a teendőidet, a program megjutalmaz érte. Minél nehezebb egy feladat, annál több XP-t és aranyat kapsz. A cuccaid elmentődnek, így legközelebb ott folytathatod, ahol abbahagytad.


## Indítás


A program elindítása után ezt látod:


```
        ----Üdvözöljük!----
Bejelentkezés (1) | Regisztrálás (2) | Kilépés (3):
```


| Szám | Művelet |
|------|---------|
| `1`  | Bejelentkezés létező fiókba |
| `2`  | Új fiók hozzáadása |
| `3`  | Kilépés a programból |


## Regisztráció


1. Válaszd a **2**-es opciót.
2. Add meg a neveded (bármi lehet). A program automatikusan **nagybetűssé** alakítja, szóval a `Peti` és a `peti` ugyanaz a fiók.
3. Add meg a jelszavadat.
4. Ha a név még szabad, a fiók létrejön, és visszavisz a kezdőmenübe, ahol be tudsz jelentkezni.


Ha már van ilyen nevű felhasználó, a program hibaüzenetet ír ki.


> **Tipp:** A felhasználónévben ne használj kettőspontot (`:`), mert az nem tuff (hibaüzenetet ad vissza).


## Bejelentkezés


Add meg a neved és a jelszavad. Ha bármelyikhez `0`-t írsz, visszavisz az elejére.


## Főmenü


Sikeres belépés után ez a menü jelenik meg:


```
1. Taskok megtekintése
2. Új task hozzáadása
3. Profil statok megnézése (XP, Level)
4. Task teljesítése
5. Kijelentkezés
```


### 1. Taskok megtekintése


Kilistázza mind a **9 task-ot**. Egy sor így néz ki:


```
Task sorszáma - Neve - Mennyire nehéz - XP - Gold
1 - Matekházi - 5 - 20 - 9
2 - üres task slot
3 - üres task slot
...
```


### 2. Új task hozzáadása


1. Válaszd ki, melyik **slotba** (1–9) akarod felvenni a feladatot. Ha a slot már foglalt, a program szól hogy másikat kell választanod.
2. Add meg a task **nevét**. A névben nem lehet kötőjelet (`-`)(Ha megadod akkor hibaüzenetet ad vissza).
3. Add meg a **nehézséget** 1 és 9 között (1 = legkönnyebb, 9 = legnehezebb).


A jutalom (XP, Gold) a nehézségtől függően, egy megadott határon  belül **random** dől el a task felvételekor (Gambling).


| Nehézség | XP  | Gold |
|---------|---------|---------|
| 1–3 | 5–10 | 1–5 |
| 4–6 | 15–25 | 6–12 |
| 7–9 | 35–50 | 20–30 |


Kilépés a menübe bármikor a `0` beírásával.


### 3. Profil statok megnézése


Megmutatja a statjaidat (XP, Gold, jelenlegi fegyver, páncél). Az **Enter** lenyomásával vissza mész a főmenübe.


### 4. Task teljesítése


Kilistázza a taskjaidat, majd megkérdezi, melyiket csináltad meg. A teljesítés után a slot kiürül, az XP-t és a gold-ot megkapod. Üres slotot nem lehet teljesíteni.


### 5. Task törlése


Kilistázza a taskjaidat, majd megkérdezi, melyiket akarod törölni. Ezzel a task eltűnik és az XP-t és a Gold-ot nem kapod meg.


### 6. Kijelentkezés


Visszavisz a kezdőmenübe.




## Hol vannak a mentéseim?


A fájlok a `../Fileok/<FELHASZNÁLÓNÉV>/` -ban találhatók. Ezeket ne írd át kézzel, mert a program érzékeny, és egy elírás elronthatja.


---


# Fejlesztőinknek


## A programról


- Nyelv: **C#**
- Adattárolás: sima szöveges / **CSV** fájlokkal
- A  nyelve: magyar


## Sükségek és futtatás


1. Töltsd le a NET SDK-t.
2. Klónozd a repót, majd indítsd el a program.cs fájlt.
**Fontos:** a program a **./Fileok mappában** keresi a fájlokat (látod lent). Figyelj rá, hogy a CSV-k a mappában elérhetők legyenek.




## Szükséges CSV fájlok



| CSV | Létrejövő fájl |
|--------|--------------------------------|---------|
| `template_player_stats.csv` | `player_stats.csv` |
| `template_tasks.csv` | `player_tasks.csv`  |
| `template_task_archivum.csv` | `player_task_archivum.csv` |
| `template_player_inventory.csv` | `player_inventory.csv` |
| `template_legyozott_bossok.csv` | `legyozott_bossok.csv` |


Ha valamelyik hiányzik, a regisztráció nem lesz sikeres.


## Fájlstruktúra


```
../Fileok/
├── regisztraciok.txt
└── <FELHASZNÁLÓNÉV>/
    ├── player_stats.csv
    ├── player_tasks.csv
    ├── player_task_archivum.csv
    ├── player_inventory.csv
    └── legyozott_bossok.csv
```


### `regisztraciok.txt`


Soronként fiókok, `NÉV:kódolt_jelszó` formátumban. A nevet a program nagybetűsé alakítja, a jelszót a `JelszoKoder` osztály kódolja, és ellenőrzi (`Ellenorzes`).


### `player_tasks.csv`


Pontosvesszővel tagolt fájl, fejléccel, **9 slot-al**:


```
id;nev;nehezseg;xp;gold
1;-;1;0;0
2;Matekházi;5;20;9
```



### `player_stats.csv`


Fejléccel, a sorrend:


```
xp;gold;felvett_fegyver;felvett_pancel;megcsinalt_taskok
```


Az `xp_gold_adding` az `adatok[0]`, `adatok[1]` és `adatok[4]` okat növeli.


## A kód felépítése


A program a `Program.cs` függvényekből áll, amelyek egymást hívják.


| Függvény | Feladata |
|----------|----------|
| `startup()` | Kezdőmenü (bejelentkezés / regisztráció / kilépés) |
| `register()` | Regisztráció, dupla név ellenőrzése, fájlok létrehozása |
| `login()` | Bejelentkezés a `regisztraciok.txt` alapján |
| `create_datafiles()` | A mappák létrehozása |
| `main_menu()` | Főmenü |
| `task_peldanyositas()` | A `player_tasks.csv` beolvasása  |
| `task_felvetele()` | Új task hozzáadása  |
| `task_felvetele_write()` | A teljes lista írása CSV-be (fejléc + 9 sor) |
| `task_listazas()` | Taskok kiírása |
| `task_teljesitese()` | Task megcsinálása, slot ürítése, jutalom odaadása |
| `xp_gold_adding()` | XP, arany és megcsinált taskok számának növelése |
| `task_xp_szamitas()` / `task_gold_szamitas()` | Random számú jutalom a nehézség alapján |
| `kilepes_check_startup()` / `kilepes_check_main_menu()` | A `0` beírására visszalépés a megfelelő menübe |


A `Task`, `Regisztracio` és `JelszoKoder` osztályok külön fájlokban vannak.



## Hozzájárulás


Hibát vagy ötletet issue-ban jelezhetsz. Új funkciónál tartsd a CSV-formátumokat.



