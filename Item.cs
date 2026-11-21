public class Item
{
    public int id {  get; set; }
    public string nev { get; set; }
    public string ritkasag { get; set; }


    public Item(int id , string nev, string ritkasag)
    {
        this.id = id;
        this.nev = nev;
        this.ritkasag = ritkasag;
    }

}