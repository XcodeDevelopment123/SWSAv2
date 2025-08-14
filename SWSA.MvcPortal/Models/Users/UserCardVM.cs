namespace SWSA.MvcPortal.Models.Users;

public class UserCardVM
{
    public int Id { get; set; }
    public string StaffId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Experience { get; set; }
    public string Avatar
    {
        get
        {
            var words = Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (words.Length >= 2)
            {
                return string.Concat(
                    char.ToUpperInvariant(words[0][0]),
                    char.ToUpperInvariant(words[1][0]));
            }

            var caps = Name.Where(char.IsUpper).Take(2).ToArray();
            if (caps.Length >= 2)
                return new string(caps);
            if (caps.Length == 1)
                return caps[0].ToString();

            return char.ToUpperInvariant(Name[0]).ToString();
        }
    }
    public string Department { get; set; }
}
