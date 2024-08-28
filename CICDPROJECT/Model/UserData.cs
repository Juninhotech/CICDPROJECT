namespace CICDPROJECT.Model
{
    public class UserData
    {

        public static List<User> Users { get; set; } = new List<User>
        {
            new User {Id = 1, Name = "Moses Bliss", Email = "blissplay@gmail.com", Password = "123456"},
            new User {Id = 2, Name = "Tade Ogidan", Email = "tadeogi@gmail.com", Password = "1293939"}
        };

        public static AccessCode code { get; } = new AccessCode
        {
            AccessCodes = "283828"
        };
    }
}
