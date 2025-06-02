using System.ComponentModel.DataAnnotations;

namespace E_commerce.Server.Model.Entities
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        public string? First_Name { get; set; }

        public string? Last_Name { get; set; }

        public DateTime Date_Of_Birth { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone_Number { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

    }
}
