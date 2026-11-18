using System.Security.Cryptography;

class JelszoKoder
{
    public string Kodolas(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] kodolt_jelszo = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);
        return Convert.ToBase64String(salt)+"."+Convert.ToBase64String(kodolt_jelszo);
    }

    public bool Ellenorzes(string password, string stored)
    {
        string[] bont = stored.Split('.');
        byte[] salt = Convert.FromBase64String(bont[0]);
        byte[] tenyleges_jelszo = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);
        return bont[1] == Convert.ToBase64String(tenyleges_jelszo);
    }
}
